using System;

using GymLog.FunctionApp.Traces;

using Microsoft.Extensions.Logging;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for logger.
    /// </summary>
    public static class LoggerExtensions
    {
        private static string template = "{logLevel}|{message}|{eventType}|{eventStatus}|{eventId}|{entityType}|{spanType}|{spanStatus}|{spanId}|{interfaceType}|{correlationId}|{clientRequestId}|{messageId}|{recordId}";

        /// <summary>
        /// Logs the given data.
        /// </summary>
        /// <typeparam name="T">Type of payload.</typeparam>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="logLevel"><see cref="ILogger"/> value.</param>
        /// <param name="payload">Payload to log.</param>
        /// <param name="eventType"><see cref="EventType"/> value.</param>
        /// <param name="eventStatus"><see cref="EventStatusType"/> value.</param>
        /// <param name="eventId">Event ID.</param>
        /// <param name="spanType"><see cref="SpanType"/> value.</param>
        /// <param name="spanStatus"><see cref="SpanStatusType"/> value.</param>
        /// <param name="spanId">Span ID.</param>
        /// <param name="interfaceType"><see cref="InterfaceType"/> value.</param>
        /// <param name="correlationId">Correlation ID.</param>
        /// <param name="clientRequestId">Client request ID from Table Storage transaction.</param>
        /// <param name="messageId">Message ID for Service Bus.</param>
        /// <param name="recordId">Record ID of Cosmos DB.</param>
        /// <param name="ex"><see cref="Exception"/> instance.</param>
        /// <param name="message">Log message.</param>
        public static void LogData<T>(this ILogger logger,
                                      LogLevel logLevel, T payload,
                                      EventType eventType, EventStatusType eventStatus, Guid eventId,
                                      SpanType spanType, SpanStatusType spanStatus, Guid spanId,
                                      InterfaceType interfaceType, Guid correlationId,
                                      string clientRequestId = null,
                                      string messageId = null,
                                      string recordId = null,
                                      Exception ex = null,
                                      string message = null)
        {
            var @event = new EventId((int)eventType, eventType.ToString());
            var entityType = payload.GetType().Name;

            if (ex == null)
            {
                logger.Log(logLevel, @event, template, logLevel, message, eventType, eventStatus, eventId, entityType, spanType, spanStatus, spanId, interfaceType, correlationId, clientRequestId, messageId, recordId);

                return;
            }

            logger.Log(logLevel, @event, ex, template, logLevel, message, eventType, eventStatus, eventId, entityType, spanType, spanStatus, spanId, interfaceType, correlationId, clientRequestId, messageId, recordId);
        }
    }
}
