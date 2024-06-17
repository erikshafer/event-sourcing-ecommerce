namespace ShoppingCart;

public record AddItemToCart
{
    public string CartId { get; init; } = default!;
    public string ItemId { get; init; } = default!;
}
