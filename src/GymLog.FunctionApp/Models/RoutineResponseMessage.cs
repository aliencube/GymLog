using System;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class RoutineResponseMessage : ResponseMessage
    {
        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        [JsonProperty("routine")]
        public virtual RoutineType Routine { get; set; }
    }
}
