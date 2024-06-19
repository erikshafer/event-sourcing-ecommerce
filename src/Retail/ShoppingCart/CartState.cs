using Ecommerce.Eventuous.Exceptions;
using Eventuous;
using static ShoppingCart.CartEvents;

namespace ShoppingCart;

public record CartState : State<CartState>
{
    public CartId Id { get; init; } = null!;
    public CustomerId CustomerId { get; init; } = null!;
    public CartStatus Status { get; init; } = CartStatus.Unset;
    public ProductItems ProductItems { get; init; } = null!;

    public CartState()
    {
        On<V1.CartOpened>(Handle);
        On<V1.ProductAddedToCart>(Handle);
    }

    private static CartState Handle(CartState state, V1.CartOpened @event) => state with
    {
        Id = new CartId(@event.CartId),
        CustomerId = new CustomerId(@event.CustomerId),
        Status = CartStatus.Opened,
        ProductItems = ProductItems.Empty
    };

    private static CartState Handle(CartState state, V1.ProductAddedToCart @event) => state.Status switch
    {
        CartStatus.Confirmed => throw InvalidStateChangeException.For<CartState, V1.ProductAddedToCart>(state.Id, CartStatus.Confirmed),
        CartStatus.Expired => throw InvalidStateChangeException.For<CartState, V1.ProductAddedToCart>(state.Id, CartStatus.Expired),
        _ => state with { ProductItems = state.ProductItems.Add(ProductItem.Create(@event.ProductId, 0)) }
    };
}
