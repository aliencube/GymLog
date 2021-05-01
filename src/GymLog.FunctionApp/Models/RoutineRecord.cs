using System;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the record entity for routine.
    /// </summary>
    public class RoutineRecord : RoutineQueueMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoutineRecord"/> class.
        /// </summary>
        /// <param name="message"><see cref="RoutineQueueMessage"/> object.</param>
        public RoutineRecord(RoutineQueueMessage message)
        {
            this.CorrelationId = message.CorrelationId;
            this.RoutineId = message.RoutineId;
            this.Routine = message.Routine;
            this.Exercises = message.Exercises;
        }

        /// <summary>
        /// Gets or sets the entity record ID.
        /// </summary>
        [JsonProperty("id")]
        public virtual Guid EntityId { get; set; }
    }
}
