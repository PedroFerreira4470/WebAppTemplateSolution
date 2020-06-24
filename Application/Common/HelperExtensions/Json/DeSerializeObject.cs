using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Common.HelperExtensions.Json
{
    //MORE: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
    public static class DeserializeObject
    {
        public static T To<T>(this string json)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
