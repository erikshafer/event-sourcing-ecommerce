namespace Legacy.Data.Entities;

public class Marketplace : AuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public bool IsActive { get; set; }
}
