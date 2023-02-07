using Ecommerce.Core.Aggregates;
using Marten;

namespace Ecommerce.Core.Marten.Repositories;

public interface IMartenRepository<T> where T : class, IAggregate
{
#pragma warning disable VSTHRD200
    Task<T?> Find(Guid id, CancellationToken cancellationToken);
    Task<long> Add(T aggregate, CancellationToken cancellationToken = default);
    Task<long> Update(T aggregate, long? expectedVersion = null, CancellationToken cancellationToken = default);
    Task<long> Delete(T aggregate, long? expectedVersion = null, CancellationToken cancellationToken = default);
#pragma warning restore VSTHRD200
}

public class MartenRepository<T>: IMartenRepository<T> where T : class, IAggregate
{
    private readonly IDocumentSession _documentSession;

    public MartenRepository(IDocumentSession documentSession) =>
        _documentSession = documentSession;

    public Task<T?> Find(Guid id, CancellationToken ct) =>
        _documentSession.Events.AggregateStreamAsync<T>(id, token: ct);

    public async Task<long> Add(T aggregate, CancellationToken ct = default)
    {
        var events = aggregate.DequeueUncommittedEvents();

        _documentSession.Events.StartStream<Aggregate>(
            aggregate.Id,
            events);

        await _documentSession.SaveChangesAsync(ct).ConfigureAwait(false);

        return events.Length;
    }

    public async Task<long> Update(T aggregate, long? expectedVersion = null, CancellationToken ct = default)
    {
        var events = aggregate.DequeueUncommittedEvents();

        var nextVersion = (expectedVersion ?? aggregate.Version) + events.Length;

        _documentSession.Events.Append(
            aggregate.Id,
            nextVersion,
            events);

        await _documentSession.SaveChangesAsync(ct).ConfigureAwait(false);

        return nextVersion;
    }

    public Task<long> Delete(T aggregate, long? expectedVersion = null, CancellationToken ct = default) =>
        Update(aggregate, expectedVersion, ct);
}
