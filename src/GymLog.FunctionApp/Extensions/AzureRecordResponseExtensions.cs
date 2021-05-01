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
    /// <summary>
    /// This represents the extension entity containing the <see cref="RecordResponseMessage"/> object.
    /// </summary>
    public static partial class AzureResponseExtensions
    {
        /// <summary>
        /// Gets the <see cref="ObjectResult"/> object from the list of <see cref="ExerciseEntity"/> objects.
        /// </summary>
        /// <param name="entities">List of <see cref="ExerciseEntity"/> objects.</param>
        /// <param name="correlationId">Correlation ID.</param>
        /// <param name="interface"><see cref="InterfaceType"/> value.</param>
        /// <param name="spanId">Span ID.</param>
        /// <param name="eventId">Event ID.</param>
        /// <param name="routineId">Routine ID.</param>
        /// <param name="routine"><see cref="RoutineType"/> value.</param>
        /// <param name="httpStatusCode"><see cref="HttpStatusCode"/> value.</param>
        /// <returns>Returns the <see cref="ObjectResult"/> object.</returns>
        public static ObjectResult ToRecordResponseMessage(this List<ExerciseEntity> entities,
                                                                Guid correlationId,
                                                                InterfaceType @interface,
                                                                Guid spanId,
                                                                Guid eventId,
                                                                Guid routineId,
                                                                RoutineType routine,
                                                                HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            if (!entities.Any())
            {
                var result = new ErrorObjectResult()
                {
                    CorrelationId = correlationId,
                    Interface = @interface,
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
                Interface = @interface,
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
