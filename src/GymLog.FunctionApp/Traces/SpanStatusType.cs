using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpanStatusType
    {
        [EnumMember(Value = "Publisher Initiated")]
        PublisherInitiated = 1000,

        [EnumMember(Value = "Publisher In-Progress")]
        PublisherInProgress = 1100,

        [EnumMember(Value = "Publisher Completed")]
        PublisherCompleted = 1200,

        [EnumMember(Value = "Subscriber Initiated")]
        SubscriberInitiated = 2000,

        [EnumMember(Value = "Subscriber In-Progress")]
        SubscriberInProgress = 2100,

        [EnumMember(Value = "Subscriber Completed")]
        SubscriberCompleted = 2200,
    }
}
