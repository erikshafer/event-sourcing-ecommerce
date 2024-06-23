using ShoppingCart.Carts;

namespace ShoppingCart.Inventories;

public class CheckInventoryService : ICheckInventoryService
{
    public IReadOnlyList<ProductItem> Check(params ProductId[] productIds)
    {
        if (productIds.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(productIds.Length));

        // TODO: access cache or make a gRPC call

        var rng = new Random();

        return productIds
            .Select(pi => ProductItem.From(pi, rng.Next(1, 50)))
            .ToList();
    }
}
