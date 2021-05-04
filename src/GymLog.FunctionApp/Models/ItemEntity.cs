using System;

using Azure;
using Azure.Data.Tables;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the item entity for Table Storage. This MUST be inherited.
    /// </summary>
    public abstract class ItemEntity : ITableEntity
    {
        /// <summary>
        /// Gets or sets the partition key. It's the correlation ID.
        /// </summary>
        public virtual string PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the row key. It's the event ID.
        /// </summary>
        public virtual string RowKey { get; set; }

        /// <summary>
        /// Gets or sets the user principal name (UPN).
        /// </summary>
        public virtual string Upn { get; set; }

        /// <summary>
        /// Gets or sets the correlation ID.
        /// </summary>
        public virtual Guid CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the span ID.
        /// </summary>
        public virtual Guid SpanId { get; set; }

        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        public virtual Guid EventId { get; set; }

        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public virtual string EventName { get; set; }

        /// <summary>
        /// Gets or sets the timestamp. It's systematically generated.
        /// </summary>
        public virtual DateTimeOffset? Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the e-tag. It's systematically generated.
        /// </summary>
        public virtual ETag ETag { get; set; }
    }
}
