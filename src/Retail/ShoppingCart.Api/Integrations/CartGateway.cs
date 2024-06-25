using Eventuous;
using Eventuous.EventStore.Producers;
using Eventuous.Gateway;
using Eventuous.Subscriptions.Context;
using ShoppingCart.Carts;

namespace ShoppingCart.Api.Integrations;

public static class CartGateway
{
    private static readonly StreamName Stream = new("CartsIntegration");

    public static ValueTask<GatewayMessage<EventStoreProduceOptions>[]> Transform(IMessageConsumeContext original)
    {
        var result = original.Message is CartEvents.V1.CartConfirmed evt
            ? new GatewayMessage<EventStoreProduceOptions>(
                Stream,
                new CartIntegrationEvents.CartConfirmedRecorded(
                    original.Stream.GetId(),
                    evt.CustomerId,
                    evt.ProductIds,
                    evt.TotalPriceQuoted,
                    evt.ConfirmedAt),
                new Metadata(),
                new EventStoreProduceOptions())
            : null;

        GatewayMessage<EventStoreProduceOptions>[] gatewayMessages = result != null
            ? [result]
            : [];

        return ValueTask.FromResult(gatewayMessages);
    }
}
