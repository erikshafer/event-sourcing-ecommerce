namespace ShoppingCart;

public record CartOpened
{
    public CartOpened(string cartId, string customerId)
    {
        CartId = cartId;
        CustomerId = customerId;
    }

    public string CartId { get; init; } = default!;
    public string CustomerId { get; init; } = default!;
}
