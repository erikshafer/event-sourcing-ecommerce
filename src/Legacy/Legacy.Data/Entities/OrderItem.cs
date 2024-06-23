namespace Legacy.Data.Entities;

public class OrderItem : AuditableEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; } = default!;
    public int Units { get; set; }

}
