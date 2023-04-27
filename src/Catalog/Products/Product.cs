using Ecommerce.Core.Aggregates;
using Ecommerce.Core.Exceptions;

namespace Products;

public sealed class Product : Aggregate
{
    public string Sku { get; private set; } = default!;

    public Guid BrandId { get; private set; }

    public Guid CategoryId { get; private set; }

    public ProductStatus Status { get; private set; }

    public Product()
    {
    }

    public Product(ProductDrafted @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    private Product(Guid productId, string sku, Guid brandId, Guid categoryId)
    {
        var @event = new ProductDrafted(productId, sku, brandId, categoryId);

        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;
        BrandId = @event.BrandId;
        // TODO: Category

        Status = ProductStatus.Drafted;
    }

    public async Task<bool> ValidateSku(ISkuValidatorService validator)
    {
        var alreadyExists = await validator.Exists(Sku);
        return alreadyExists;
    }

    public void Apply(BrandAdjusted @event)
    {
        BrandId = @event.BrandId;
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
