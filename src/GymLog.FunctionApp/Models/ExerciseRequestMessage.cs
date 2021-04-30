using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class ExerciseRequestMessage : RequestMessage
    {
        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        [JsonProperty("routine")]
        public virtual RoutineType Routine { get; set; }

        [JsonProperty("exercise")]
        public virtual string Exercise { get; set; }

        [JsonProperty("sets")]
        public virtual List<ExerciseSet> Sets { get; set; } = new List<ExerciseSet>();
    }
}
