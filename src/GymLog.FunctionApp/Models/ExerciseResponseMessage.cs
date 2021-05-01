using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the response message entity for exercise.
    /// </summary>
    public class ExerciseResponseMessage : ResponseMessage
    {
        /// <summary>
        /// Gets or sets the routine ID.
        /// </summary>
        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        /// <summary>
        /// Gets or sets the exercise ID.
        /// </summary>
        [JsonProperty("exerciseId")]
        public virtual Guid ExerciseId { get; set; }

        /// <summary>
        /// Gets or sets the exercise name.
        /// </summary>
        [JsonProperty("exercise")]
        public virtual string Exercise { get; set; }

        /// <summary>
        /// Gets or sets the list of the exercise sets.
        /// </summary>
        [JsonProperty("sets")]
        public virtual List<ExerciseSet> Sets { get; set; } = new List<ExerciseSet>();

        /// <summary>
        /// Gets or sets the additional notes about the exercise.
        /// </summary>
        [JsonProperty("additionalNotes")]
        public virtual string AdditionalNotes { get; set; }
    }
}
