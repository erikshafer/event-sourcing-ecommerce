namespace Legacy.Domain.Entities;

public class Promotion : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsDiscountPercentage { get; set; } = false;
    public float? DiscountPercentage { get; set; }

    public bool IsFlatDiscount { get; set; } = false;
    public decimal? FlatDiscount { get; set; }

    public decimal MinimumSpending { get; set; }
    public decimal MaximumDiscount { get; set; }

    public bool? Claimable { get; set; }
    public bool HasBeenClaimed { get; set; }
    public int? ClaimedByCustomerId { get; set; }
    public Customer ClaimedByCustomer { get; set; }
}
