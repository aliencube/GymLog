using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class RoutineRequestMessage : RequestMessage
    {
        [JsonProperty("routine")]
        public virtual RoutineType Routine { get; set; }
    }
}
