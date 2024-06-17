using Eventuous;

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
        OnNew<OpenCart>(cmd => GetStream(cmd.CartId), OpenCart);
        OnExisting<AddItemToCart>(cmd => GetStream(cmd.CartId), AddItemToCart);
    }

    // Helper function to get the stream name from the command
    private static StreamName GetStream(string id) => new($"Cart-{id}");

    // When there's no stream to load, the function only receives the command
    private static IEnumerable<object> OpenCart(OpenCart cmd)
    {
        yield return new CartOpened(cmd.CartId, cmd.CustomerId);
    }

    // For an existing stream, the function receives the state and the events
    private static IEnumerable<object> AddItemToCart(
        ShoppingCartState state,
        object[] originalEvents,
        AddItemToCart cmd)
    {
        return new List<object>(); // TODO
    }
}
