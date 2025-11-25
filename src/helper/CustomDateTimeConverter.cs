using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api_orders.Converters;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private static readonly string[] AcceptedFormats =
    {
        "M/d/yyyy h:mm:ss tt",
        "MM/dd/yyyy hh:mm:ss tt",
        "M/d/yyyy h:mm tt",
        "MM/dd/yyyy",
        "dd/MM/yyyy",
        "dd/MM/yyyy HH:mm:ss",
        "yyyy-MM-dd",
        "yyyy-MM-ddTHH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss.fffZ"
    };

    private const string DefaultOutputFormat = "dd/MM/yyyy HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateString = reader.GetString();

        if (string.IsNullOrWhiteSpace(dateString))
            return default;
        
        
        if (DateTime.TryParseExact(
                dateString,
                AcceptedFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out DateTime parsed))
        {
            return parsed;
        }

        if (DateTime.TryParse(
                dateString,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal,
                out parsed))
        {
            return parsed;
        }

        throw new JsonException($"Não foi possível converter a data '{dateString}'.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DefaultOutputFormat, CultureInfo.InvariantCulture));
    }
}
