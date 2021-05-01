namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the exercise entity group.
    /// </summary>
    public class ExerciseEntityGroup
    {
        /// <summary>
        /// Gets or sets the exercise name.
        /// </summary>
        public string Exercise { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ExerciseEntity"/> object.
        /// </summary>
        public ExerciseEntity Entity { get; set; }
    }
}
