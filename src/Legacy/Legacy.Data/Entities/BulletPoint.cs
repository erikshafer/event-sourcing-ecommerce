namespace Legacy.Data.Entities;

public class BulletPoint : AuditableEntity
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int Position { get; set; }

    public string Value { get; set; } = default!;

    public BulletPoint()
    {
    }

    public BulletPoint(int itemId, int position, string value)
    {
        if (position < 0)
            throw new ArgumentOutOfRangeException(nameof(position));

        ItemId = itemId;
        Position = position;
        Value = value;
    }

    public override string ToString()
    {
        return $"Id: {Id} - ItemId: {ItemId} - Position: {Position} - Value: {Value}";
    }
}
