namespace Ecommerce.Core.Aggregates;

[Obsolete("At this time use the Aggregate from Eventuous instead.")]
public interface IAggregate : IAggregate<Guid>
{
}

[Obsolete("At this time use the Aggregate from Eventuous instead.")]
public interface IAggregate<out T>
{
    T Id { get; }
    int Version { get; }

    object[] DequeueUncommittedEvents();
}
