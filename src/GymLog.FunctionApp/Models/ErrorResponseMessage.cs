using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    /// <summary>
    /// This represents the response message entity for errors.
    /// </summary>
    public class ErrorResponseMessage : ResponseMessage
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("message")]
        public virtual string Message { get; set; }
    }
}
