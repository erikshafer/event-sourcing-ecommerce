namespace Ecommerce.Core.Aggregates;

public interface IAggregateWithId : IAggregate
{
}

public interface IAggregate : IAggregate<Guid>
{
}

public interface IAggregate<out T>
{
    T Id { get; }
    int Version { get; }

    object[] DequeueUncommittedEvents();
}
