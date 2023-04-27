using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace Products;

public record ProductConfirmed(Guid ProductId); // event

public record ConfirmProduct(Guid ProductId); // command

public class ConfirmProductValidator : AbstractValidator<ConfirmProduct>
{
    public ConfirmProductValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}

[WolverineHandler]
public class ProductConfirmedHandler
{
    [Transactional]
    public static async Task Handle(ConfirmProduct command, IDocumentSession session)
    {
        var id = command.ProductId;
        var product = await session.Events.AggregateStreamAsync<Product>(id);
        product!.Confirm();
        var @event = product.DequeueUncommittedEvents();
        session.Events.Append(id, @event);
        await session.SaveChangesAsync();
    }
}
