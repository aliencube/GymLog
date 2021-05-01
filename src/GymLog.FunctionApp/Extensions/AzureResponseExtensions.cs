using System.Net;

using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="Response"/> class.
    /// </summary>
    public static partial class AzureResponseExtensions
    {
        /// <summary>
        /// Gets the <see cref="LogLevel"/> value based on the HTTP status code.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <returns>Returns the <see cref="LogLevel"/> value.</returns>
        public static LogLevel ToLogLevel(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return LogLevel.Information;
            }

            return LogLevel.Error;
        }

        /// <summary>
        /// Gets the <see cref="EventStatusType"/> value based on the HTTP status code.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <returns>Returns the <see cref="EventStatusType"/> value.</returns>
        public static EventStatusType ToEventStatusType(this int statusCode)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
            {
                return EventStatusType.Succeeded;
            }

            return EventStatusType.Failed;
        }

        /// <summary>
        /// Gets the response message based on the HTTP status code.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="res"><see cref="ObjectResult"/> value.</param>
        /// <returns>Returns the response message.</returns>
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
