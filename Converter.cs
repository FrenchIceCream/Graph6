using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Graph6
{
    public class Converter : JsonConverter<Shape>
    {
        public override Shape? ReadJson(JsonReader reader, Type objectType, Shape? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jsonObject = JObject.Load(reader);
            var points = jsonObject["points"].ToObject<List<MyPoint>>();
            var edges = jsonObject["edges"].ToObject<List<(int, int)>>();
            return new Shape(points, edges);
        }

        public override void WriteJson(JsonWriter writer, Shape? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var jsonObject = new JObject
            {
                { "points", JToken.FromObject(value.Points, serializer) },
                { "edges", JToken.FromObject(value.Edges, serializer) }
            };
            jsonObject.WriteTo(writer);
        }
    }

}
