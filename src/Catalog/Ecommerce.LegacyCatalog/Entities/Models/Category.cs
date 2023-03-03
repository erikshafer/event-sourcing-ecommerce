namespace Ecommerce.LegacyCatalog.Entities.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int ParentId { get; set; }
    public string ParentName { get; set; } = default!;

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
}
