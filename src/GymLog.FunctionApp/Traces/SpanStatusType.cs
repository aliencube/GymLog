using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    /// <summary>
    /// This specifies the span status type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpanStatusType
    {
        /// <summary>
        /// Identifies the publisher initiated.
        /// </summary>
        [EnumMember(Value = "Publisher Initiated")]
        PublisherInitiated = 1000,

        /// <summary>
        /// Identifies the publisher in-progress.
        /// </summary>
        [EnumMember(Value = "Publisher In-Progress")]
        PublisherInProgress = 1100,

        /// <summary>
        /// Identifies the publisher completed.
        /// </summary>
        [EnumMember(Value = "Publisher Completed")]
        PublisherCompleted = 1200,

        /// <summary>
        /// Identifies the subscriber initiated.
        /// </summary>
        [EnumMember(Value = "Subscriber Initiated")]
        SubscriberInitiated = 2000,

        /// <summary>
        /// Identifies the subscriber in-progress.
        /// </summary>
        [EnumMember(Value = "Subscriber In-Progress")]
        SubscriberInProgress = 2100,

        /// <summary>
        /// Identifies the subscriber completed.
        /// </summary>
        [EnumMember(Value = "Subscriber Completed")]
        SubscriberCompleted = 2200,
    }
}
