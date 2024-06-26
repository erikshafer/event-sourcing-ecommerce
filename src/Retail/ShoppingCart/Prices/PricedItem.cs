namespace ShoppingCart.Prices;

public record PricedItem
{
    public PriceId PriceId { get; }
    public decimal Price { get; }

    private PricedItem(PriceId priceId, decimal price)
    {
        PriceId = priceId;
        Price = price;
    }

    public static PricedItem From(PriceId? priceId, decimal? price)
    {
        if (priceId is null || string.IsNullOrWhiteSpace(priceId.Value))
            throw new ArgumentNullException(nameof(priceId));

        return price switch
        {
            null => throw new ArgumentNullException(nameof(price)),
            <= 0 => throw new ArgumentOutOfRangeException(nameof(price), "Must be a positive number"),
            _ => new PricedItem(priceId, price.Value)
        };
    }
}
