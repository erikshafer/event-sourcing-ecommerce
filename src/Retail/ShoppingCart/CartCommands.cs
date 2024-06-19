namespace ShoppingCart;

public static class CartCommands
{
    public static class V1
    {
        public record OpenCart(
            string CartId,
            string CustomerId
        );

        public record AddProductToCart(
            string CartId,
            string ProductId,
            int Quantity
        );
    }
}
