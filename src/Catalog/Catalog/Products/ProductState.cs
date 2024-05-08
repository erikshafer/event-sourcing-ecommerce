using Eventuous;

using static Catalog.Products.ProductEvents;

namespace Catalog.Products;

public record ProductState : State<ProductState>
{
    public ProductId Id { get; init; } = null!;
    public ProductStatus Status { get; init; } = ProductStatus.Unset;
    public string Name { get; init; } = null!;
    public Sku Sku { get; init; } = null!;

    public ProductState()
    {
        On<V1.ProductInitialized>(HandleInitialized);
        On<V1.ProductConfirmed>(HandleConfirmed);
        On<V1.ProductDeprecated>(HandleDeprecated);
        On<V1.ProductCancelled>(HandleCancelled);
    }

    private static ProductState HandleInitialized(ProductState state, V1.ProductInitialized @event) => state with
    {
        Id = new ProductId(@event.ProductId),
        Status = ProductStatus.Initialized,
        Name = @event.Name,
        Sku = new Sku(@event.Sku)
    };

    private static ProductState HandleConfirmed(ProductState state, V1.ProductConfirmed @event) => state with
    {
        Status = ProductStatus.Confirmed
    };

    private static ProductState HandleDeprecated(ProductState state, V1.ProductDeprecated @event) => state with
    {
        Status = ProductStatus.Deprecated
    };

    private static ProductState HandleCancelled(ProductState state, V1.ProductCancelled @event) => state with
    {
        Status = ProductStatus.Cancelled
    };
}
