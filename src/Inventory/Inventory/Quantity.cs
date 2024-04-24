using Eventuous;
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace Inventory;

public sealed record Quantity
{
    public int Value { get; internal init; } = 0;

    internal Quantity() { }

    public Quantity(int value)
    {
        if (int.IsNegative(value))
            throw new DomainException("Quantity value cannot be negative");

        Value = value;
    }

    public Quantity(string value)
    {
        bool wasParsed = int.TryParse(value, out int result);

        if (wasParsed is false)
            throw new DomainException("Could not parse string value into an integer");

        if (int.IsNegative(result))
            throw new DomainException("Quantity value cannot be negative");

        Value = result;
    }

    public Quantity Add(Quantity another) => Add(another.Value);

    public Quantity Add(int another) => new(Value + another);

    public bool HasSameValue(Quantity another) => Value == another.Value;

    public static implicit operator int(Quantity quantity) => quantity.Value;

    public static Quantity IsZero => new(0);
}
