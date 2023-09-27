namespace Catalog.Products;

public sealed record ProductInitialized(
    ProductId ProductId,
    Sku Sku,
    string Name
);

public sealed record ProductConfirmed(
    ProductId ProductId,
    string ConfirmedBy
);

public sealed record ProductDeprecated(
    ProductId ProductId,
    string DeprecatedBy,
    string Reason
);

public sealed record ProductCancelled(
    ProductId ProductId,
    string CancelledBy,
    string Reason
);

public enum ProductStatus
{
    Initialized = 1,
    Confirmed = 2,
    Cancelled = 4,
    Deprecated = 8,

    Closed = Confirmed | Cancelled | Deprecated
}

public sealed record Product(
    ProductId Id,
    Sku Sku,
    string Name,
    ProductStatus Status)
{
    public static Product When(Product entity, object @event)
    {
        return @event switch
        {
            ProductInitialized (var productId, var sku, var name) =>
                entity with
                {
                    Id = productId,
                    Sku = sku,
                    Name = name,
                    Status = ProductStatus.Initialized
                },

            ProductConfirmed (var productId, var confirmedBy) =>
                entity with
                {
                    Status = ProductStatus.Confirmed
                },

            ProductDeprecated (var productId, var deprecatedBy, var reason) =>
                entity with
                {
                    Status = ProductStatus.Deprecated
                },

            ProductCancelled (var productId, var cancelledBy, var reason) =>
                entity with
                {
                    Status = ProductStatus.Cancelled
                },

            _ => entity
        };
    }

    public static Product Default() =>
        new(ProductId.Empty(), Sku.Empty(), string.Empty, ProductStatus.Initialized);

    public static string MapToStreamId(Guid productId) =>
        $"Product-{productId}";
}


