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
        /// Gets the <see cref="ObjectResult"/> object containing the <see cref="RoutineResponseMessage"/> object.
        /// </summary>
        /// <param name="response"><see cref="Response"/> object.</param>
        /// <param name="correlationId">Correlation ID.</param>
        /// <param name="interface"><see cref="InterfaceType"/> value.</param>
        /// <param name="spanId">Span ID.</param>
        /// <param name="eventId">Event ID.</param>
        /// <param name="routineId">Routine ID.</param>
        /// <param name="routine"><see cref="RoutineType"/> value.</param>
        /// <param name="httpStatusCode"><see cref="HttpStatusCode"/> value.</param>
        /// <returns>Returns the <see cref="ObjectResult"/> object.</returns>
        public static ObjectResult ToRoutineResponseMessage(this Response response,
                                                                 Guid correlationId,
                                                                 InterfaceType @interface,
                                                                 Guid spanId,
                                                                 Guid eventId,
                                                                 Guid routineId,
                                                                 RoutineType routine,
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

            var msg = new RoutineResponseMessage()
            {
                CorrelationId = correlationId,
                Interface = @interface,
                SpanId = spanId,
                EventId = eventId,
                RoutineId = routineId,
                Routine = routine,
            };

            return new ObjectResult(msg) { StatusCode = (int)httpStatusCode };
        }

        /// <summary>
        /// Gets the <see cref="EventType"/> value from the HTTP status code value.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <returns>Returns the <see cref="EventType"/> value.</returns>
        public static EventType ToRoutineCreatedEventType(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return EventType.RoutineCreated;
            }

            return EventType.RoutineNotCreated;
        }

        /// <summary>
        /// Gets the <see cref="EventType"/> value from the HTTP status code value.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <returns>Returns the <see cref="EventType"/> value.</returns>
        public static EventType ToRoutineCompletedEventType(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return EventType.RoutineCompleted;
            }

            return EventType.RoutineNotCompleted;
        }
    }
}
