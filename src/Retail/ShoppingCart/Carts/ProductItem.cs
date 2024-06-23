namespace ShoppingCart.Carts;

public record ProductItem
{
    public ProductId ProductId { get; init; }
    public int Quantity { get; init; }

    private ProductItem(ProductId productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public static ProductItem From(ProductId? productId, int? quantity)
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

    public static ProductItem From(string? productId, int? quantity)
    {
        return From(new ProductId(productId!), quantity);
    }

    public static ProductItem From(Guid? productId, int? quantity)
    {
        return From(new ProductId(productId.ToString()!), quantity);
    }

    public ProductItem MergeWith(ProductItem productItem)
    {
        if (!MatchesProduct(productItem))
            throw new ArgumentException("Product does not match.");

        return From(ProductId, Quantity + productItem.Quantity);
    }

    public ProductItem Subtract(ProductItem productItem)
    {
        if (!MatchesProduct(productItem))
            throw new ArgumentException("Product does not match.");

        return From(ProductId, Quantity - productItem.Quantity);
    }

    public bool MatchesProduct(ProductItem productItem) =>
        ProductId == productItem.ProductId;

    public bool HasEnough(int quantity) => Quantity >= quantity;

    public bool HasTheSameQuantity(ProductItem productItem) =>
        Quantity == productItem.Quantity;

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
