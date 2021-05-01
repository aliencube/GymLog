using System;

using GymLog.FunctionApp.Traces;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the response message entity. This MUST be inherited.
    /// </summary>
    public abstract class ResponseMessage
    {
        /// <summary>
        /// Gets or sets the correlation ID.
        /// </summary>
        [JsonProperty("correlationId")]
        public virtual Guid CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the interface type.
        /// </summary>
        [JsonProperty("interface")]
        public virtual InterfaceType Interface { get; set; }

        /// <summary>
        /// Gets or sets the span ID.
        /// </summary>
        [JsonProperty("spanId")]
        public virtual Guid SpanId { get; set; }

        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        [JsonProperty("eventId")]
        public virtual Guid EventId { get; set; }
    }
}
