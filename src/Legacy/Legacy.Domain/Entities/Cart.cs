namespace Legacy.Domain.Entities;

public class Cart : AuditableEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public bool IsLocked { get; set; }
    public int DeliveryAddressId { get; set; }
    public Address DeliveryAddress { get; set; }
}
