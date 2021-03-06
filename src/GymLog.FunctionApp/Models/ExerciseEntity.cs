using System;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the exercise entity.
    /// </summary>
    public class ExerciseEntity : ItemEntity
    {
        /// <summary>
        /// Gets or sets the routine ID.
        /// </summary>
        public virtual Guid RoutineId { get; set; }

        /// <summary>
        /// Gets or sets the routine name.
        /// </summary>
        public virtual string Routine { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public virtual TargetType Target { get; set; }

        /// <summary>
        /// Gets or sets the exercise ID.
        /// </summary>
        public virtual Guid ExerciseId { get; set; }

        /// <summary>
        /// Gets or sets the exercise name.
        /// </summary>
        public virtual string Exercise { get; set; }

        /// <summary>
        /// Gets or sets the list of exercise sets.
        /// </summary>
        public virtual string Sets { get; set; }

        /// <summary>
        /// Gets or sets the additional notes.
        /// </summary>
        public virtual string AdditionalNotes { get; set; }
    }
}
