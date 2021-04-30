using System.Net;

using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GymLog.FunctionApp.Extensions
{
    public static partial class AzureResponseExtensions
    {
        public static LogLevel ToLogLevel(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return LogLevel.Information;
            }

            return LogLevel.Error;
        }

        public static EventStatusType ToEventStatusType(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return EventStatusType.Succeeded;
            }

            return EventStatusType.Failed;
        }

        public static string ToResponseMessage(this int statusCode, ObjectResult res)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return null;
            }

            return (string)res.Value;
        }
    }
}
