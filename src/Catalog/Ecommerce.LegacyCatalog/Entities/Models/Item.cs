namespace Ecommerce.LegacyCatalog.Entities.Models;

public class Item
{
    public int Id { get; set; }

    public string Sku() => Id.ToString();

    public string Name { get; set; } = default!;

    public int BrandId { get; set; }
    public string BrandName { get; set; } = default!;

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;

    public int CategoryParentId { get; set; }
    public string CategoryParentName { get; set; } = default!;

    public bool IsVariant { get; set; }
    public int? IsVariantOf { get; set; }

    public bool DoNotSell { get; set; } = true;

    public string WeightUnit { get; set; } = "lbs";
    public decimal? Weight { get; set; }

    public string MeasureUnit { get; set; } = "in";
    public decimal? Height { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
}
