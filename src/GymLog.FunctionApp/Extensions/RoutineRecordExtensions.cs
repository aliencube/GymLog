using System;

using GymLog.FunctionApp.Models;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="RoutineRecordItem"/> class.
    /// </summary>
    public static class RoutineRecordItemExtensions
    {
        /// <summary>
        /// Sets the entity ID in a fluent way.
        /// </summary>
        /// <param name="entityId">Entity ID.</param>
        /// <returns>Returns the <see cref="RoutineRecordItem"/> object.</returns>
        public static RoutineRecordItem WithEntityId(this RoutineRecordItem value, Guid entityId)
        {
            value.EntityId = entityId;

            return value;
        }

        /// <summary>
        /// Sets the timestamp in a fluent way.
        /// </summary>
        /// <param name="timestamp">Timestamp value.</param>
        /// <returns>Returns the <see cref="RoutineRecordItem"/> object.</returns>
        public static RoutineRecordItem WithTimestamp(this RoutineRecordItem value, DateTimeOffset timestamp)
        {
            value.Timestamp = timestamp;;

            return value;
        }
    }
}
