using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This specifies the routine type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoutineType
    {
        /// <summary>
        /// Identifies the back routine.
        /// </summary>
        Back = 0,

        /// <summary>
        /// Identifies the chest routine.
        /// </summary>
        Chest = 1,

        /// <summary>
        /// Identifies the shoulder routine.
        /// </summary>
        Shoulder = 2,

        /// <summary>
        /// Identifies the core routine.
        /// </summary>
        Core = 3,
    }
}
