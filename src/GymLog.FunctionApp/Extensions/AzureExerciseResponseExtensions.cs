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
    public static partial class AzureResponseExtensions
    {
        public static ObjectResult ToExerciseResponseMessage(this Response response, Guid correlationId, Guid spanId, Guid eventId, Guid routineId, Guid exerciseId, string exercise, string sets, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
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

            var msg = new ExerciseResponseMessage()
            {
                CorrelationId = correlationId,
                SpanId = spanId,
                EventId = eventId,
                RoutineId = routineId,
                ExerciseId = exerciseId,
                Exercise = exercise,
                Sets  = sets.FromJson<List<ExerciseSet>>(),
            };

            return new ObjectResult(msg) { StatusCode = (int)httpStatusCode };
        }

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
