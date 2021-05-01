using System;
using System.Collections.Generic;
using System.Net;

using Azure;

using GymLog.FunctionApp.ActionResults;
using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Mvc;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="Response"/> class.
    /// </summary>
    public static partial class AzureResponseExtensions
    {
        /// <summary>
        /// Gets the <see cref="ObjectResult"/> object containing the <see cref="ExerciseResponseMessage"/> object.
        /// </summary>
        /// <param name="response"><see cref="Response"/> object.</param>
        /// <param name="correlationId">Correlation ID.</param>
        /// <param name="interface"><see cref="InterfaceType"/> value.</param>
        /// <param name="spanId">Span ID.</param>
        /// <param name="eventId">Event ID.</param>
        /// <param name="routineId">Routine ID.</param>
        /// <param name="exerciseId">Exercise ID.</param>
        /// <param name="exercise">Exercise name.</param>
        /// <param name="sets">List of exercise sets.</param>
        /// <param name="additionalNotes">Additional notes.</param>
        /// <param name="httpStatusCode"><see cref="HttpStatusCode"/> value.</param>
        /// <returns>Returns the <see cref="ObjectResult"/> object.</returns>
        public static ObjectResult ToExerciseResponseMessage(this Response response,
                                                                  Guid correlationId,
                                                                  InterfaceType @interface,
                                                                  Guid spanId,
                                                                  Guid eventId,
                                                                  Guid routineId,
                                                                  RoutineType routine,
                                                                  Guid exerciseId,
                                                                  string exercise,
                                                                  string sets,
                                                                  string additionalNotes,
                                                                  HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            if (response.Status >= (int)HttpStatusCode.BadRequest)
            {
                var result = new ErrorObjectResult()
                {
                    CorrelationId = correlationId,
                    Interface = @interface,
                    SpanId = spanId,
                    EventId = eventId,
                    Message = $"{response.Status}: {response.ReasonPhrase}",
                    StatusCode = response.Status,
                };

                return result;
            }

            var msg = new ExerciseResponseMessage()
            {
                CorrelationId = correlationId,
                Interface = @interface,
                SpanId = spanId,
                EventId = eventId,
                RoutineId = routineId,
                Routine = routine,
                ExerciseId = exerciseId,
                Exercise = exercise,
                Sets  = sets.FromJson<List<ExerciseSet>>(),
                AdditionalNotes = additionalNotes,
            };

            return new ObjectResult(msg) { StatusCode = (int)httpStatusCode };
        }

        /// <summary>
        /// Gets the <see cref="EventType"/> value from the HTTP status code value.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <returns>Returns the <see cref="EventType"/> value.</returns>
        public static EventType ToExerciseEventType(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return EventType.ExerciseCreated;
            }

            return EventType.ExerciseNotCreated;
        }
    }
}
