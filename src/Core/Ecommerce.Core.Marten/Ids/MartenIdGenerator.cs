using Ecommerce.Core.Ids;
using Marten;
using Marten.Schema.Identity;

namespace Ecommerce.Core.Marten.Ids;

public class MartenIdGenerator : IIdGenerator
{
    private readonly IDocumentSession _documentSession;

    public MartenIdGenerator(IDocumentSession documentSession)
    {
        _documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
    }

    public Guid New() => CombGuidIdGeneration.NewGuid();
}
