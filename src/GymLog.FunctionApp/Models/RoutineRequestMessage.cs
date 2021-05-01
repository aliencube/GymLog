using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the request message entity for routine.
    /// </summary>
    public class RoutineRequestMessage : RequestMessage
    {
        /// <summary>
        /// Gets or sets the routine name.
        /// </summary>
        [JsonProperty("routine")]
        public virtual RoutineType Routine { get; set; }
    }
}
