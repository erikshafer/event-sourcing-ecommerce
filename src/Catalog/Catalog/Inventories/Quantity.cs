using Microsoft.CodeAnalysis.Emit;

namespace Catalog.Inventories;

public record Quantity(int Value)
{
    public const int None = 0;
    public static Quantity Zero() => new(0);

    public bool IsZero() =>
        Value == None;

    public bool IsZeroOrLess() =>
        Value <= None;

    public Quantity CalculateDelta(Quantity quantity) =>
        new(this.Value + quantity.Value);

}
