using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    /// <summary>
    /// This specifies the span type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpanType
    {
        /// <summary>
        /// Identifies publisher.
        /// </summary>
        Publisher,

        /// <summary>
        /// Identifies subscriber.
        /// </summary>
        Subscriber,
    }
}
