using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoutineType
    {
        Back = 0,

        Chest = 1,

        Shoulder = 2,

        Core = 3,
    }
}
