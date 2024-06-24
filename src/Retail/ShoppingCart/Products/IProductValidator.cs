namespace ShoppingCart.Products;

public interface IProductValidator
{
    IReadOnlyList<(ProductId productId, bool isValid)> Quote(params ProductId[] productIds);
}
