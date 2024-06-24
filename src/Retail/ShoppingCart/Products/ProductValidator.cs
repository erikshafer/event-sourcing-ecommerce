namespace ShoppingCart.Products;

public class ProductValidator : IProductValidator
{
    public IReadOnlyList<(ProductId productId, bool isValid)> Quote(params ProductId[] productIds)
    {
        if (productIds.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(productIds.Length));

        // TODO: access cache or make a gRPC call

        return productIds
            .Select(pi => new ValueTuple<ProductId, bool>(pi, true))
            .ToList();
    }
}
