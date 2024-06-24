using ShoppingCart.Products;

namespace ShoppingCart.Inventories;

public interface IInventoryChecker
{
    IReadOnlyList<ProductItem> Check(params ProductId[] productIds);
}
