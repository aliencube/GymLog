using System.Net;

using GymLog.FunctionApp.Models;

using Microsoft.AspNetCore.Mvc;

namespace GymLog.FunctionApp.ActionResults
{
    /// <summary>
    /// This represents the action result entity for internal server errors (500).
    /// </summary>
    public class InternalServerErrorObjectResult : ErrorObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorObjectResult"/> class.
        /// </summary>
        public InternalServerErrorObjectResult()
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Converts the <see cref="InternalServerErrorObjectResult"/> object to the <see cref="ObjectResult"/> object implicitly.
        /// </summary>
        /// <param name="instance"><see cref="InternalServerErrorObjectResult"/> object.</param>
        /// <returns>Returns the <see cref="ObjectResult"/> object.</returns>
        public static implicit operator ObjectResult(InternalServerErrorObjectResult instance)
        {
            var message = new ErrorResponseMessage()
            {
                    CorrelationId = instance.CorrelationId,
                    Interface = instance.Interface,
                    SpanId = instance.SpanId,
                    EventId = instance.EventId,
                    Message = instance.Message,
            };
            var result = new ObjectResult(message) { StatusCode = instance.StatusCode };

            return result;
        }
    }
}
