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
    /// <summary>
    /// This represents the HTTP trigger entity for exercise.
    /// </summary>
    public class ExerciseHttpTrigger
    {
        private readonly AppSettings _settings;
        private readonly TableServiceClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciseHttpTrigger"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="client"><see cref="TableServiceClient"/> instance.</param>
        public ExerciseHttpTrigger(AppSettings settings, TableServiceClient client)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Creates exercises record.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="routineId">Routine ID.</param>
        /// <param name="context"><see cref="ExecutionContext"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="ExerciseResponseMessage"/> object.</returns>
        [FunctionName(nameof(ExerciseHttpTrigger.CreateExercisesAsync))]
        [OpenApiOperation(operationId: "CreateExercise", tags: new[] { "publisher", "exercise" }, Summary = "Create a new exercise", Description = "This creates a new exercise", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header, Description = "API key to execute this endpoint")]
        [OpenApiParameter(name: "routineId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Routine ID", Description = "The routine to add the exercise", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: ContentTypes.ApplicationJson, bodyType: typeof(ExerciseRequestMessage), Required = true, Example = typeof(ExerciseRequestMessageExample), Description = "The request message payload for an exercise")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: ContentTypes.ApplicationJson, bodyType: typeof(ExerciseResponseMessage), Example = typeof(ExerciseResponseMessageExample), Summary = "200 response", Description = "This returns the response of 'OK'")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: ContentTypes.ApplicationJson, bodyType: typeof(ErrorResponseMessage), Example = typeof(ErrorResponseMessageExample), Summary = "500 response", Description = "This returns the response of 'Internal Server Error'")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "400 response", Description = "This returns the response of 'Bad Request'")]
        public async Task<IActionResult> CreateExercisesAsync(
            [HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "routines/{routineId}/exercises")] HttpRequest req,
            Guid routineId,
            ExecutionContext context,
            ILogger log)
        {
            var request = await req.ToRequestMessageAsync<ExerciseRequestMessage>().ConfigureAwait(false);
            var eventId = context.InvocationId;
            var spanId = request.SpanId;
            var correlationId = request.CorrelationId;
            var @interface = request.Interface;

            log.LogData(LogLevel.Information, request,
                        EventType.ExerciseReceived, EventStatusType.Succeeded, eventId,
                        SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                        @interface, correlationId);

            if (routineId != request.RoutineId)
            {
                log.LogData(LogLevel.Error, request,
                            EventType.InvalidRoutine, EventStatusType.Failed, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            message: EventType.InvalidRoutine.ToDisplayName());

                return new BadRequestResult();
            }

            var exerciseId = Guid.NewGuid();
            var entity = new ExerciseEntity()
            {
                PartitionKey = correlationId.ToString(),
                RowKey = eventId.ToString(),
                CorrelationId = correlationId,
                SpanId = spanId,
                EventId = eventId,
                EventName = EventType.ExerciseCreated.ToDisplayName(),
                RoutineId = request.RoutineId,
                Routine = request.Routine,
                ExerciseId = exerciseId,
                Exercise = request.Exercise,
                Sets = request.Sets.ToJson(),
                AdditionalNotes = request.AdditionalNotes,
            };

            var res = default(ObjectResult);
            try
            {
                await this._client.CreateTableIfNotExistsAsync(this._settings.GymLog.StorageAccount.Table.TableName).ConfigureAwait(false);
                var table = this._client.GetTableClient(this._settings.GymLog.StorageAccount.Table.TableName);
                var response = await table.UpsertEntityAsync(entity).ConfigureAwait(false);

                res = response.ToExerciseResponseMessage(correlationId, @interface, spanId, eventId, entity.RoutineId, entity.ExerciseId, entity.Exercise, entity.Sets, entity.AdditionalNotes);

                log.LogData(response.Status.ToLogLevel(), res.Value,
                            response.Status.ToExerciseEventType(), response.Status.ToEventStatusType(), eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            clientRequestId: response.ClientRequestId,
                            message: response.Status.ToResponseMessage(res));
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
                            EventType.ExerciseNotCreated, EventStatusType.Failed, eventId,
                            SpanType.Publisher, SpanStatusType.PublisherInProgress, spanId,
                            @interface, correlationId,
                            ex: ex,
                            message: ex.Message);
            }

            return res;
        }
    }
}
