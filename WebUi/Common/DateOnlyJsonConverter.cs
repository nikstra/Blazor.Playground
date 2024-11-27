using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebUi.Common;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.TryParseExact(
            reader.GetString(),
            DateFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var d)
            ? d
            : default;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
    }
}