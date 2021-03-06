using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the queue message for the routine.
    /// </summary>
    public class RoutineQueueMessage
    {
        /// <summary>
        /// Gets or sets the user principal name (UPN).
        /// </summary>
        [JsonProperty("upn")]
        public virtual string Upn { get; set; }

        /// <summary>
        /// Gets or sets the correlation ID.
        /// </summary>
        [JsonProperty("correlationId")]
        public virtual Guid CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the routine ID.
        /// </summary>
        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        /// <summary>
        /// Gets or sets the routine name.
        /// </summary>
        [JsonProperty("routine")]
        public virtual string Routine { get; set; }

        /// <summary>
        /// Gets or sets the list of exercises.
        /// </summary>
        [JsonProperty("exercises")]
        public virtual List<Exercise> Exercises { get; set; }

        /// <summary>
        /// Converts the <see cref="RoutineQueueMessage"/> object to the <see cref="RoutineRecordItem"/> object implicitly.
        /// </summary>
        /// <param name="instance"><see cref="RoutineQueueMessage"/> object.</param>
        /// <returns>Returns the <see cref="RoutineRecordItem"/> object.</returns>
        public static implicit operator RoutineRecordItem(RoutineQueueMessage instance)
        {
            return new RoutineRecordItem()
            {
                Upn = instance.Upn,
                CorrelationId = instance.CorrelationId,
                RoutineId = instance.RoutineId,
                Routine = instance.Routine,
                Exercises = instance.Exercises,
            };
        }
    }
}
