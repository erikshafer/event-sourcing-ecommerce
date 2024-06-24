namespace Legacy.Domain.Entities;

public class Customer : AuditableEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int ShippingAddressId { get; set; }
    // public Address ShippingAddress { get; set; }
    public int BillingAddressId { get; set; }
    // public Address BillingAddress { get; set; }
}
