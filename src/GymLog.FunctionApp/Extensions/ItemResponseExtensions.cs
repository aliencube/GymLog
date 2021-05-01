using System.Net;

using GymLog.FunctionApp.Traces;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for Cosmos DB records.
    /// </summary>
    public static class ItemResponseExtensions
    {
        /// <summary>
        /// Gets the <see cref="EventType"/> value from the HTTP status code value.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <returns>Returns the <see cref="EventType"/> value.</returns>
        public static EventType ToMessageEventType(this HttpStatusCode statusCode)
        {
            if (statusCode < HttpStatusCode.BadRequest)
            {
                return EventType.MessageProcessed;
            }

            return EventType.MessageNotProcessed;
        }
    }
}
