using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class RecordResponseMessage : ResponseMessage
    {
        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        [JsonProperty("routine")]
        public virtual RoutineType Routine { get; set; }

        [JsonProperty("exercises")]
        public virtual List<Exercise> Exercises { get; set;}

        public static implicit operator RoutineQueueMessage(RecordResponseMessage instance)
        {
            var message = new RoutineQueueMessage()
            {
                CorrelationId = instance.CorrelationId,
                RoutineId = instance.RoutineId,
                Routine = instance.Routine,
                Exercises = instance.Exercises,
            };

            return message;
        }
    }
}
