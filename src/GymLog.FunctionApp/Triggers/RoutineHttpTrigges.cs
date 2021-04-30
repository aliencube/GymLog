using System;
using System.Net;
using System.Threading.Tasks;

using Azure.Data.Tables;

using GymLog.FunctionApp.ActionResults;
using GymLog.FunctionApp.Configurations;
using GymLog.FunctionApp.Examples;
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
    public class RoutineHttpTrigger
    {
        private readonly AppSettings _settings;
        private readonly TableServiceClient _client;

        public RoutineHttpTrigger(AppSettings settings, TableServiceClient client)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [FunctionName(nameof(RoutineHttpTrigger.CreateRoutineAsync))]
        [OpenApiOperation(operationId: "CreateRoutine", tags: new[] { "publisher", "routine" }, Summary = "Create a new routine", Description = "This creates a new routine", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header, Description = "API key to execute this endpoint")]
        [OpenApiRequestBody(contentType: ContentTypes.ApplicationJson, bodyType: typeof(RoutineRequestMessage), Required = true, Example = typeof(RoutineRequestMessageExample), Description = "The request message payload for a routine")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: ContentTypes.ApplicationJson, bodyType: typeof(RoutineResponseMessage), Example = typeof(RoutineResponseMessageExample), Summary = "200 response", Description = "This returns the response of 'OK'")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: ContentTypes.ApplicationJson, bodyType: typeof(ErrorResponseMessage), Example = typeof(ErrorResponseMessageExample), Summary = "500 response", Description = "This returns the response of 'Internal Server Error'")]
        public async Task<IActionResult> CreateRoutineAsync(
            [HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "routines")] HttpRequest req,
            ExecutionContext context,
            ILogger log)
        {
            var request = await req.ToRoutineRequestMessageAsync().ConfigureAwait(false);
            var eventId = context.InvocationId;
            var spanId = request.SpanId;
            var correlationId = request.CorrelationId;

            log.LogData(LogLevel.Information, request,
                        EventType.RoutineReceived, EventStatusType.Succeeded, eventId,
                        SpanType.Publisher, SpanStatusType.PublisherInitiated, spanId,
                        request.Interface, correlationId);

            var routineId = Guid.NewGuid();
            var entity = new RoutineEntity()
            {
                PartitionKey = correlationId.ToString(),
                RowKey = eventId.ToString(),
                CorrelationId = correlationId,
                SpanId = spanId,
                EventId = eventId,
                EventName = EventType.RoutineCreated.ToDisplayName(),
                RoutineId = routineId,
                Routine = request.Routine,
            };

            var res = default(ObjectResult);
            try
            {
                await this._client.CreateTableIfNotExistsAsync(this._settings.GymLog.StorageAccount.Table.TableName).ConfigureAwait(false);
                var table = this._client.GetTableClient(this._settings.GymLog.StorageAccount.Table.TableName);
                var response = await table.UpsertEntityAsync(entity).ConfigureAwait(false);

                res = response.ToRoutineResponseMessage(correlationId, spanId, eventId, entity.RoutineId, entity.Routine);

                log.LogData(response.Status.ToLogLevel(), res.Value,
                            response.Status.ToRoutineCreatedEventType(), response.Status.ToEventStatusType(), eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            request.Interface, correlationId,
                            clientRequestId: response.ClientRequestId,
                            message: response.Status.ToResponseMessage(res));
            }
            catch (Exception ex)
            {
                res = new InternalServerErrorObjectResult()
                {
                    CorrelationId = correlationId,
                    SpanId = spanId,
                    EventId = eventId,
                    Message = ex.Message,
                };

                log.LogData(LogLevel.Error, res.Value,
                            EventType.RoutineNotCreated, EventStatusType.Failed, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            request.Interface, correlationId,
                            ex: ex,
                            message: ex.Message);
            }

            return res;
        }
    }
}
