using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using GymLog.FunctionApp.ActionResults;
using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

namespace GymLog.FunctionApp.Extensions
{
    public static partial class AzureResponseExtensions
    {
        public static ObjectResult ToRecordResponseMessage(this List<ExerciseEntity> entities, Guid correlationId, Guid spanId, Guid eventId, Guid routineId, RoutineType routine, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            if (!entities.Any())
            {
                var result = new ErrorObjectResult()
                {
                    CorrelationId = correlationId,
                    SpanId = spanId,
                    EventId = eventId,
                    Message = EventType.RecordNotFound.ToDisplayName(),
                    StatusCode = (int)HttpStatusCode.NotFound,
                };

                return result;
            }

            var exercises = entities.Select(p => new Exercise()
                                            {
                                                ExerciseId = p.ExerciseId,
                                                Name = p.Exercise,
                                                Sets = p.Sets.FromJson<List<ExerciseSet>>(),
                                                AdditionalNotes = p.AdditionalNotes,
                                            }
                                     )
                                    .ToList();

            var msg = new RecordResponseMessage()
            {
                CorrelationId = correlationId,
                SpanId = spanId,
                EventId = eventId,
                RoutineId = routineId,
                Routine = routine,
                Exercises = exercises,
            };

            return new ObjectResult(msg) { StatusCode = (int)httpStatusCode };
        }
    }
}
