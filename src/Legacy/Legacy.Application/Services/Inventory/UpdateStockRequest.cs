using MediatR;

namespace Legacy.Application.Services.Inventory;

public class UpdateStockRequest : IRequest
{
    public int ItemId { get; set; }
    public int WarehouseId { get; set; }
    public int AdjustedQuantity { get; set; }
    public string Note { get; set; }
}
