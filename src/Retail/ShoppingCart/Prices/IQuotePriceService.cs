using ShoppingCart.Carts;

namespace ShoppingCart.Prices;

public interface IQuotePriceService
{
    IReadOnlyList<PricedProductItem> Quote(params ProductItem[] productItems);
}
