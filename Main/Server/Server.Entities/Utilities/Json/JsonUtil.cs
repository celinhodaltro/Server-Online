
using System.Text.Json;

namespace Server.Util;
public static class JsonConverter
{
    public static string ToJson<T>(this T obj)
    {
        if (obj == null)
        {
            return "{}";
        }


        return JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
}