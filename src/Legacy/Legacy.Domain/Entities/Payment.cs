namespace Legacy.Domain.Entities;

public class Payment : AuditableEntity
{
    public int Id { get; set; }
    public bool Completed { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public decimal TotalAmount { get; set; }
}
