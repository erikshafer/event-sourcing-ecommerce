namespace Legacy.Domain.Entities;

public class Item : AuditableEntity
{
    public int Id { get; set; }

    public string Sku() => Id.ToString();

    public string Name { get; set; } = default!;
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = default!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public bool IsVariant { get; set; }
    public int? IsVariantOf { get; set; }
    public bool Discontinued { get; set; }
    public string Description { get; set; } = default!;
    public string WeightUnit { get; set; } = "lbs";
    public decimal? Weight { get; set; }
    public string MeasureUnit { get; set; } = "in";
    public decimal? Height { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public string Color { get; set; }
    public string Color2 { get; set; }
    public string BulletPoint1 { get; set; } = default!;
    public string BulletPoint2 { get; set; } = default!;
    public string BulletPoint3 { get; set; } = default!;
    public string WarningCode1 { get; set; }
    public string WarningCode2 { get; set; }
    public string WarningCode3 { get; set; }
    public bool ChildCouldChokeWarning { get; set; }
    public string Picture1Url { get; set; }
    public string Picture2Url { get; set; }
    public string Picture3Url { get; set; }
}
