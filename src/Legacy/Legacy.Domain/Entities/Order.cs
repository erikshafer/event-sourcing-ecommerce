namespace Legacy.Domain.Entities;

public class Order : AuditableEntity
{
    public int Id { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = null!;

    public OrderStatus Status { get; set; }

    public decimal TotalPrice { get; set; }

    public int PaymentId { get; set; }
    public Payment Payment { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public bool InProcess { get; set; }
    public bool IsCompleted { get; set; }

    public Order()
    {
        OrderItems = new List<OrderItem>();
        TotalPrice = decimal.Zero;
        Status = OrderStatus.Submitted;
        InProcess = false;
        IsCompleted = false;
    }
}

public enum OrderStatus
{
    Submitted = 1,
    Validating = 2,
    StockConfirmed = 4,
    Paid = 8,
    Shipped = 16,
    Cancelled = 32,
    Returned = 64
}
