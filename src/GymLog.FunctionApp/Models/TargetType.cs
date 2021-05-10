using System.Runtime.Serialization;

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
        /// Identifies targetting the full body.
        /// </summary>
        [EnumMember(Value = "Full Body")]
        FullBody = 0,

        /// <summary>
        /// Identifies targetting the leg.
        /// </summary>
        Leg = 1,

        /// <summary>
        /// Identifies targetting the core.
        /// </summary>
        Core = 2,

        /// <summary>
        /// Identifies targetting the back.
        /// </summary>
        Back = 3,

        /// <summary>
        /// Identifies targetting the chest.
        /// </summary>
        Chest = 4,

        /// <summary>
        /// Identifies targetting the shoulder.
        /// </summary>
        Shoulder = 5,

        /// <summary>
        /// Identifies targetting cardio.
        /// </summary>
        Cardio = 6,
    }
}
