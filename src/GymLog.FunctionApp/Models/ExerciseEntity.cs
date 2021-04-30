using System;

namespace GymLog.FunctionApp.Models
{
    public class ExerciseEntity : ItemEntity
    {
        /// <summary>
        /// Gets or sets the routine ID.
        /// </summary>
        public virtual Guid RoutineId { get; set; }

        /// <summary>
        /// Gets or sets the routine type.
        /// </summary>
        public virtual RoutineType Routine { get; set; }

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
    }
}
