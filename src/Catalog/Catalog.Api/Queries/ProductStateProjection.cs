using Catalog.Products;
using Eventuous;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;
using static Catalog.Products.ProductEvents;

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
                .Set(x => x.Sku, evt.Sku);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductActivated> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var evt = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Activated));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductArchived> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var evt = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Archived));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductDraftCancelled> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var evt = ctx.Message;

        return update.Set(x => x.Status, nameof(ProductStatus.Cancelled));
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductNameAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var evt = ctx.Message;

        return update.Set(x => x.Name, evt.Name);
    }

    private static UpdateDefinition<ProductDocument> Handle(
        IMessageConsumeContext<V1.ProductDescriptionAdjusted> ctx,
        UpdateDefinition<ProductDocument> update)
    {
        var evt = ctx.Message;

        return update.Set(x => x.Description, evt.Description);
    }
}
