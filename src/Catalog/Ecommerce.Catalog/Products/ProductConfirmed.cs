using Ecommerce.Core.Exceptions;
using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Catalog.Products;

public sealed record ProductConfirmed(Guid ProductId, DateTime ConfirmedAt);

public sealed record ConfirmProduct(Guid ProductId, DateTime ConfirmedAt);

internal static class ProductConfirmedHandler
{
    [Transactional]
    public static async Task Handle(ConfirmProduct command, IDocumentSession session)
    {
        var product = await session.Events.AggregateStreamAsync<Product>(command.ProductId);
        
        if (product == null)
            throw AggregateNotFoundException.For<Product>(command.ProductId);
        
        product.Confirm();
    }
}