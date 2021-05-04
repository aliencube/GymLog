using System;
using System.Net;
using System.Threading.Tasks;

using Azure.Data.Tables;

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
    /// This represents the HTTP trigger entity for routine.
    /// </summary>
    public class RoutineHttpTrigger
    {
        private readonly AppSettings _settings;
        private readonly TableServiceClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutineHttpTrigger"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="client"><see cref="TableServiceClient"/> instance.</param>
        public RoutineHttpTrigger(AppSettings settings, TableServiceClient client)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Creates routine record.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="context"><see cref="ExecutionContext"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="RoutineResponseMessage"/> object.</returns>
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
            var request = await req.ToRequestMessageAsync<RoutineRequestMessage>().ConfigureAwait(false);
            var eventId = context.InvocationId;
            var spanId = request.SpanId;
            var correlationId = request.CorrelationId;
            var upn = request.Upn;
            var @interface = request.Interface;

            log.LogData(LogLevel.Information, request,
                        EventType.RoutineReceived, EventStatusType.Succeeded, eventId,
                        SpanType.Publisher, SpanStatusType.PublisherInitiated, spanId,
                        @interface, correlationId);

            var routineId = Guid.NewGuid();
            var entity = new RoutineEntity()
            {
                PartitionKey = correlationId.ToString(),
                RowKey = eventId.ToString(),
                Upn = upn,
                CorrelationId = correlationId,
                SpanId = spanId,
                Interface = @interface,
                EventId = eventId,
                EventName = EventType.RoutineCreated.ToDisplayName(),
                RoutineId = routineId,
                Routine = request.Routine,
                Targets = request.Targets.ToJson(),
            };

            var res = default(ObjectResult);
            try
            {
                if (this._settings.ForceError.Publisher.Routine)
                {
                    throw new ErrorEnforcementException("Error Enforced!");
                }

                await this._client.CreateTableIfNotExistsAsync(this._settings.GymLog.StorageAccount.Table.TableName).ConfigureAwait(false);
                var table = this._client.GetTableClient(this._settings.GymLog.StorageAccount.Table.TableName);
                var response = await table.UpsertEntityAsync(entity).ConfigureAwait(false);

                res = response.ToRoutineResponseMessage(request, entity.EventId, entity.RoutineId);

                log.LogData(response.Status.ToLogLevel(), res.Value,
                            response.Status.ToRoutineCreatedEventType(), response.Status.ToEventStatusType(), eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            clientRequestId: response.ClientRequestId,
                            message: response.Status.ToResponseMessage(res));
            }
            catch (Exception ex)
            {
                res = new InternalServerErrorObjectResult()
                {
                    Upn = upn,
                    CorrelationId = correlationId,
                    Interface = @interface,
                    SpanId = spanId,
                    EventId = eventId,
                    Message = ex.Message,
                };

                log.LogData(LogLevel.Error, res.Value,
                            EventType.RoutineNotCreated, EventStatusType.Failed, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            ex: ex,
                            message: ex.Message);
            }

            return res;
        }
    }
}
