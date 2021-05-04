using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the request message entity for exercise.
    /// </summary>
    public class ExerciseRequestMessage : RequestMessage
    {
        /// <summary>
        /// Gets or sets the routine ID.
        /// </summary>
        [JsonProperty("routineId")]
        public virtual Guid RoutineId { get; set; }

        /// <summary>
        /// Gets or sets the routine name.
        /// </summary>
        [JsonProperty("routine")]
        public virtual string Routine { get; set; }

        /// <summary>
        /// Gets or sets the exercise name.
        /// </summary>
        [JsonProperty("exercise")]
        public virtual string Exercise { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        [JsonProperty("target")]
        public virtual TargetType Target { get; set; }

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
