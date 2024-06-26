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

        [EventType("V1.PricedProductAddedToCart")]
        public record PricedProductAddedToCart(
            string CartId,
            string ProductId,
            string PriceId,
            decimal UnitPrice,
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
            string CartId,
            string CustomerId,
            string[] ProductIds,
            decimal TotalPriceQuoted,
            DateTime ConfirmedAt
        );

        [EventType("V1.CartCancelled")]
        public record CartCancelled(
            string CartId
        );

        [EventType("V1.EmptyCartDetected")]
        public record EmptyCartDetected(
            string CartId
        );

        [EventType("V1.CartHasProductsReminder")]
        public record CartHasProductsReminder(
            string CartId,
            string CustomerId,
            DateTime RemindAfter
        );
    }
}
