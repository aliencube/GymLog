using System;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class RoutineRecord : RoutineQueueMessage
    {
        public RoutineRecord(RoutineQueueMessage message)
        {
            this.CorrelationId = message.CorrelationId;
            this.RoutineId = message.RoutineId;
            this.Routine = message.Routine;
            this.Exercises = message.Exercises;
        }

        [JsonProperty("id")]
        public virtual Guid EntityId { get; set; }
    }
}
