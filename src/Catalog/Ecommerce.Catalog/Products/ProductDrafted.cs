using Marten;
using Wolverine.Attributes;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public record ProductDrafted(Guid ProductId, Sku Sku, Brand Brand, string Name); // event

public record DraftProduct(Guid ProductId, Sku Sku, Brand Brand, string Name); // command

internal static class DraftProductHandler
{
    [Transactional]
    public static async Task Handle(
        DraftProduct command,
        IDocumentSession session,
        IMartenOutbox outbox,
        CancellationToken ct)
    {
        // Deconstruct the command and
        var (productId, sku, brand, name) = command;

        // initialize the aggregate's initial (AKA creation) event
        var @event = new ProductDrafted(productId, sku, brand, name);

        // Registers the creation of a new event stream, appending the event(s) in order
        session.Events.StartStream<Product>(productId, @event);

        // Message isn't actually sent until session is committed
        await outbox.SendAsync(@event);

        // Asynchronously saves all the pending changes in a single Postgres transaction
        await session.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
