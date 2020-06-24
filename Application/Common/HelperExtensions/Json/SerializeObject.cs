using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Common.HelperExtensions.Json
{
    //MORE: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
    public static class SerializeObject
    {
        public static string ToJson<T>(this T instance)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.WriteIndented = true;
            return JsonSerializer.Serialize(instance, options);
        }
    }
}
