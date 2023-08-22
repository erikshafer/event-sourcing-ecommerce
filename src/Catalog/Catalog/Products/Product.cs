using Ecommerce.Core.Aggregates;

namespace Catalog.Products;

public sealed class Product : Aggregate
{
    public string Sku { get; private set; } = default!;

    public ProductStatus Status { get; private set; }

    public ProductName Name { get; private set; } = default!;

    public ProductWeightDimensions WeightDimensions { get; private set; } = default!;

    public ProductImageUrl ImageUrl { get; private set; } = default!;

    public ProductBrand Brand { get; private set; } = default!;

    public Product()
    {
    }

    public Product(ProductDrafted @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    private Product(Guid productId, string sku)
    {
        var @event = new ProductDrafted(productId, sku);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;

        Status = ProductStatus.Drafted;
    }

    public void DefineBrand(ProductBrand brand)
    {
        var @event = new ProductBrandDefined(Id, brand.BrandId, brand.BrandName);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(ProductBrandDefined @event)
    {
        Brand = new ProductBrand(@event.BrandId, @event.BrandName);
    }

    public void DefineImageUrl(ProductImageUrl imageUrl)
    {
        var @event = new ProductImageUrlDefined(Id, imageUrl.Value);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(ProductImageUrlDefined @event)
    {
        ImageUrl = new ProductImageUrl(@event.ImageUrl);
    }

    public void DefineProductWeightDimensions(ProductWeightDimensions weightDimensions)
    {
        bool isTheSame = WeightDimensions.Matches(weightDimensions);
        if (isTheSame)
            throw new InvalidOperationException("All values found to be the same");

        var (weight, height, length, depth) = weightDimensions;

        var @event = new ProductWeightDimensionsDefined(weight, height, length, depth);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(ProductWeightDimensionsDefined @event)
    {
        var (weight, height, length, depth) = @event;
        WeightDimensions = new ProductWeightDimensions(weight, height, length, depth);
    }
}

public record ProductWeightDimensionsDefined(float Weight, float Height, float Length, float Depth);
