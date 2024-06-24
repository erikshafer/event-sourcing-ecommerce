namespace Legacy.Domain.Entities;

public class ListingError
{
    public int Id { get; set; }

    public int ListingId { get; set; } // no entity here

    public int ItemId { get; set; } // no entity here

    public int MarketplaceId { get; set; } // no entity here

    public string ErrorId { get; set; }

    public string ErrorMessage { get; set; }

    public string WarningId { get; set; }

    public string WarningMessage { get; set; }
}
