namespace ShoppingCart;

public record OpenCart
{
    public string CartId { get; init; } = default!;
    public string CustomerId { get; init; } = default!;
}
