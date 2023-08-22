namespace Catalog.Products;

public record ProductName
{
    public string Value { get; } = default!;

    private ProductName() {}

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Name must not be empty");

        if (value.Length > 100)
            throw new InvalidCastException("Name exceeds 100 character limit");

        Value = value;
    }
}
