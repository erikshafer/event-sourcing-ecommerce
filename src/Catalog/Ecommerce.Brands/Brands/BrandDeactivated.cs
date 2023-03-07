using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Brands.Brands;

public record BrandDeactivated(Guid BrandId);

public record DeactivateBrand(Guid BrandId); // command

public class DeactivateBrandValidator : AbstractValidator<DeactivateBrand>
{
    public DeactivateBrandValidator()
    {
        RuleFor(x => x.BrandId).NotEmpty();
    }
}

[WolverineHandler]
public class DeactivateBrandHandler
{
    [Transactional]
    public static async Task Handle(DeactivateBrand command, IDocumentSession session)
    {
        var id = command.BrandId;
        var brand = await session.Events.AggregateStreamAsync<Brand>(id);
        brand!.Deactivate();
        var @event = brand.DequeueUncommittedEvents();
        session.Events.Append(id, @event);
        await session.SaveChangesAsync();
    }
}
