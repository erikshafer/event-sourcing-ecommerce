namespace Legacy.Catalog.Entities.Models;

public class Restriction : AuditableEntity
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public bool IsRestricted { get; set; } = true;
    public string? Reason { get; set; }
}
