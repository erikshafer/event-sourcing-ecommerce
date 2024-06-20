using Ecommerce.Core.Identities;
using Eventuous;
using Commands = Inventory.Inventories.InventoryCommands.V1;
using Events = Inventory.Inventories.InventoryEvents.V1;

namespace Inventory.Inventories;

public class InventoryFuncService : FunctionalCommandService<InventoryState>
{
    [Obsolete("Obsolete according to Eventuous - TBU")]
    public InventoryFuncService(
        IEventStore store,
        ICombIdGenerator idGenerator,
        TypeMapper? typeMap = null)
        : base(store, typeMap)
    {
        var generatedId = idGenerator.New();

        OnNew<Commands.InitializeInventory>(cmd
            => GetStream(generatedId), InitializeInventory);

        OnExisting<Commands.StockInventoryFromProcurementOrder>(cmd
            => GetStream(cmd.InventoryId), StockInventoryFromProcurementOrder);

        OnExisting<Commands.IncrementInventory>(cmd
            => GetStream(cmd.InventoryId), IncrementInventory);

        OnExisting<Commands.DecrementInventory>(cmd
            => GetStream(cmd.InventoryId), DecrementInventory);

        static StreamName GetStream(string id) => new($"Inventory-{id}");

        IEnumerable<object> InitializeInventory(Commands.InitializeInventory cmd)
        {
            yield return new Events.InventoryInitialized(generatedId, cmd.Sku);
        }

        static IEnumerable<object> StockInventoryFromProcurementOrder(
            InventoryState state,
            object[] originalEvents,
            Commands.StockInventoryFromProcurementOrder cmd)
        {
            var procurementStock = new Events.InventoryStockedFromProcurementOrder(
                cmd.InventoryId,
                cmd.ProcurementId,
                cmd.Quantity);
            yield return procurementStock;

            var newState = state.When(procurementStock);
            // can evaluate new state's behavior to emit other events, etc
        }


        static IEnumerable<object> IncrementInventory(
            InventoryState state,
            object[] originalEvents,
            Commands.IncrementInventory cmd)
        {
            var inventoryIncremented = new Events.InventoryIncremented(
                cmd.InventoryId,
                cmd.Quantity);
            yield return inventoryIncremented;

            var newState = state.When(inventoryIncremented);
            // can evaluate new state's behavior to emit other events, etc
        }

        static IEnumerable<object> DecrementInventory(
            InventoryState state,
            object[] originalEvents,
            Commands.DecrementInventory cmd)
        {
            var inventoryDecremented = new Events.InventoryDecremented(
                cmd.InventoryId,
                cmd.Quantity);
            yield return inventoryDecremented;

            var newState = state.When(inventoryDecremented);
            // can evaluate new state's behavior to emit other events, etc
        }
    }
}
