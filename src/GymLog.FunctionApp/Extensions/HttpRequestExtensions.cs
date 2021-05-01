using System.IO;
using System.Threading.Tasks;

using GymLog.FunctionApp.Models;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="HttpRequest"/> class.
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Builds the <see cref="RoutineRequestMessage"/> object from HTTP request.
        /// </summary>
        /// <typeparam name="T">Type to deserialise.</typeparam>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <returns>Returns the <see cref="RoutineRequestMessage"/> object.</returns>
        public static async Task<T> ToRequestMessageAsync<T>(this HttpRequest req) where T : RequestMessage
        {
            var serialised = default(string);
            using (var reader = new StreamReader(req.Body))
            {
                serialised = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            var payload = JsonConvert.DeserializeObject<T>(serialised);

            return payload;
        }
    }
}
