using System;
using System.Collections.Generic;

using GymLog.FunctionApp.Traces;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the record entity for routine.
    /// </summary>
    public class RoutineRecordItem : RecordItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoutineRecordItem"/> class.
        /// </summary>
        public RoutineRecordItem()
        {
            this.ItemType = RecordItemType.Routine;
        }

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
        /// Gets or sets the interface.
        /// </summary>
        [JsonProperty("interface")]
        public virtual InterfaceType Interface { get; set; }

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
        public virtual List<Exercise> Exercises { get; set;}

        /// <summary>
        /// Gets or sets the date/time when the message is queued.
        /// </summary>
        [JsonProperty("timestamp")]
        public virtual DateTimeOffset Timestamp { get; set; }
    }
}
