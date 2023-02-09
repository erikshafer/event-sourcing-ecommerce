using Ecommerce.Catalog.Products;
using Ecommerce.Domain.Values;
using FluentAssertions;
using Marten;
using NSubstitute;
using Wolverine.Marten;
using Xunit;

namespace Ecommerce.Catalog.Tests.Products;

public class DraftingProductHandlerTests : IAsyncLifetime
{
    private Product _theProduct = default!;

    private readonly IDocumentSession _theDocumentSession = Substitute.For<IDocumentSession>();

    public async Task InitializeAsync()
    {
        var id = new ProductId(Guid.NewGuid());
        var sku = Sku.From("ABC123");

        var command = new DraftProduct(id, sku);
        var @event = new ProductDrafted(id, sku);
        _theProduct = new Product(@event);

        var session = Substitute.For<IDocumentSession>();
        var outbox = Substitute.For<IMartenOutbox>();

        await DraftProductHandler.Handle(command , session, outbox, default);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void the_tags_should_be_empty()
    {
        _theProduct.Tags.Should().BeEmpty();
    }

    [Fact]
    public void the_status_should_be_drafted()
    {
        _theProduct.Status.Should().Be(ProductStatus.Drafted);
    }
}
