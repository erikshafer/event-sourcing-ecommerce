using Catalog.Prices;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;
using static Catalog.Prices.PriceEvents;

namespace Catalog.Api.Queries;

[Obsolete("Obsolete per Eventuous; use new API instead (TODO)")]
public class PriceStateProjection : MongoProjection<PriceDocument>
{
    public PriceStateProjection(IMongoDatabase database) : base(database)
    {
        On<V1.PriceInitialized>(stream => stream.GetId(), Handle);
    }

    private static UpdateDefinition<PriceDocument> Handle(
        IMessageConsumeContext<V1.PriceInitialized> ctx,
        UpdateDefinitionBuilder<PriceDocument> update)
    {
        var evt = ctx.Message;

        return update.SetOnInsert(x => x.Id, ctx.Stream.GetId())
            .Set(x => x.Sku, evt.Sku)
            .Set(x => x.CreatedAt, evt.CreatedAt)
            .Set(x => x.CreatedBy, evt.CreatedBy)
            .Set(x => x.Status, nameof(PriceStatus.Initialized))
            .Set(x => x.MinimumAdvertisedPrice, evt.MinimumAdvertisedPrice)
            .Set(x => x.ManufacturerSuggestedRetailPrice, evt.ManufacturerSuggestedRetailPrice);
        // TODO: bundled pricing, etc
    }

    private static UpdateDefinition<PriceDocument> Handle(
        IMessageConsumeContext<V1.PriceActivated> ctx,
        UpdateDefinition<PriceDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(PriceStatus.Activated));
    }

    private static UpdateDefinition<PriceDocument> Handle(
        IMessageConsumeContext<V1.PriceDeprecated> ctx,
        UpdateDefinition<PriceDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(PriceStatus.Deprecated));
    }
}
