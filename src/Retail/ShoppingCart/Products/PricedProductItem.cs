using ShoppingCart.Prices;

namespace ShoppingCart.Products;

public class PricedProductItem
{
    public ProductId ProductId => ProductItem.ProductId;
    public PriceId PriceId => PricedItem.PriceId;

    public decimal UnitPrice => PricedItem.Price;

    public ProductItem ProductItem { get; }
    public PricedItem PricedItem { get; }

    private PricedProductItem(ProductItem productItem, PricedItem pricedItem)
    {
        ProductItem = productItem;
        PricedItem = pricedItem;
    }

    public static PricedProductItem From(ProductItem productItem, PricedItem pricedItem)
    {
        return new PricedProductItem(productItem, pricedItem);
    }

    public static PricedProductItem From(string? productId, int? quantity, string? priceId, decimal? unitPrice)
    {
        return From(
            ProductItem.From(new ProductId(productId!), quantity),
            PricedItem.From(new PriceId(priceId!), unitPrice)
        );
    }

    public bool MatchesProductIdAndPriceId(PricedProductItem pricedProductItem) =>
        ProductId == pricedProductItem.ProductId &&
        PriceId == pricedProductItem.PriceId;

    public bool MatchesUnitPrice(PricedProductItem pricedProductItem) =>
        UnitPrice == pricedProductItem.UnitPrice;

    public PricedProductItem MergeWith(PricedProductItem pricedProductItem)
    {
        if (!MatchesProductIdAndPriceId(pricedProductItem))
            throw new ArgumentException("Product or price does not match.");

        return new PricedProductItem(ProductItem.MergeWith(pricedProductItem.ProductItem), PricedItem);
    }

    public PricedProductItem Subtract(PricedProductItem pricedProductItem)
    {
        if (!MatchesProductIdAndPriceId(pricedProductItem))
            throw new ArgumentException("Product or price does not match.");

        return new PricedProductItem(ProductItem.Subtract(pricedProductItem.ProductItem), PricedItem);
    }

    public bool HasEnough(int quantity) => ProductItem.HasEnough(quantity);

    public bool HasTheSameQuantity(PricedProductItem pricedProductItem) =>
        ProductItem.HasTheSameQuantity(pricedProductItem.ProductItem);
}

public class PricedProductItems
{
    public static PricedProductItems Empty = new([]);

    public PricedProductItem[] Values { get; }

    private PricedProductItems(PricedProductItem[] values)
    {
        Values = values;
    }

    public bool IsEmpty => Values.Length == 0;
    public int Length => Values.Length;
}
