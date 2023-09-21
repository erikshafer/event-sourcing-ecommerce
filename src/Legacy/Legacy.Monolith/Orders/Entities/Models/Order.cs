using Legacy.Monolith.Catalog.Entities.Models;
using Legacy.Monolith.Core.Entities.Models;

namespace Legacy.Monolith.Orders.Entities.Models;

public class Order : AuditableEntity
{
    public int Id { get; set; }

    public ICollection<Item> Items { get; set; }

    public decimal TotalPrice { get; set; }

    public int PaymentId { get; set; }
    public Payment Payment { get; set; }

    public bool Completed { get; set; }


    public Order()
    {
        Items = new List<Item>();
        TotalPrice = decimal.Zero;
        Completed = false;
    }
}
