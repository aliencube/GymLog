using System;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the request message entity for publish.
    /// </summary>
    public class PublishRequestMessage : RequestMessage
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
    }
}
