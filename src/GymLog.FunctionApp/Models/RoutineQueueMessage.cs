using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class RoutineQueueMessage
    {
        [JsonProperty("correlationId")]
        public virtual Guid CorrelationId { get; set; }

        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        [JsonProperty("routine")]
        public virtual RoutineType Routine { get; set; }

        [JsonProperty("exercises")]
        public virtual List<Exercise> Exercises { get; set;}
    }
}
