namespace ShoppingCart;

public static class CartCommands
{
    public static class V1
    {
        public record OpenCart(
            string CustomerId
        );

        public record AddProductToCart(
            string CartId,
            string ProductId,
            int Quantity
        );

        public record RemoveProductFromCart(
            string CartId,
            string ProductId,
            int Quantity
        );

        public record PrepareCartForCheckout(
            string CartId
        );

        public record ConfirmCart(
            string CartId
        );
    }
}
