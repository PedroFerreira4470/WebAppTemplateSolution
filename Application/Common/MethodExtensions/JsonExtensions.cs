using System.Text.Json;

namespace Application.Common.MethodExtensions
{
    public static class JsonExtensions
    {
        //MORE: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
        public static T DeserializeTo<T>(this string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static string SerializeToJson<T>(this T instance)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            //options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return JsonSerializer.Serialize(instance, options);
        }


    }
}
