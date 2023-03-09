using Catalog.Items.Products;
using FluentAssertions;
using Marten;
using NSubstitute;
using Xunit;

namespace Catalog.Items.Tests.Products;

public class DraftingProductHandlerTests : IAsyncLifetime
{
    private Product _theProduct = default!;

    private readonly IDocumentSession _theDocumentSession = Substitute.For<IDocumentSession>();

    public async Task InitializeAsync()
    {
        var id = Guid.NewGuid();
        var sku = "ABC123";
        var brandId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();

        var command = new DraftProduct(id, sku, brandId, categoryId);
        var @event = new ProductDrafted(id, sku, brandId, categoryId);
        _theProduct = new Product(@event);

        var session = Substitute.For<IDocumentSession>();

        await DraftProductHandler.Handle(command , session);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void the_status_should_be_drafted()
    {
        _theProduct.Status.Should().Be(ProductStatus.Drafted);
    }
}
