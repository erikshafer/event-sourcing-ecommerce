namespace Legacy.Data.Entities;

public class Address : AuditableEntity
{
    public int Id { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string CountryId { get; set; }

    public Country Country { get; set; }

    public string Phone { get; set; }
}
