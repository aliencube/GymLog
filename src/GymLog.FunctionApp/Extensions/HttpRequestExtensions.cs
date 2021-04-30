using System.IO;
using System.Threading.Tasks;

using GymLog.FunctionApp.Models;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<RoutineRequestMessage> ToRoutineRequestMessageAsync(this HttpRequest req)
        {
            var serialised = default(string);
            using (var reader = new StreamReader(req.Body))
            {
                serialised = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            var payload = JsonConvert.DeserializeObject<RoutineRequestMessage>(serialised);

            return payload;
        }

        public static async Task<ExerciseRequestMessage> ToExerciseRequestMessageAsync(this HttpRequest req)
        {
            var serialised = default(string);
            using (var reader = new StreamReader(req.Body))
            {
                serialised = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            var payload = JsonConvert.DeserializeObject<ExerciseRequestMessage>(serialised);

            return payload;
        }

        public static async Task<RecordRequestMessage> ToRecordRequestMessageAsync(this HttpRequest req)
        {
            var serialised = default(string);
            using (var reader = new StreamReader(req.Body))
            {
                serialised = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            var payload = JsonConvert.DeserializeObject<RecordRequestMessage>(serialised);

            return payload;
        }
    }
}
