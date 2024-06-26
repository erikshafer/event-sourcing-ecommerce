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
                PricedProductItem.From(
                    pi,
                    PricedItem.From(
                        new PriceId(Guid.NewGuid().ToString()),
                        Math.Round(new decimal(rng.NextDouble() * 199),2))
                )
            )
            .ToList();
    }

    public PricedProductItem Quote(ProductId productId)
    {
        var productItem = ProductItem.From(productId, 1);
        var rng = new Random();
        var priceId = new PriceId(Guid.NewGuid().ToString());
        var price = Math.Round(new decimal(rng.NextDouble() * 199), 2);
        var pricedItem = PricedItem.From(priceId, price);
        return PricedProductItem.From(productItem, pricedItem);
    }
}
