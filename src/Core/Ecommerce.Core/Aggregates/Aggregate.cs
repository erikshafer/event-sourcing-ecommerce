namespace Ecommerce.Core.Aggregates;

[Obsolete("At this time use the Aggregate from Eventuous instead.")]
public abstract class Aggregate : Aggregate<Guid>, IAggregate
{
}

[Obsolete("At this time use the Aggregate from Eventuous instead.")]
public abstract class Aggregate<T> : IAggregate<T> where T : notnull
{
    public T Id { get; set; } = default!;

    public int Version { get; protected set; }

    [NonSerialized] private readonly Queue<object> _uncommittedEvents = new();

    public virtual void When(object @event) { }

    public object[] DequeueUncommittedEvents()
    {
        var dequeuedEvents = _uncommittedEvents.ToArray();

        _uncommittedEvents.Clear();

        return dequeuedEvents;
    }

    protected void Enqueue(object @event)
    {
        _uncommittedEvents.Enqueue(@event);
    }
}
