namespace ShoppingCart;

public static class ShoppingCartEvents
{
    public static class V1
    {
        public record CartOpened(string CartId, string CustomerId);

        
    }
}
