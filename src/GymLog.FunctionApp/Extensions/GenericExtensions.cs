using Newtonsoft.Json;

namespace GymLog.FunctionApp.Extensions
{
    public static class GenericExtensions
    {
        public static string ToJson<T>(this T instance, JsonSerializerSettings serializerSettings = null)
        {
            if (serializerSettings == null)
            {
                serializerSettings = new JsonSerializerSettings();
            }

            var serialised = JsonConvert.SerializeObject(instance, serializerSettings);

            return serialised;
        }

        public static T FromJson<T>(this string serialised, JsonSerializerSettings serializerSettings = null)
        {
            if (serializerSettings == null)
            {
                serializerSettings = new JsonSerializerSettings();
            }

            var instance = JsonConvert.DeserializeObject<T>(serialised, serializerSettings);

            return instance;
        }
    }
}
