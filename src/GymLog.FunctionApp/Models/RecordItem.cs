using System;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the record item entity. This MUST be inherited.
    /// </summary>
    public abstract class RecordItem
    {
        /// <summary>
        /// Gets or sets the entity record ID.
        /// </summary>
        [JsonProperty("id")]
        public virtual Guid EntityId { get; set; }

        /// <summary>
        /// Gets or sets the item type of the record.
        /// </summary>
        [JsonProperty("itemType")]
        public virtual RecordItemType ItemType { get; set; }
    }
}
