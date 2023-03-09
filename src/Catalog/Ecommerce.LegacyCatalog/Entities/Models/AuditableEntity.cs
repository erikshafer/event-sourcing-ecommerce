namespace Ecommerce.LegacyCatalog.Entities.Models;

public class AuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
