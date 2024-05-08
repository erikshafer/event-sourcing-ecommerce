using Catalog.Products;
using Eventuous;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;
using static Catalog.Products.ProductEvents;

namespace Catalog.Api.Queries;

[Obsolete("Obsolete per Eventuous")]
public class ProductStateProjection : MongoProjection<ProductDocument>
{
    public ProductStateProjection(IMongoDatabase database) : base(database)
    {
        On<V1.ProductInitialized>(stream => stream.GetId(), HandleProductInitialized);

        On<V1.ProductConfirmed>(builder => builder
            .UpdateOne
            .DefaultId()
            .Update((evt, update) =>
                update.Set(x => x.Status, ProductStatus.Confirmed.ToString())));
    }

    private static UpdateDefinition<ProductDocument> HandleProductInitialized(
        IMessageConsumeContext<V1.ProductInitialized> ctx,
        UpdateDefinitionBuilder<ProductDocument> update)
    {
        var evt = ctx.Message;

        return update.SetOnInsert(x => x.Id, ctx.Stream.GetId())
            .Set(x => x.Status, ProductStatus.Initialized.ToString())
            .Set(x => x.Name, evt.Name)
            .Set(x => x.Sku, evt.Sku);
    }
}
