using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    /// <summary>
    /// This specifies the event status type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventStatusType
    {
        /// <summary>
        /// Identifies event succeeded.
        /// </summary>
        Succeeded,

        /// <summary>
        /// Identifies event failed.
        /// </summary>
        Failed,
    }
}
