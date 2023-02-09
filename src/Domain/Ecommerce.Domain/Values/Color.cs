namespace Ecommerce.Domain.Values;

[ValueObject<string>(conversions: Conversions.NewtonsoftJson)]
[Instance("Unspecified", "")]
public partial struct Color
{
    private static Validation Validate(string value) => string.IsNullOrWhiteSpace(value)
        ? Validation.Invalid("A color cannot be empty")
        : Validation.Ok;
}
