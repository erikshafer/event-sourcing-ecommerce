namespace Ecommerce.Domain.Values;

[ValueObject<string>(conversions: Conversions.NewtonsoftJson)]
[Instance("Unspecified", "")]
public partial struct Tag
{
    private static Validation Validate(string value) => string.IsNullOrWhiteSpace(value)
        ? Validation.Invalid("A tag cannot be empty")
        : Validation.Ok;
}
