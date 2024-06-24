using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;
using ShoppingCart.Carts;

namespace ShoppingCart.Api.Queries.Carts;

[Obsolete("Obsolete per Eventuous; use new API instead (TODO)")]
public class UserCartProjection : MongoProjection<UserCartDocument>
{
    public UserCartProjection(IMongoDatabase database) : base(database)
    {
        On<CartEvents.V1.CartOpened>(stream => stream.GetId(), Handle);

        On<CartEvents.V1.CartConfirmed>(builder => builder
            .UpdateOne
            .DefaultId()
            .Update((evt, update) =>
                update.Set(x => x.Status, nameof(CartStatus.Confirmed))));

        On<CartEvents.V1.CartCancelled>(builder => builder
            .UpdateOne
            .DefaultId()
            .Update((evt, update) =>
                update.Set(x => x.Status, nameof(CartStatus.Cancelled))));
    }

    private static UpdateDefinition<UserCartDocument> Handle(
        IMessageConsumeContext<CartEvents.V1.CartOpened> ctx,
        UpdateDefinitionBuilder<UserCartDocument> update)
    {
        var evt = ctx.Message;

        return update.SetOnInsert(x => x.Id, ctx.Stream.GetId())
            .Set(x => x.CustomerId, evt.CustomerId)
            .Set(x => x.Status, nameof(CartStatus.Opened));
    }
}
