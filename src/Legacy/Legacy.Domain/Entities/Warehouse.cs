namespace Legacy.Domain.Entities;

public class Warehouse : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
}
