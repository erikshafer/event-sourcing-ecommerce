namespace Ecommerce.LegacyCatalog.Entities.Models;

public class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string CountryOrigin { get; set; } = "US";

    public string? Phone { get; set; }

    public string? PrimaryContactName { get; set; }

    public string? PrimaryContactPhone { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
}
