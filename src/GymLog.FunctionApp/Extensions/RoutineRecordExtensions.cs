using System;

using GymLog.FunctionApp.Models;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="RoutineRecord"/> class.
    /// </summary>
    public static class RoutineRecordExtensions
    {
        /// <summary>
        /// Sets the entity ID in a fluent way.
        /// </summary>
        /// <param name="entityId">Entity ID.</param>
        /// <returns>Returns the <see cref="RoutineRecord"/> object.</returns>
        public static RoutineRecord WithEntityId(this RoutineRecord value, Guid entityId)
        {
            value.EntityId = entityId;

            return value;
        }

        /// <summary>
        /// Sets the entity ID in a fluent way.
        /// </summary>
        /// <param name="timestamp">Timestamp value.</param>
        /// <returns>Returns the <see cref="RoutineRecord"/> object.</returns>
        public static RoutineRecord WithTimestamp(this RoutineRecord value, DateTimeOffset timestamp)
        {
            value.Timestamp = timestamp;;

            return value;
        }
    }
}
