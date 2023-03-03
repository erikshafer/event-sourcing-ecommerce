namespace Ecommerce.LegacyCatalog.Entities.Models;

public class Content
{
    public int Id { get; set; }
    public int ItemId { get; set; }

    public string? Color { get; set; }
    public string? Color2 { get; set; }

    public string? BulletPoint1 { get; set; }
    public string? BulletPoint2 { get; set; }
    public string? BulletPoint3 { get; set; }
    public string? BulletPoint4 { get; set; }
    public string? BulletPoint5 { get; set; }

    public string? WarningCode { get; set; }
    public string? WarningCode2 { get; set; }
    public string? WarningCode3 { get; set; }

    public bool ChildCouldChokeWarning { get; set; } = false;

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
}
