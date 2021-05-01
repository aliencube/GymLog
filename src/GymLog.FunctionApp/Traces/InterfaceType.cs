using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    /// <summary>
    /// This specifies the interface type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InterfaceType
    {
        /// <summary>
        /// Identifies the test harness.
        /// </summary>
        [EnumMember(Value = "Test Harness")]
        TestHarness = 0,

        /// <summary>
        /// Identifies the web app.
        /// </summary>
        [EnumMember(Value = "Web App")]
        WebApp = 1,

        /// <summary>
        /// Identifies the power apps app.
        /// </summary>
        [EnumMember(Value = "Power Apps App")]
        PowerAppsApp = 2,
    }
}
