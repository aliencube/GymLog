using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Azure.Data.Tables;
using Azure.Messaging.ServiceBus;

using GymLog.FunctionApp.ActionResults;
using GymLog.FunctionApp.Configurations;
using GymLog.FunctionApp.Examples;
using GymLog.FunctionApp.Exceptions;
using GymLog.FunctionApp.Extensions;
using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GymLog.FunctionApp.Triggers
{
    /// <summary>
    /// This represents the HTTP trigger entity for record/publish.
    /// </summary>
    public class RecordHttpTrigger
    {
        private const string GymLogTopicKey = "%AzureWebJobsServiceBusTopicName%";

        private readonly AppSettings _settings;
        private readonly TableServiceClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordHttpTrigger"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="client"><see cref="TableServiceClient"/> instance.</param>
        public RecordHttpTrigger(AppSettings settings, TableServiceClient client)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Publishes routine record.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="routineId">Routine ID.</param>
        /// <param name="context"><see cref="ExecutionContext"/> instance.</param>
        /// <param name="collector"><see cref="IAsyncCollector{ServiceBusMessage}"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="RecordResponseMessage"/> object.</returns>
        [FunctionName(nameof(RecordHttpTrigger.PublishRoutineAsync))]
        [OpenApiOperation(operationId: "PublishRoutine", tags: new[] { "publisher", "publish" }, Summary = "Publish the routine", Description = "This publishes the routine", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header, Description = "API key to execute this endpoint")]
        [OpenApiParameter(name: "routineId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Routine ID", Description = "The routine ID to publish", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: ContentTypes.ApplicationJson, bodyType: typeof(RecordRequestMessage), Required = true, Example = typeof(RecordRequestMessageExample), Description = "The request message payload for a record/publish")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: ContentTypes.ApplicationJson, bodyType: typeof(RecordResponseMessage), Example = typeof(RecordResponseMessageExample), Summary = "200 response", Description = "This returns the response of 'OK'")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: ContentTypes.ApplicationJson, bodyType: typeof(ErrorResponseMessage), Example = typeof(ErrorResponseMessageExample), Summary = "500 response", Description = "This returns the response of 'Internal Server Error'")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "400 response", Description = "This returns the response of 'Bad Request'")]
        public async Task<IActionResult> PublishRoutineAsync(
            [HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "routines/{routineId}/publish")] HttpRequest req,
            Guid routineId,
            ExecutionContext context,
            [ServiceBus(GymLogTopicKey)] IAsyncCollector<ServiceBusMessage> collector,
            ILogger log)
        {
            var request = await req.ToRequestMessageAsync<RecordRequestMessage>().ConfigureAwait(false);
            var eventId = context.InvocationId;
            var spanId = request.SpanId;
            var correlationId = request.CorrelationId;
            var @interface = request.Interface;

            log.LogData(LogLevel.Information, request,
                        EventType.PublishRequestReceived, EventStatusType.Succeeded, eventId,
                        SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                        @interface, correlationId);

            if (routineId != request.RoutineId)
            {
                log.LogData(LogLevel.Error, request,
                            EventType.InvalidPublishRequest, EventStatusType.Failed, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            message: EventType.InvalidPublishRequest.ToDisplayName());

                return new BadRequestResult();
            }

            var entity = new RoutineEntity()
            {
                PartitionKey = correlationId.ToString(),
                RowKey = eventId.ToString(),
                CorrelationId = correlationId,
                SpanId = spanId,
                EventId = eventId,
                EventName = EventType.RoutineCompleted.ToDisplayName(),
                RoutineId = routineId,
                Routine = request.Routine,
            };

            var res = default(ObjectResult);
            try
            {
                if (this._settings.ForceError.Publisher.Publish)
                {
                    throw new ErrorEnforcementException("Error Enforced!");
                }

                await this._client.CreateTableIfNotExistsAsync(this._settings.GymLog.StorageAccount.Table.TableName).ConfigureAwait(false);
                var table = this._client.GetTableClient(this._settings.GymLog.StorageAccount.Table.TableName);

                var records = await table.QueryAsync<ExerciseEntity>(p => p.PartitionKey == correlationId.ToString()
                                                                       && p.SpanId == spanId)
                                         .ToListAsync()
                                         .ConfigureAwait(false);

                records = records.Where(p => !p.Exercise.IsNullOrWhiteSpace())
                                 .ToList();

                if (!records.Any())
                {
                    res = records.ToRecordResponseMessage(correlationId, @interface, spanId, eventId, request.RoutineId, request.Routine);

                    log.LogData(LogLevel.Error, res.Value,
                                EventType.RecordNotFound, EventStatusType.Failed, eventId,
                                SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                                request.Interface, correlationId,
                                message: EventType.RecordNotFound.ToDisplayName());

                    return res;
                }

                var entities = records.GroupBy(p => p.Exercise)
                                      .Select(g => new ExerciseEntityGroup() { Exercise = g.Key, Entity = g.OrderByDescending(q => q.Timestamp).First() })
                                      .Select(p => p.Entity)
                                      .OrderByDescending(p => p.Timestamp)
                                      .ToList();

                res = entities.ToRecordResponseMessage(correlationId, @interface, spanId, eventId, request.RoutineId, request.Routine);

                log.LogData(LogLevel.Information, res.Value,
                            EventType.RecordPopulated, EventStatusType.Succeeded, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            message: EventType.RecordPopulated.ToDisplayName());

                var messageId = Guid.NewGuid();
                var subSpanId = Guid.NewGuid();
                var message = (RoutineQueueMessage)(RecordResponseMessage)res.Value;
                var msg = new ServiceBusMessage(message.ToJson())
                {
                    CorrelationId = correlationId.ToString(),
                    MessageId = messageId.ToString(),
                };
                msg.ApplicationProperties.Add("pubSpanId", spanId);
                msg.ApplicationProperties.Add("subSpanId", subSpanId);
                msg.ApplicationProperties.Add("interface", @interface.ToString());

                await collector.AddAsync(msg).ConfigureAwait(false);

                log.LogData(LogLevel.Information, msg,
                            EventType.MessagePublished, EventStatusType.Succeeded, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            messageId: messageId.ToString(),
                            message: EventType.MessagePublished.ToDisplayName());

                var response = await table.UpsertEntityAsync(entity).ConfigureAwait(false);

                var r = response.ToRoutineResponseMessage(correlationId, @interface, spanId, eventId, entity.RoutineId, entity.Routine);

                log.LogData(response.Status.ToLogLevel(), r.Value,
                            response.Status.ToRoutineCompletedEventType(), response.Status.ToEventStatusType(), eventId,
                            SpanType.Publisher, SpanStatusType.PublisherCompleted, spanId,
                            @interface, correlationId,
                            clientRequestId: response.ClientRequestId,
                            message: response.Status.ToResponseMessage(r));
            }
            catch (Exception ex)
            {
                res = new InternalServerErrorObjectResult()
                {
                    CorrelationId = correlationId,
                    Interface = @interface,
                    SpanId = spanId,
                    EventId = eventId,
                    Message = ex.Message,
                };

                log.LogData(LogLevel.Error, res.Value,
                            EventType.MessageNotPublished, EventStatusType.Failed, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherCompleted, spanId,
                            @interface, correlationId,
                            ex: ex,
                            message: ex.Message);
            }

            return res;
        }
    }
}
