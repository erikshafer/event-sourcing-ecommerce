namespace Legacy.Data.Entities;

public class Inventory : AuditableEntity
{
    public int ItemId { get; set; }
    public int Available { get; set; }
}
