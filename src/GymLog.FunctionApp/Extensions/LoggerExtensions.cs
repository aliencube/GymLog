using System;

using GymLog.FunctionApp.Traces;

using Microsoft.Extensions.Logging;

namespace GymLog.FunctionApp.Extensions
{
    public static class LoggerExtensions
    {
        private static string template = "{logLevel}|{message}|{eventType}|{eventStatus}|{eventId}|{entityType}|{spanType}|{spanStatus}|{spanId}|{interfaceType}|{correlationId}|{clientRequestId}|{messageId}|{recordId}";

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
