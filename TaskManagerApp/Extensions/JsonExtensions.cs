using Newtonsoft.Json;

namespace TaskManagerApp.Extensions
{
    public static class JsonExtensions
    {
        public static TReturn DeserializeJson<TReturn>(this string str)
        {
            return JsonConvert.DeserializeObject<TReturn>(str);
        }

        public static string ToJson(this object model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}