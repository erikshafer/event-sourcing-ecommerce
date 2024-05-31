using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;
using static Catalog.ProductEvents;

namespace Catalog.Api.Queries;

[Obsolete("Obsolete per Eventuous; use new API instead (TODO)")]
public class ProductStateProjection : MongoProjection<ProductDocument>
{
    public ProductStateProjection(IMongoDatabase database) : base(database)
    {
        On<V1.ProductDrafted>(stream => stream.GetId(), Handle);

        On<V1.ProductActivated>(builder => builder
            .UpdateOne
            .DefaultId()
            .Update((evt, update) =>
                update.Set(x => x.Status, nameof(ProductStatus.Activated))));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductDrafted> ctx,
        UpdateDefinitionBuilder<ProductDocument> update)
    {
        var evt = ctx.Message;

        return update.SetOnInsert(x => x.Id, ctx.Stream.GetId())
            .Set(x => x.CreatedAt, evt.CreatedAt)
            .Set(x => x.CreatedBy, evt.CreatedBy)
            .Set(x => x.Status, nameof(ProductStatus.Drafted))
            .Set(x => x.Name, evt.Name)
            .Set(x => x.Sku, evt.Sku)
            .Set(x => x.Description, evt.Description)
            .Set(x => x.Brand, evt.Brand)
            .Set(x => x.Measurements, evt.Measurements);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductActivated> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Activated));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductArchived> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Archived));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductDraftCancelled> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Cancelled));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductNameAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Name, @event.Name);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductDescriptionAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Description, @event.Description);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductBrandAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Brand, @event.Brand);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductTakeMeasurement> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Measurements, @event.ToString());
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductRemoveMeasurement> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Measurements, string.Empty);
    }
}
