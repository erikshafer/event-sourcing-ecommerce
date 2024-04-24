using Eventuous;

namespace Inventory;

public sealed record Sku
{
    public string Value { get; internal init; } = string.Empty;

    internal Sku() { }

    public Sku(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("SKU value cannot be empty");
    }

    public bool HasSameValue(Sku another) => Value == another.Value;

    public static implicit operator string(Sku sku) => sku.Value;
}
