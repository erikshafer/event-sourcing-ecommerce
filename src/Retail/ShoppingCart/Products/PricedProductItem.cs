using ShoppingCart.Prices;

namespace ShoppingCart.Products;

public class PricedProductItem
{
    public ProductId ProductId => ProductItem.ProductId;
    public PriceId PriceId => PricedItem.PriceId;

    public int Quantity => ProductItem.Quantity;

    public decimal UnitPrice => PricedItem.Price;

    public decimal TotalPrice => Quantity * UnitPrice;

    public ProductItem ProductItem { get; }
    public PricedItem PricedItem { get; }

    private PricedProductItem(ProductItem productItem, PricedItem pricedItem)
    {
        ProductItem = productItem;
        PricedItem = pricedItem;
    }

    public static PricedProductItem Create(ProductItem productItem, PricedItem pricedItem)
    {
        return new PricedProductItem(productItem, pricedItem);
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
