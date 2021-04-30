using System;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public abstract class ResponseMessage
    {
        [JsonProperty("correlationId")]
        public virtual Guid CorrelationId { get; set; }

        [JsonProperty("spanId")]
        public virtual Guid SpanId { get; set; }

        [JsonProperty("eventId")]
        public virtual Guid EventId { get; set; }
    }
}
