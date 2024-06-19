using Eventuous;
using Commands = ShoppingCart.CartCommands.V1;
using Events = ShoppingCart.CartEvents.V1;

namespace ShoppingCart;

public class CartFuncService : FunctionalCommandService<CartState>
{
    [Obsolete("Obsolete according to Eventuous - TBU")]
    public CartFuncService(
        IEventStore store,
        TypeMapper? typeMap = null)
        : base(store, typeMap)
    {
        // Register the command handlers here
        OnNew<Commands.OpenCart>(cmd => GetStream(cmd.CartId), OpenCart);
        OnExisting<Commands.AddItemToCart>(cmd => GetStream(cmd.CartId), AddItemToCart);
    }

    // Helper function to get the stream name from the command
    private static StreamName GetStream(string id) => new($"ShoppingCart-{id}");

    // When there's no stream to load, the function only receives the command
    private static IEnumerable<object> OpenCart(Commands.OpenCart cmd)
    {
        yield return new Events.CartOpened(cmd.CartId, cmd.CustomerId);
    }

    // For an existing stream, the function receives the state and the events
    private static IEnumerable<object> AddItemToCart(
        CartState state,
        object[] originalEvents,
        Commands.AddItemToCart cmd)
    {
        var added = new Events.ItemAddedToCart(cmd.CartId, cmd.ProductId);

        yield return added;

        var newState = state.When(added);

        // could have logic based on the cart's current state
    }
}
