using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This specifies the type type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TargetType
    {
        /// <summary>
        /// Identifies targetting the back.
        /// </summary>
        Back = 0,

        /// <summary>
        /// Identifies targetting the chest.
        /// </summary>
        Chest = 1,

        /// <summary>
        /// Identifies targetting the shoulder.
        /// </summary>
        Shoulder = 2,

        /// <summary>
        /// Identifies targetting the core.
        /// </summary>
        Core = 3,
    }
}
