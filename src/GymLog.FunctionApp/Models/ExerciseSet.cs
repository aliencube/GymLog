using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the exercise set entity.
    /// </summary>
    public class ExerciseSet
    {
        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        [JsonProperty("sequence")]
        public virtual int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the repetition count.
        /// </summary>
        [JsonProperty("repetitions")]
        public virtual int Repetitions { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        [JsonProperty("weight")]
        public virtual decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the weight unit.
        /// </summary>
        [JsonProperty("unit")]
        public virtual WeightUnitType Unit { get; set; }
    }
}
