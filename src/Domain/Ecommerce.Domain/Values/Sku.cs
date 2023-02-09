namespace Ecommerce.Domain.Values;

[ValueObject<string>(conversions: Conversions.NewtonsoftJson)]
[Instance("Unspecified", "")]
public partial struct Sku
{
    private static Validation Validate(string value) => string.IsNullOrWhiteSpace(value)
        ? Validation.Invalid("A Stock Keeping Unit (SKU) cannot be empty")
        : Validation.Ok;
}
