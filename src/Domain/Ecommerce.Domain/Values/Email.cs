namespace Ecommerce.Domain.Values;

[ValueObject<string>(conversions: Conversions.NewtonsoftJson)]
[Instance("Unspecified", "")]
public partial struct Email
{
    private static Validation Validate(string value) => string.IsNullOrWhiteSpace(value)
        ? Validation.Invalid("An email cannot be empty")
        : Validation.Ok;
}
