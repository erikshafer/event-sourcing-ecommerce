using Ecommerce.Core.Aggregates;

namespace Ecommerce.Core.Testing;

public static class AggregateExtensions
{
#pragma warning disable CS0618 // Type or member is obsolete
    public static T? PublishedEvent<T>(this IAggregate aggregate) where T : class
#pragma warning restore CS0618 // Type or member is obsolete
    {
        return aggregate.DequeueUncommittedEvents().LastOrDefault() as T;
    }
}
