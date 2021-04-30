using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class Exercise
    {
        [JsonProperty("exerciseId")]
        public virtual Guid ExerciseId { get; set; }

        [JsonProperty("name")]
        public virtual string Name { get; set; }

        [JsonProperty("sets")]
        public virtual List<ExerciseSet> Sets { get; set; } = new List<ExerciseSet>();
    }
}
