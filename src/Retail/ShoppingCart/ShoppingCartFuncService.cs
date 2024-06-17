using Eventuous;
using Commands = ShoppingCart.ShoppingCartCommands.V1;
using Events = ShoppingCart.ShoppingCartEvents.V1;

namespace ShoppingCart;

public class ShoppingCartFuncService : FunctionalCommandService<ShoppingCartState>
{
    [Obsolete("Obsolete according to Eventuous - TBU")]
    public ShoppingCartFuncService(
        IEventStore store,
        TypeMapper? typeMap = null)
        : base(store, typeMap)
    {
        // Register the command handlers here
        OnNew<Commands.OpenCart>(cmd => GetStream(cmd.CartId), OpenCart);
        OnExisting<Commands.AddItemToCart>(cmd => GetStream(cmd.CartId), AddItemToCart);
    }

    // Helper function to get the stream name from the command
    private static StreamName GetStream(string id) => new($"Cart-{id}");

    // When there's no stream to load, the function only receives the command
    private static IEnumerable<object> OpenCart(Commands.OpenCart cmd)
    {
        yield return new Events.CartOpened(cmd.CartId, cmd.CustomerId);
    }

    // For an existing stream, the function receives the state and the events
    private static IEnumerable<object> AddItemToCart(
        ShoppingCartState state,
        object[] originalEvents,
        Commands.AddItemToCart cmd)
    {
        return new List<ProductItem>(); // TODO
    }
}
