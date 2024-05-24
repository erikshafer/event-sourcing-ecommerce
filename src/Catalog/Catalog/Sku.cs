using Eventuous;

namespace Catalog;

public record Sku
{
    public string Value { get; internal init; } = string.Empty;

    internal Sku() { }

    public Sku(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("SKU value cannot be empty");

        Value = value;
    }

    public bool HasSameValue(Sku another) => Value == another.Value;

    public static implicit operator string(Sku sku) => sku.Value;
}
