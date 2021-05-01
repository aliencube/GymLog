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
        public virtual RoutineType Routine { get; set; }

        /// <summary>
        /// Gets or sets the list of exercises.
        /// </summary>
        [JsonProperty("exercises")]
        public virtual List<Exercise> Exercises { get; set;}
    }
}
