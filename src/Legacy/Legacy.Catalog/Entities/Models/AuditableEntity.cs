namespace Legacy.Catalog.Entities.Models;

public class AuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
