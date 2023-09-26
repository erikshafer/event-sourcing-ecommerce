using Eventuous;

namespace Catalog.Products;

public static class ProductEvents
{
    [EventType("ProductDrafted")]
    public record ProductDrafted(
        Sku Sku,
        string Code
    );
}
