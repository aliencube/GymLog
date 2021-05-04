using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the record entity for routine.
    /// </summary>
    public class RoutineRecord
    {
        /// <summary>
        /// Gets or sets the entity record ID.
        /// </summary>
        [JsonProperty("id")]
        public virtual Guid EntityId { get; set; }

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
        public virtual RoutineType Routine { get; set; }

        /// <summary>
        /// Gets or sets the list of exercises.
        /// </summary>
        [JsonProperty("exercises")]
        public virtual List<Exercise> Exercises { get; set;}

        /// <summary>
        /// Gets or sets the date/time when the message is queued.
        /// </summary>
        [JsonProperty("timestamp")]
        public virtual DateTimeOffset Timestamp { get; set; }
    }
}
