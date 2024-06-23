using ShoppingCart.Carts;

namespace ShoppingCart.Prices;

public class QuotePriceService : IQuotePriceService
{
    public IReadOnlyList<PricedProductItem> Quote(params ProductItem[] productItems)
    {
        if (productItems.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(productItems.Length));

        // TODO: access cache or make a gRPC call

        var rng = new Random();

        return productItems
            .Select(pi =>
                PricedProductItem.Create(pi, Math.Round(new decimal(rng.NextDouble() * 299),2)))
            .ToList();
    }
}
