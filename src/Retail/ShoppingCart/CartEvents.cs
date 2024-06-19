namespace ShoppingCart;

public static class CartEvents
{
    public static class V1
    {
        public record CartOpened(string CartId, string CustomerId);

        public record ItemAddedToCart(string CartId, string ProductId);
    }
}
