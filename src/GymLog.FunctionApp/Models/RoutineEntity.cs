using System;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the entity for routine.
    /// </summary>
    public class RoutineEntity : ItemEntity
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
        /// Gets or sets the list of targets.
        /// </summary>
        public virtual string Targets { get; set; }
    }
}
