namespace Legacy.Domain.Entities;

public class AuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
