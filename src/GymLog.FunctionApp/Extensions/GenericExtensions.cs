using Newtonsoft.Json;

namespace GymLog.FunctionApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for generic.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Serialises the given object into JSON string.
        /// </summary>
        /// <typeparam name="T">Type of instance to serialise.</typeparam>
        /// <param name="instance">Instance object to serialise.</param>
        /// <param name="serializerSettings"><see cref="JsonSerializerSettings"/> object.</param>
        /// <returns>Returns the JSON seiralised string.</returns>
        public static string ToJson<T>(this T instance, JsonSerializerSettings serializerSettings = null)
        {
            if (serializerSettings == null)
            {
                serializerSettings = new JsonSerializerSettings();
            }

            var serialised = JsonConvert.SerializeObject(instance, serializerSettings);

            return serialised;
        }

        /// <summary>
        /// Deserialises the string into the given type.
        /// </summary>
        /// <typeparam name="T">Type of instance to deserialise.</typeparam>
        /// <param name="serialised">JSON serialised string.</param>
        /// <param name="serializerSettings"><see cref="JsonSerializerSettings"/> object.</param>
        /// <returns>Returns the deserialised object.</returns>
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
