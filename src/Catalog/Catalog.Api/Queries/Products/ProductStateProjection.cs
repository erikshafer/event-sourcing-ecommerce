using Catalog.Products;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;

namespace Catalog.Api.Queries.Products;

[Obsolete("Obsolete per Eventuous; use new API instead (TODO)")]
public class ProductStateProjection : MongoProjection<ProductDocument>
{
    public ProductStateProjection(IMongoDatabase database) : base(database)
    {
        On<ProductEvents.V1.ProductDrafted>(stream => stream.GetId(), Handle);

        On<ProductEvents.V1.ProductActivated>(builder => builder
            .UpdateOne
            .DefaultId()
            .Update((evt, update) =>
                update.Set(x => x.Status, nameof(ProductStatus.Activated))));

        On<ProductEvents.V1.ProductTakeMeasurement>(builder => builder
            .UpdateOne
            .DefaultId()
            .UpdateFromContext((ctx, update) =>
                update.AddToSet(
                    x => x.Measurements,
                    new ProductDocument.Measurement(
                        ctx.Message.Type,
                        ctx.Message.Unit,
                        ctx.Message.Value))));

        On<ProductEvents.V1.ProductRemoveMeasurement>(builder => builder
            .UpdateOne
            .Filter((ctx, doc) =>
                doc.Measurements.Select(booking => booking.Type).Contains(ctx.Message.Type))
            .UpdateFromContext((ctx, update) =>
                update.PullFilter(
                    x => x.Measurements,
                    x => x.Type == ctx.Message.Type)));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<ProductEvents.V1.ProductDrafted> ctx,
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
            .Set(x => x.Brand, evt.Brand);
        // TODO: evaluate measurements during ProductDrafted event
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<ProductEvents.V1.ProductActivated> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Activated));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<ProductEvents.V1.ProductArchived> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Archived));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<ProductEvents.V1.ProductDraftCancelled> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Cancelled));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<ProductEvents.V1.ProductNameAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Name, @event.Name);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<ProductEvents.V1.ProductDescriptionAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Description, @event.Description);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<ProductEvents.V1.ProductBrandAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var @event = ctx.Message;

        return update.Set(x => x.Brand, @event.Brand);
    }
}
