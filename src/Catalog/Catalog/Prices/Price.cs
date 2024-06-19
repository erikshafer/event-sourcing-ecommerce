using Eventuous;

namespace Catalog.Prices;

public class Price : Aggregate<PriceState>
{
    public async Task Draft(
        string priceId,
        string sku,
        decimal minimumAdvertisedPrice,
        decimal manufacturerSuggestedRetailPrice,
        int bundledQuantity,
        decimal bundledPrice,
        string currency,
        DateTimeOffset createdAt,
        string createdBy,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized)
    {
        EnsureDoesntExist();
        await ValidateSkuAvailability(new Sku(sku), isSkuAvailable);
        await AuthorizeInternalUser(new InternalUserId(createdBy), isUserAuthorized);

        Apply(
            new PriceEvents.V1.PriceInitialized(
                priceId,
                sku,
                minimumAdvertisedPrice,
                manufacturerSuggestedRetailPrice,
                bundledQuantity,
                bundledPrice,
                currency,
                createdAt,
                createdBy
            )
        );
    }

    public void Activate(DateTimeOffset activatedAt, string activatedBy)
    {
        EnsureExists();

        Apply(
            new PriceEvents.V1.PriceActivated(
                State.Id.Value,
                activatedAt,
                activatedBy
            )
        );
    }

    public void Deprecate(DateTimeOffset archivedAt, string archivedBy, string reason)
    {
        EnsureExists();

        Apply(
            new PriceEvents.V1.PriceDeprecated(
                State.Id.Value,
                archivedAt,
                archivedBy,
                reason
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
