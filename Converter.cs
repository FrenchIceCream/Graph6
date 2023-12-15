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
            var faces = jsonObject["faces"].ToObject<List<Face>>();
            return new Shape(points, faces);
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
                    { "faces", JToken.FromObject(value.Faces, serializer) }
                };
            jsonObject.WriteTo(writer);
        }
    }
}
