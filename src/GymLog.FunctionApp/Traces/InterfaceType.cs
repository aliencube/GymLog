using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InterfaceType
    {
        [EnumMember(Value = "Test Harness")]
        TestHarness = 0,

        [EnumMember(Value = "Web App")]
        WebApp = 1,

        [EnumMember(Value = "Power Apps App")]
        PowerAppsApp = 2,
    }
}
