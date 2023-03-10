namespace Legacy.Catalog.Entities.Models;

public class Brand : AuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}
