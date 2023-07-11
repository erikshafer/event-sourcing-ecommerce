using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace Catalog.Products;

public record ProductDrafted(Guid ProductId, string Sku);

public record DraftProduct(Guid ProductId, string Sku);

public class DraftProductValidator : AbstractValidator<DraftProduct>
{
    public DraftProductValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Sku).NotEmpty().MaximumLength(16);
    }
}

[WolverineHandler]
public class DraftProductHandler
{
    [Transactional]
    public static async Task Handle(DraftProduct command, IDocumentSession session)
    {
        // Deconstruct the command and
        var (productId, sku) = command;

        // initialize the aggregate's initial (AKA creation) event
        var @event = new ProductDrafted(productId, sku);

        // Registers the creation of a new event stream, appending the event(s) in order
        session.Events.StartStream<Product>(productId, @event);

        // Asynchronously saves all the pending changes in a single Postgres transaction
        await session.SaveChangesAsync();
    }
}
