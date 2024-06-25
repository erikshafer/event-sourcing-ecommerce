using Ecommerce.Core.Identities;
using Eventuous;
using Commands = ShoppingCart.Carts.CartCommands.V1;
using Events = ShoppingCart.Carts.CartEvents.V1;

namespace ShoppingCart.Carts;

public class CartFuncService : FunctionalCommandService<CartState>
{
    [Obsolete("Obsolete according to Eventuous - TBU")]
    public CartFuncService(
        IEventStore store,
        ICombIdGenerator idGenerator)
        : base(store)
    {
        var generatedId = idGenerator.New();

        OnNew<Commands.OpenCart>(cmd
            => GetStream(generatedId), OpenCart);

        OnExisting<Commands.AddProductToCart>(cmd
            => GetStream(cmd.CartId), AddProductToCart);

        OnExisting<Commands.RemoveProductFromCart>(cmd
            => GetStream(cmd.CartId), RemoveProductFromCart);

        OnExisting<Commands.ConfirmCart>(cmd
            => GetStream(cmd.CartId), ConfirmCart);

        OnExisting<Commands.CancelCart>(cmd
            => GetStream(cmd.CartId), CancelCart);

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

        static IEnumerable<object> ConfirmCart(
            CartState state,
            object[] originalEvents,
            Commands.ConfirmCart cmd)
        {
            if (state.CanProceedToCheckout())
            {
                var customerId = state.CustomerId.ToString();
                var productIds = state.ProductItems.Values.Select(pi => pi.ProductId.ToString()).ToArray();
                var totalPriceQuoted = 99.99m; // TODO
                var confirmedAt = DateTime.Now;
                yield return new Events.CartConfirmed(
                    cmd.CartId,
                    customerId,
                    productIds,
                    totalPriceQuoted,
                    confirmedAt);
            }
        }

        static IEnumerable<object> CancelCart(
            CartState state,
            object[] originalEvents,
            Commands.CancelCart cmd)
        {
            yield return new Events.CartCancelled(cmd.CartId);
        }
    }
}
