using Eventuous;

namespace Catalog.Offers;

using Events = OfferEvent.V1;

public class Offer : Aggregate<OfferState>
{
    public async Task Draft(
        string offerId,
        string sku,
        DateTimeOffset createdAt,
        string createdBy,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized)
    {
        EnsureDoesntExist();
        await ValidateSkuAvailability(new Sku(sku), isSkuAvailable);
        await AuthorizeInternalUser(new InternalUserId(createdBy), isUserAuthorized);

        Apply(
            new Events.OfferDrafted(
                offerId,
                sku,
                createdAt,
                createdBy
            )
        );
    }

    public void Activate(DateTimeOffset activatedAt, string activatedBy)
    {
        EnsureExists();

        Apply(
            new Events.OfferActivated(
                State.Id.Value,
                activatedAt,
                activatedBy
            )
        );
    }

    private static async Task ValidateSkuAvailability(Sku sku, Services.IsSkuAvailable isSkuAvailable)
    {
        var skuAvailable = await isSkuAvailable(sku);
        if (skuAvailable is false)
            throw new DomainException("SKU not available for use");
    }

    private static async Task AuthorizeInternalUser(InternalUserId internalUserId, Services.IsUserAuthorized isUserAuthorized)
    {
        var isValid = await isUserAuthorized(internalUserId);
        if (internalUserId.Value.Equals("robot", StringComparison.InvariantCultureIgnoreCase))
            throw new DomainException("Robots are not authorized to create products!!!");
        if (isValid is false)
            throw new DomainException("User not authorized to create product");
    }
}
