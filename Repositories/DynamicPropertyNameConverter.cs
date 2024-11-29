using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TaskManagement.API.Repositories
{
    public class DynamicPropertyNameConverter : JsonConverter
    {
        private readonly Type _type;

        // Constructor to accept the type of the class dynamically
        public DynamicPropertyNameConverter(Type type)
        {
            _type = type;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _type; // Only apply to the specific type passed
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject obj = new JObject();
            var className = _type.Name; // Get the class name dynamically (e.g., "UserDetails")
            obj.Add(className, JToken.FromObject(value)); // Add dynamic property with the class name
            obj.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
