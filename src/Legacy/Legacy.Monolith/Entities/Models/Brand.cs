namespace Legacy.Monolith.Entities.Models;

public class Brand : AuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string PrimaryContactName { get; set; } = default!;

    public string PrimaryContactEmail { get; set; } = default!;
}
