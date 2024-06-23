using ShoppingCart.Carts;

namespace ShoppingCart.Inventories;

public interface ICheckInventoryService
{
    IReadOnlyList<ProductItem> Check(params ProductId[] productIds);
}
