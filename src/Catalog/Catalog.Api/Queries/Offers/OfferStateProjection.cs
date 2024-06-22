using Catalog.Offers;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;

namespace Catalog.Api.Queries.Offers;

[Obsolete("Obsolete per Eventuous; use new API instead (TODO)")]
public class OfferStateProjection : MongoProjection<OfferDocument>
{
    public OfferStateProjection(IMongoDatabase database) : base(database)
    {
        On<OfferEvent.V1.OfferDrafted>(stream => stream.GetId(), Handle);
    }

    private static UpdateDefinition<OfferDocument> Handle(
        IMessageConsumeContext<OfferEvent.V1.OfferDrafted> ctx,
        UpdateDefinitionBuilder<OfferDocument> update)
    {
        var evt = ctx.Message;

        return update.SetOnInsert(x => x.Id, ctx.Stream.GetId())
            .Set(x => x.CreatedAt, evt.CreatedAt)
            .Set(x => x.CreatedBy, evt.CreatedBy)
            .Set(x => x.Status, nameof(OfferStatus.Drafted))
            .Set(x => x.Sku, evt.Sku);
    }

    private static UpdateDefinition<OfferDocument> Handle(
        IMessageConsumeContext<OfferEvent.V1.OfferActivated> ctx,
        UpdateDefinition<OfferDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(OfferStatus.Activated));
    }
}
