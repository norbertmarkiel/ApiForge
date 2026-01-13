using ApiForge.Models;
using System.Text.Json;

namespace ApiForge.Runtime
{
    public class SchemaValidator
    {
        public SchemaValidationResult Validate(
            JsonElement payload,
            SchemaDefinition schema)
        {
            var errors = new List<string>();

            foreach (var field in schema.Fields)
            {
                if (!payload.TryGetProperty(field.Name, out var value))
                {
                    if (field.IsRequired)
                        errors.Add($"Missing field: {field.Name}");

                    continue;
                }

                if (!IsValidType(value, field.Type))
                {
                    errors.Add($"Invalid type for field: {field.Name}");
                }
            }

            return new SchemaValidationResult(errors);
        }

        private bool IsValidType(JsonElement value, FieldType type)
        {
            return type switch
            {
                FieldType.String => value.ValueKind == JsonValueKind.String,
                FieldType.Number => value.ValueKind == JsonValueKind.Number,
                FieldType.Boolean => value.ValueKind is JsonValueKind.True or JsonValueKind.False,
                FieldType.Timestamp => value.ValueKind == JsonValueKind.String
                                      && DateTime.TryParse(value.GetString(), out _),
                FieldType.Json => value.ValueKind is JsonValueKind.Object or JsonValueKind.Array,
                _ => false
            };
        }
    }
}
