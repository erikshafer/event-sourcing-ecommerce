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
        ISnowflakeIdGenerator idGenerator,
        TypeMapper? typeMap = null)
        : base(store, typeMap)
    {
        var generatedId = idGenerator.New(); // TODO: leverage

        // Register command handlers
        OnNew<Commands.OpenCart>(cmd => GetStream(generatedId), OpenCart);
        OnNew<Commands.OpenCartWithProvidedId>(cmd => GetStream(cmd.CartId), OpenCartCommandHasId);
        OnExisting<Commands.AddProductToCart>(cmd => GetStream(cmd.CartId), AddItemToCart);

        // Helper function to get the stream name from the command
        static StreamName GetStream(string id) => new($"Cart-{id}");

        // When there's no stream to load, the function only receives the command. (CLOSURES)
        IEnumerable<object> OpenCart(Commands.OpenCart cmd)
        {
            yield return new Events.CartOpened(generatedId, cmd.CustomerId);
        }

        // When there's no stream to load, the function only receives the command
        static IEnumerable<object> OpenCartCommandHasId(Commands.OpenCartWithProvidedId cmd)
        {
            yield return new Events.CartOpened(cmd.CartId, cmd.CustomerId);
        }

        // For an existing stream, the function receives the state and the events
        static IEnumerable<object> AddItemToCart(
            CartState state,
            object[] originalEvents,
            Commands.AddProductToCart cmd)
        {
            var added = new Events.ProductAddedToCart(cmd.CartId, cmd.ProductId, cmd.Quantity);

            yield return added;

            var newState = state.When(added);

            // could have logic based on the cart's current state
        }
    }
}
