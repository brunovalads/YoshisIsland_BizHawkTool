using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YoshisIsland_BizHawkTool
{
    internal class ToolOptionsConverter : JsonConverter<ToolOptions>
    {
        public override ToolOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;

            ToolOptions instance = ToolOptions.Instance;

            PropertyInfo[] properties = typeof(ToolOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite && doc.RootElement.TryGetProperty(property.Name, out JsonElement jsonElement))
                {
                    object? value = JsonSerializer.Deserialize(jsonElement.GetRawText(), property.PropertyType, options);
                    property.SetValue(instance, value);
                }
            }

            return instance;
        }

        public override void Write(Utf8JsonWriter writer, ToolOptions value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            PropertyInfo[] properties = typeof(ToolOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead)
                {
                    writer.WritePropertyName(property.Name);
                    object propertyValue = property.GetValue(value);
                    JsonSerializer.Serialize(writer, propertyValue, options);
                }
            }

            writer.WriteEndObject();
        }
    }
}