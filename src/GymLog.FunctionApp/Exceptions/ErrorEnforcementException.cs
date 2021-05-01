using System;
using System.Runtime.Serialization;

namespace GymLog.FunctionApp.Exceptions
{
    /// <summary>
    /// This represents the exception entity for custom error enforcement.
    /// </summary>
    public class ErrorEnforcementException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEnforcementException"/> class.
        /// </summary>
        public ErrorEnforcementException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEnforcementException"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public ErrorEnforcementException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEnforcementException"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner <see cref="Exception"/> instance.</param>
        public ErrorEnforcementException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEnforcementException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/> instance.</param>
        /// <param name="context"><see cref="StreamingContext"/> instance.</param>
        protected ErrorEnforcementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
   }
}
