namespace Legacy.Monolith.Entities.Models;

public class Payment : AuditableEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public bool Completed { get; set; }
}
