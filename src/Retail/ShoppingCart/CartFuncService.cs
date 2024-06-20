using Ecommerce.Core.Identities;
using Eventuous;
using Commands = ShoppingCart.CartCommands.V1;
using Events = ShoppingCart.CartEvents.V1;

namespace ShoppingCart;

public class CartFuncService : FunctionalCommandService<CartState>
{
    [Obsolete("Obsolete according to Eventuous - TBU")]
    public CartFuncService(
        IEventStore store,
        ICombIdGenerator idGenerator,
        TypeMapper? typeMap = null)
        : base(store, typeMap)
    {
        var generatedId = idGenerator.New();

        OnNew<Commands.OpenCart>(cmd
            => GetStream(generatedId), OpenCart);

        OnExisting<Commands.AddProductToCart>(cmd
            => GetStream(cmd.CartId), AddProductToCart);

        OnExisting<Commands.RemoveProductFromCart>(cmd
            => GetStream(cmd.CartId), RemoveProductFromCart);

        OnExisting<Commands.PrepareCartForCheckout>(cmd
            => GetStream(cmd.CartId), PrepareCartForCheckout);

        static StreamName GetStream(string id) => new($"Cart-{id}");

        IEnumerable<object> OpenCart(Commands.OpenCart cmd)
        {
            yield return new Events.CartOpened(generatedId, cmd.CustomerId);
        }

        static IEnumerable<object> AddProductToCart(
            CartState state,
            object[] originalEvents,
            Commands.AddProductToCart cmd)
        {
            var added = new Events.ProductAddedToCart(
                cmd.CartId,
                cmd.ProductId,
                cmd.Quantity);
            yield return added;

            var newState = state.When(added);
            // can evaluate new state's behavior to emit other events, etc
        }

        static IEnumerable<object> RemoveProductFromCart(
            CartState state,
            object[] originalEvents,
            Commands.RemoveProductFromCart cmd)
        {
            var removed = new Events.ProductRemovedFromCart(
                cmd.CartId,
                cmd.ProductId,
                cmd.Quantity);
            yield return removed;

            var newState = state.When(removed);

            if (newState.HasProductItems is false)
                yield return new Events.EmptyCartDetected(cmd.CartId);
        }

        static IEnumerable<object> PrepareCartForCheckout(
            CartState state,
            object[] originalEvents,
            Commands.PrepareCartForCheckout cmd)
        {
            if (state.CanProceedToCheckout())
                yield return new Events.CartConfirmed(cmd.CartId);
        }
    }
}
