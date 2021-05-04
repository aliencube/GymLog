using System;
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
        /// <param name="request"><see cref="ExerciseRequestMessage"/> object.</param>
        /// <param name="eventId">Event ID.</param>
        /// <param name="exerciseId">Exercise ID.</param>
        /// <param name="httpStatusCode"><see cref="HttpStatusCode"/> value.</param>
        /// <returns>Returns the <see cref="ObjectResult"/> object.</returns>
        public static ObjectResult ToExerciseResponseMessage(this Response response,
                                                                  ExerciseRequestMessage request,
                                                                  Guid eventId,
                                                                  Guid exerciseId,
                                                                  HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            if (response.Status >= (int)HttpStatusCode.BadRequest)
            {
                var result = new ErrorObjectResult()
                {
                    Upn = request.Upn,
                    CorrelationId = request.CorrelationId,
                    Interface = request.Interface,
                    SpanId = request.SpanId,
                    EventId = eventId,
                    Message = $"{response.Status}: {response.ReasonPhrase}",
                    StatusCode = response.Status,
                };

                return result;
            }

            var msg = new ExerciseResponseMessage()
            {
                Upn = request.Upn,
                CorrelationId = request.CorrelationId,
                Interface = request.Interface,
                SpanId = request.SpanId,
                EventId = eventId,
                RoutineId = request.RoutineId,
                Routine = request.Routine,
                Target = request.Target,
                ExerciseId = exerciseId,
                Exercise = request.Exercise,
                Sets  = request.Sets,
                AdditionalNotes = request.AdditionalNotes,
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
