using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Catalog.Products;

public record ProductCancelled(Guid ProductId); // event

public record CancelProduct(Guid ProductId); // command

public class CancelProductValidator : AbstractValidator<CancelProduct>
{
    public CancelProductValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}

[WolverineHandler]
public class CancelProductHandler
{
    [Transactional]
    public static async Task Handle(CancelProduct command, IDocumentSession session)
    {
        var id = command.ProductId;
        var product = await session.Events.AggregateStreamAsync<Product>(id);
        product!.Cancel();
        var @event = product.DequeueUncommittedEvents();
        session.Events.Append(id, @event);
        await session.SaveChangesAsync();
    }
}
