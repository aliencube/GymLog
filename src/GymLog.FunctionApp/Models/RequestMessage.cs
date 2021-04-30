using System;

using GymLog.FunctionApp.Traces;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public abstract class RequestMessage
    {
        [JsonProperty("correlationId")]
        public virtual Guid CorrelationId { get; set; }

        [JsonProperty("interface")]
        public virtual InterfaceType Interface { get; set; }

        [JsonProperty("spanId")]
        public virtual Guid SpanId { get; set; }
    }
}
