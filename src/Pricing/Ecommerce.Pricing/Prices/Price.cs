using Ecommerce.Core.Aggregates;

namespace Ecommerce.Pricing.Prices;

public sealed class Price : Aggregate
{
    public ProductId ProductId { get; private set; }

    public string Sku { get; private set; } = default!;

    public decimal Amount { get; set; } = decimal.Zero;

    public Price()
    {
    }

    public Price(PriceId priceId, ProductId productId, string sku, decimal amount)
    {
        Id = priceId.Value;
        ProductId = productId;
        Sku = sku;
        Amount = amount;
    }
}
