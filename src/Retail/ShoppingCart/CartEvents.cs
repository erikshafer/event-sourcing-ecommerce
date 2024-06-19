using Eventuous;

namespace ShoppingCart;

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
    }
}
