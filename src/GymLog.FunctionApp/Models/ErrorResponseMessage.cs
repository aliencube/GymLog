using Newtonsoft.Json;

namespace GymLog.FunctionApp.Models
{
    public class ErrorResponseMessage : ResponseMessage
    {
        [JsonProperty("message")]
        public virtual string Message { get; set; }
    }
}
