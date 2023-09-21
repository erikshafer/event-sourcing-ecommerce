using Legacy.Monolith.Core.Entities.Models;

namespace Legacy.Monolith.Orders.Entities.Models;

public class Payment : AuditableEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public bool Completed { get; set; }
}
