namespace Legacy.Data.Entities;

public class Listing : AuditableEntity
{
    public int Id { get; set; }

    public int ItemId { get; set; }
    // public Item Item { get; set; }

    public int MarketplaceId { get; set; }
    // public Marketplace Marketplace { get; set; }

    public bool IsActive { get; set; }

    public bool IsActiveOnMarketplace { get; set; }

    public string Title { get; set; } = default!;

    public string DescriptionShort { get; set; } = default!;

    public string DescriptionLong { get; set; } = default!;

    public decimal? BuyItNowPrice { get; set; }

    public decimal? StartingPrice { get; set; }

    public decimal? ReservePrice { get; set; } = default!;

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}
