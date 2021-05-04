using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This specifies the record item type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RecordItemType
    {
        /// <summary>
        /// Identifies the routine.
        /// </summary>
        Routine,
    }
}
