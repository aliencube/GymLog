using System.Net;

using GymLog.FunctionApp.Traces;

using Microsoft.Extensions.Logging;

namespace GymLog.FunctionApp.Extensions
{
    public static class ItemResponseExtensions
    {
        public static LogLevel ToLogLevel(this HttpStatusCode statusCode)
        {
            return AzureResponseExtensions.ToLogLevel((int)statusCode);
        }

        public static EventStatusType ToEventStatusType(this HttpStatusCode statusCode)
        {
            return AzureResponseExtensions.ToEventStatusType((int)statusCode);
        }

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
