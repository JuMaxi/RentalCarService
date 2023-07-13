using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RentalCarService
{
    public class DayOfWeekConverter : JsonConverter<DayOfWeek>
    {
        public override DayOfWeek Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), value);
        }

        public override void Write(Utf8JsonWriter writer, DayOfWeek value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
