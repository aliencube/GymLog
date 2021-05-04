using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the response message entity for record/publish.
    /// </summary>
    public class RecordResponseMessage : ResponseMessage
    {
        /// <summary>
        /// Gets or sets the routine ID.
        /// </summary>
        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        /// <summary>
        /// Gets or sets the routine name.
        /// </summary>
        [JsonProperty("routine")]
        public virtual RoutineType Routine { get; set; }

        /// <summary>
        /// Gets or sets the list of exercises.
        /// </summary>
        [JsonProperty("exercises")]
        public virtual List<Exercise> Exercises { get; set;}

        /// <summary>
        /// Converts the <see cref="RecordResponseMessage"/> object to the <see cref="RoutineQueueMessage"/> object implicitly.
        /// </summary>
        /// <param name="instance"><see cref="RecordResponseMessage"/> object.</param>
        /// <returns>Returns the <see cref="RoutineQueueMessage"/> object.</returns>
        public static implicit operator RoutineQueueMessage(RecordResponseMessage instance)
        {
            var message = new RoutineQueueMessage()
            {
                Upn = instance.Upn,
                CorrelationId = instance.CorrelationId,
                RoutineId = instance.RoutineId,
                Routine = instance.Routine,
                Exercises = instance.Exercises,
            };

            return message;
        }
    }
}
