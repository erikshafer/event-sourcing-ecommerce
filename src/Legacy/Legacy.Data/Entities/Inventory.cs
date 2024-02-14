namespace Legacy.Data.Entities;

public class Inventory : AuditableEntity
{
    public int Id { get; set; }

    public int Available { get; set; }
}
