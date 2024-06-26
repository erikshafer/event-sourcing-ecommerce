namespace Legacy.Domain.Entities;

public class Restriction : AuditableEntity
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public bool IsRestricted { get; set; } = true;
    public string Reason { get; set; }
}
