namespace Legacy.Domain.Entities;

public class InventoryHistory : AuditableEntity
{
    public long Id { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int AdjustedQuantity { get; set; }
    public int ReportedTotal { get; set; }
    public string Note { get; set; }
}
