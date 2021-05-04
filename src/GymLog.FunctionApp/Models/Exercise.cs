using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the exercise entity.
    /// </summary>
    public class Exercise
    {
        /// <summary>
        /// Gets or sets the exercise ID.
        /// </summary>
        [JsonProperty("exerciseId")]
        public virtual Guid ExerciseId { get; set; }

        /// <summary>
        /// Gets or sets the exercise name.
        /// </summary>
        [JsonProperty("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public virtual TargetType Target { get; set; }

        /// <summary>
        /// Gets or sets the list of exercise sets.
        /// </summary>
        [JsonProperty("sets")]
        public virtual List<ExerciseSet> Sets { get; set; } = new List<ExerciseSet>();

        /// <summary>
        /// Gets or sets the additional notes about exercise.
        /// </summary>
        [JsonProperty("additionalNotes")]
        public virtual string AdditionalNotes { get; set; }
    }
}
