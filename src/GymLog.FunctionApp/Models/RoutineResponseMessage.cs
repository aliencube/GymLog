using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the response message entity for routine.
    /// </summary>
    public class RoutineResponseMessage : ResponseMessage
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
        public virtual string Routine { get; set; }

        /// <summary>
        /// Gets or sets the list of targets.
        /// </summary>
        [JsonProperty("targets")]
        public virtual List<TargetType> Targets { get; set; }
    }
}
