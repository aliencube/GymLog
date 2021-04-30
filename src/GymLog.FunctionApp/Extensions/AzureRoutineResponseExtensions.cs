using System;
using System.Net;

using Azure;

using GymLog.FunctionApp.ActionResults;
using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Mvc;

namespace GymLog.FunctionApp.Extensions
{
    public static partial class AzureResponseExtensions
    {
        public static ObjectResult ToRoutineResponseMessage(this Response response, Guid correlationId, Guid spanId, Guid eventId, Guid routineId, RoutineType routine, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            if (response.Status >= (int)HttpStatusCode.BadRequest)
            {
                var result = new ErrorObjectResult()
                {
                    CorrelationId = correlationId,
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
                SpanId = spanId,
                EventId = eventId,
                RoutineId = routineId,
                Routine = routine,
            };

            return new ObjectResult(msg) { StatusCode = (int)httpStatusCode };
        }

        public static EventType ToRoutineCreatedEventType(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return EventType.RoutineCreated;
            }

            return EventType.RoutineNotCreated;
        }

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
