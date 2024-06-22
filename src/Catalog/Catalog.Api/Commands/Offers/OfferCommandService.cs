using Catalog.Offers;
using Ecommerce.Core.Identities;
using Eventuous;
using MongoDB.Bson.Serialization;

namespace Catalog.Api.Commands.Offers;

public class OfferCommandService : CommandService<Offer, OfferState, OfferId>
{
    [Obsolete("Obsolete usage of OnNewAsync per Eventuous; use new API instead (TODO)")]
    public OfferCommandService(
        IAggregateStore store,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized,
        ICombIdGenerator idGenerator)
        : base(store)
    {
        var generatedId = idGenerator.New();
        OnNewAsync<OfferCommands.Draft>(cmd => new OfferId(generatedId),
            ((offer, cmd, _) => offer.Draft(
                generatedId,
                cmd.Sku,
                DateTimeOffset.Now,
                cmd.CreatedBy,
                isSkuAvailable,
                isUserAuthorized)));

        OnExisting<OfferCommands.Activate>(cmd => new OfferId(cmd.OfferId),
            ((offer, cmd) => offer.Activate(
                DateTimeOffset.Now,
                cmd.ActivatedBy)));
    }
}
