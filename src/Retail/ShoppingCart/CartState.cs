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

    public bool HasProductItems => ProductItems.IsEmpty;

    public CartState()
    {
        On<V1.CartOpened>(Handle);
        On<V1.ProductAddedToCart>(Handle);
        On<V1.ProductRemovedFromCart>(Handle);
        On<V1.CartConfirmed>(Handle);
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
        _ => state with { ProductItems = state.ProductItems.Add(ProductItem.Create(@event.ProductId, @event.Quantity)) }
    };

    private static CartState Handle(CartState state, V1.ProductRemovedFromCart @event) => state.Status switch
    {
        CartStatus.Confirmed => throw InvalidStateChangeException.For<CartState, V1.ProductAddedToCart>(state.Id, CartStatus.Confirmed),
        _ => state with { ProductItems = state.ProductItems.Remove(ProductItem.Create(@event.ProductId, @event.Quantity)) }
    };

    private static CartState Handle(CartState state, V1.CartConfirmed @event) => state.Status switch
    {
        CartStatus.Confirmed => throw InvalidStateChangeException.For<CartState, V1.ProductAddedToCart>(state.Id, CartStatus.Confirmed),
        _ => state with { Status = CartStatus.Confirmed }
    };

    public bool CanProceedToCheckout() => Status switch
    {
        CartStatus.Unset or CartStatus.Opened => false,
        _ => HasProductItems
    };
}
