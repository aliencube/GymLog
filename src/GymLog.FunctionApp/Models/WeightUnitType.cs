using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WeightUnitType
    {
        [EnumMember(Value = "kg")]
        Kg = 0,

        [EnumMember(Value = "lbs")]
        Lbs = 1,
    }
}
