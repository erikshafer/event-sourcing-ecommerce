using ShoppingCart.Products;

namespace ShoppingCart.Prices;

public class PriceQuoter : IPriceQuoter
{
    public IReadOnlyList<PricedProductItem> Quote(params ProductItem[] productItems)
    {
        if (productItems.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(productItems.Length));

        // TODO: access cache or make a gRPC call

        var rng = new Random();

        return productItems
            .Select(pi =>
                PricedProductItem.Create(
                    pi,
                    PricedItem.From(
                        new PriceId(Guid.NewGuid().ToString()),
                        Math.Round(new decimal(rng.NextDouble() * 199),2))
                )
            )
            .ToList();
    }
}
