using System;

using GymLog.FunctionApp.Models;

using Microsoft.AspNetCore.Mvc;

namespace GymLog.FunctionApp.ActionResults
{
    /// <summary>
    /// This represents the action result entity for generic HTTP errors.
    /// </summary>
    public class ErrorObjectResult
    {
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
        /// Gets or sets the error message.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public virtual int StatusCode { get; set; }

        /// <summary>
        /// Converts the <see cref="ErrorObjectResult"/> object to the <see cref="ObjectResult"/> object implicitly.
        /// </summary>
        /// <param name="instance"><see cref="ErrorObjectResult"/> object.</param>
        /// <return>Returns the <see cref="ObjectResult"/> object.</return>
        public static implicit operator ObjectResult(ErrorObjectResult instance)
        {
            var message = new ErrorResponseMessage()
            {
                    CorrelationId = instance.CorrelationId,
                    SpanId = instance.SpanId,
                    EventId = instance.EventId,
                    Message = instance.Message,
            };
            var result = new ObjectResult(message) { StatusCode = instance.StatusCode };

            return result;
        }
    }
}
