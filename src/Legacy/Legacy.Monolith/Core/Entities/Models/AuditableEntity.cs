namespace Legacy.Monolith.Core.Entities.Models;

public class AuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
