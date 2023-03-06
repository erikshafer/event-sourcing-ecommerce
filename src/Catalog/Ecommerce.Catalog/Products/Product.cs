using Ecommerce.Catalog.Brands;
using Ecommerce.Catalog.Categories;
using Ecommerce.Catalog.StockKeepingUnits;
using Ecommerce.Core.Aggregates;
using Ecommerce.Core.Exceptions;
using Ecommerce.Core.Extensions;
using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.Products;

public sealed class Product : Aggregate
{
    public string Sku { get; private set; } = default!;

    public Guid BrandId { get; private set; }

    public Guid CategoryId { get; private set; }

    public ProductStatus Status { get; private set; }

    public IList<Tag> Tags { get; private set; } = default!;

    public Product()
    {
    }

    public Product(ProductDrafted @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;
        BrandId = @event.BrandId;
        CategoryId = @event.CategoryId;

        Tags = new List<Tag>();
        Status = ProductStatus.Drafted;
    }

    public async Task<bool> ValidateSku(ISkuValidatorService validator)
    {
        var alreadyExists = await validator.AlreadyExists(Sku);
        return alreadyExists;
    }

    public void Apply(TagsListed @event)
    {
        Tags.ClearAndReplace(@event.Tags);
    }

    public async Task<bool> ValidateBrand(IBrandValidatorService validator)
    {
        var alreadyExists = await validator.AlreadyExists(Sku);
        return alreadyExists;
    }

    public void Apply(BrandAdjusted @event)
    {
        BrandId = @event.BrandId;
    }

    public async Task<bool> ValidateCategory(ICategoryValidatorService validator)
    {
        var alreadyExists = await validator.AlreadyExists(Sku);
        return alreadyExists;
    }

    public void Apply(CategoryAdjusted @event)
    {
        CategoryId = @event.CategoryId;
    }

    public void Confirm()
    {
        if (Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(Id, nameof(Confirm));

        var @event = new ProductConfirmed(Id);

        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(ProductConfirmed @event)
    {
        Status = ProductStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(Id, nameof(Cancel));

        var @event = new ProductCancelled(Id);

        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(ProductCancelled @event)
    {
        Status = ProductStatus.Cancelled;
    }
}
