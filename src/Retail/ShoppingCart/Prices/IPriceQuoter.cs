using ShoppingCart.Products;

namespace ShoppingCart.Prices;

public interface IPriceQuoter
{
    IReadOnlyList<PricedProductItem> Quote(params ProductItem[] productItems);
    PricedProductItem Quote(ProductId produceId);
}
