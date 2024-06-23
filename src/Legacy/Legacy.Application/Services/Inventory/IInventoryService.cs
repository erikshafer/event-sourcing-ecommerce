namespace Legacy.Application.Services.Inventory;

public interface IInventoryService
{
    Task UpdateStock(UpdateStockRequest request);
}
