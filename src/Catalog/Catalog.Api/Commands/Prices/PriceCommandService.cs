using Catalog.Prices;
using Ecommerce.Core.Identities;
using Eventuous;

namespace Catalog.Api.Commands.Prices;

public class PriceCommandService : CommandService<Price, PriceState, PriceId>
{
    [Obsolete("Obsolete usage of OnNewAsync per Eventuous; use new API instead (TODO)")]
    public PriceCommandService(
        IAggregateStore store,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized,
        ICombIdGenerator idGenerator)
        : base(store)
    {
        var generatedId = idGenerator.New();
        OnNewAsync<PriceCommands.Initialize>(cmd => new PriceId(generatedId),
            ((price, cmd, _) => price.Draft(
                generatedId,
                cmd.Sku,
                cmd.MinimumAdvertisedPrice,
                cmd.ManufacturerSuggestedRetailPrice,
                cmd.BundledQuantity,
                cmd.BundledPrice,
                cmd.Currency,
                DateTimeOffset.Now,
                cmd.CreatedBy,
                isSkuAvailable,
                isUserAuthorized)));

        OnExisting<PriceCommands.Activate>(cmd => new PriceId(cmd.PriceId),
            ((price, cmd) => price.Activate(
                DateTimeOffset.Now,
                cmd.ActivatedBy)));

        OnExisting<PriceCommands.Deprecate>(cmd => new PriceId(cmd.PriceId),
            ((price, cmd) => price.Deprecate(
                DateTimeOffset.Now,
                cmd.DeprecatedBy,
                cmd.Reason)));
    }
}
