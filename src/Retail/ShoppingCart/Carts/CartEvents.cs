using Eventuous;

namespace ShoppingCart.Carts;

public static class CartEvents
{
    public static class V1
    {
        [EventType("V1.CartOpened")]
        public record CartOpened(
            string CartId,
            string CustomerId
        );

        [EventType("V1.ProductAddedToCart")]
        public record ProductAddedToCart(
            string CartId,
            string ProductId,
            int Quantity
        );

        [EventType("V1.ProductRemovedFromCart")]
        public record ProductRemovedFromCart(
            string CartId,
            string ProductId,
            int Quantity
        );

        [EventType("V1.CartConfirmed")]
        public record CartConfirmed(
            string CartId
        );

        [EventType("V1.EmptyCartDetected")]
        public record EmptyCartDetected(
            string CartId
        );
    }
}
