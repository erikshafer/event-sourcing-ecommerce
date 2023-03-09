using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace Catalog.Brands.Brands;

public record BrandActivated(Guid BrandId);

public record ActivateBrand(Guid BrandId);

public class ActivateBrandValidator : AbstractValidator<ActivateBrand>
{
    public ActivateBrandValidator()
    {
        RuleFor(x => x.BrandId).NotEmpty();
    }
}

[WolverineHandler]
public class ActivateBrandHandler
{
    public static async Task Handle(ActivateBrand message, IDocumentSession session)
    {
        var id = message.BrandId;
        var product = await session.Events.AggregateStreamAsync<Brand>(id);
        product!.Activate();
        var @event = product.DequeueUncommittedEvents();
        session.Events.Append(id, @event);
        await session.SaveChangesAsync();
    }
}
