using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace Catalog.Brands.Brands;

public record BrandInitialized(Guid BrandId, string Name);

public record InitializeBrand(Guid BrandId, string Name);

public class InitializeBrandValidator : AbstractValidator<InitializeBrand>
{
    public InitializeBrandValidator()
    {
        RuleFor(x => x.BrandId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
    }
}

[WolverineHandler]
public class InitializeBrandHandler
{
    public static async Task Handle(InitializeBrand message, IDocumentSession session)
    {
        // Deconstruct the command and
        var (brandId, name) = message;

        // initialize the aggregate's initial (AKA creation) event
        var @event = new BrandInitialized(brandId, name);

        // Registers the creation of a new event stream, appending the event(s) in order
        session.Events.StartStream<Brand>(brandId, @event);

        // Asynchronously saves all the pending changes in a single Postgres transaction
        await session.SaveChangesAsync();
    }
}
