using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Catalog.Products;

public sealed record DraftProduct(Guid ProductId, Sku Sku); // command; present tense

public sealed record ProductDrafted(Guid ProductId, Sku Sku); // event; past tense

internal static class DraftProductHandler
{
    [Transactional]
    public static ProductDrafted Handle(DraftProduct command, IDocumentSession session)
    {
        var product = Product.Draft(command.ProductId, command.Sku);
        session.Store(product);
        return new ProductDrafted(product.Id, product.Sku);
    }
}