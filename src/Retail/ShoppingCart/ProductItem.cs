using System.Text.RegularExpressions;

namespace ShoppingCart;

public record ProductItem
{
    public ProductId ProductId { get; init; }
    public int Quantity { get; init; }

    private ProductItem(ProductId productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public static ProductItem Create(ProductId? productId, int? quantity)
    {
        if (productId is null || string.IsNullOrWhiteSpace(productId.Value))
            throw new ArgumentNullException(nameof(productId));

        return quantity switch
        {
            null => throw new ArgumentNullException(nameof(quantity)),
            <= 0 => throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be a positive number"),
            _ => new ProductItem(productId, quantity.Value)
        };
    }

    public static ProductItem Create(string? productId, int? quantity)
    {
        return Create(new ProductId(productId!), quantity);
    }

    public void Deconstruct(out string ProductId, out int Quantity)
    {
        ProductId = this.ProductId;
        Quantity = this.Quantity;
    }
}

public class ProductItems
{
    public static ProductItems Empty = new([]);

    public ProductItem[] Values { get; }

    private ProductItems(ProductItem[] values) => Values = values;

    public bool IsEmpty => Values.Length == 0;
    public int Length => Values.Length;

    public ProductItems Add(ProductItem productItem) => new(
        Values
            .Concat(new[] { productItem })
            .GroupBy(pi => pi.ProductId)
            .Select(group => group.Count() == 1
                ? group.First()
                : group.First() with { Quantity = group.Sum(pi => pi.Quantity)})
            .ToArray());

    public ProductItems Remove(ProductItem productItem) => new(
        Values
            .Select(pi => pi.ProductId == productItem.ProductId
                ? pi with { Quantity = pi.Quantity - productItem.Quantity }
                : pi)
            .Where(pi => pi.Quantity > 0)
            .ToArray());

}
