using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This specifies the weight unit.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WeightUnitType
    {
        /// <summary>
        /// Identifies KGs.
        /// </summary>
        [EnumMember(Value = "kg")]
        Kg = 0,

        /// <summary>
        /// Identifies Pounds.
        /// </summary>
        [EnumMember(Value = "lbs")]
        Lbs = 1,
    }
}
