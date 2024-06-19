namespace Catalog.Prices;

public record BundledPricing
{
    public bool Available { get; init; } = false;
    public int Quantity { get; init; } = 0;
    public Money Price { get; init; } = null!;

    internal BundledPricing() { }

    public BundledPricing(int quantity, Money price)
    {
        if (quantity > 0)
            Available = true;
        Quantity = quantity;
        Price = price;
    }

    public static BundledPricing None()
    {
        return new BundledPricing(0, Money.Zero(Constants.Currencies.USD));
    }
}
