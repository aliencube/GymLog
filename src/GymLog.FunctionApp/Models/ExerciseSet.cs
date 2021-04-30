using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class ExerciseSet
    {
        [JsonProperty("sequence")]
        public virtual int Sequence { get; set; }

        [JsonProperty("repetitions")]
        public virtual int Repetitions { get; set; }

        [JsonProperty("weight")]
        public virtual decimal Weight { get; set; }

        [JsonProperty("unit")]
        public virtual WeightUnitType Unit { get; set; }
    }
}
