using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace Catalog.Brands.Brands;

public record ContactDetailsChanged(Guid BrandId, string ContactName, string ContactEmail);

public record ChangeContactDetails(Guid BrandId, string ContactName, string ContactEmail);

public class ChangeContactDetailsValidator : AbstractValidator<ChangeContactDetails>
{
    public ChangeContactDetailsValidator()
    {
        RuleFor(x => x.BrandId).NotEmpty();
        RuleFor(x => x.ContactName).NotNull();
        RuleFor(x => x.ContactEmail).NotNull();
    }
}

[WolverineHandler]
public class ChangeContactDetailsHandler
{
    public static async Task Handle(ChangeContactDetails message, IDocumentSession session)
    {
        var (id, contactName, contactEmail) = message;
        var brand = await session.Events.AggregateStreamAsync<Brand>(id);
        brand!.ChangeContactDetails(contactName, contactName);
        var @event = brand.DequeueUncommittedEvents();
        session.Events.Append(id, @event);
        await session.SaveChangesAsync();
    }
}
