using Ecommerce.Eventuous.Exceptions;
using Eventuous;
using ShoppingCart.Products;
using static ShoppingCart.Carts.CartEvents.V1;

namespace ShoppingCart.Carts;

public record CartState : State<CartState>
{
    public CartId Id { get; init; } = null!;
    public CustomerId CustomerId { get; init; } = null!;
    public CartStatus Status { get; init; } = CartStatus.Unset;
    public ProductItems ProductItems { get; init; } = null!;

    public bool HasProductItems => ProductItems.IsEmpty is false;

    public CartState()
    {
        On<CartOpened>(Handle);
        On<ProductAddedToCart>(Handle);
        On<ProductRemovedFromCart>(Handle);
        On<CartConfirmed>(Handle);
        On<CartCancelled>(Handle);
    }

    private static CartState Handle(CartState state, CartOpened @event) => state with
    {
        Id = new CartId(@event.CartId),
        CustomerId = new CustomerId(@event.CustomerId),
        Status = CartStatus.Opened,
        ProductItems = ProductItems.Empty
    };

    private static CartState Handle(CartState state, ProductAddedToCart @event) => state.Status switch
    {
        CartStatus.Confirmed => throw InvalidStateChangeException.For<CartState, ProductAddedToCart>(state.Id, CartStatus.Confirmed),
        _ => state with { ProductItems = state.ProductItems.Add(ProductItem.From(@event.ProductId, @event.Quantity)) }
    };

    private static CartState Handle(CartState state, ProductRemovedFromCart @event) => state.Status switch
    {
        CartStatus.Confirmed => throw InvalidStateChangeException.For<CartState, ProductRemovedFromCart>(state.Id, CartStatus.Confirmed),
        _ => state with { ProductItems = state.ProductItems.Remove(ProductItem.From(@event.ProductId, @event.Quantity)) }
    };

    private static CartState Handle(CartState state, CartConfirmed @event) => state.Status switch
    {
        CartStatus.Confirmed => throw InvalidStateChangeException.For<CartState, CartConfirmed>(state.Id, CartStatus.Confirmed),
        _ => state with { Status = CartStatus.Confirmed }
    };

    private static CartState Handle(CartState state, CartCancelled @event) => state.Status switch
    {
        CartStatus.Confirmed => throw InvalidStateChangeException.For<CartState, CartCancelled>(state.Id, CartStatus.Confirmed),
        CartStatus.Cancelled => throw InvalidStateChangeException.For<CartState, CartCancelled>(state.Id, CartStatus.Cancelled),
        _ => state with { Status = CartStatus.Cancelled }
    };

    public bool CanProceedToCheckout() => Status switch
    {
        CartStatus.Unset => false,
        CartStatus.Confirmed => false,
        _ => HasProductItems
    };
}
