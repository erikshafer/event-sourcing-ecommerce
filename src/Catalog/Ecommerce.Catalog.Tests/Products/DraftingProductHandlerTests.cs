using Ecommerce.Catalog.Products;
using FluentAssertions;
using Marten;
using NSubstitute;
using Wolverine.Marten;
using Xunit;

namespace Ecommerce.Catalog.Tests.Products;

public class DraftingProductHandlerTests : IAsyncLifetime
{
    private static readonly Guid _productId = Guid.NewGuid();
    private static readonly Sku _sku = new Sku("ABC123");
    private static readonly Brand brand = new Brand(10_001, "Acme Inc");
    private static readonly string _name = "Instant Tunnel";
    private readonly DraftProduct _command = new DraftProduct(_productId, _sku, brand, _name);
    private Product _theProduct = default!;

    private readonly IDocumentSession _theDocumentSession = Substitute.For<IDocumentSession>();

    public async Task InitializeAsync()
    {
        var productId = Guid.NewGuid();
        var sku = new Sku("ABC123");
        var brand = new Brand(10_001, "Acme Inc");
        var name = "Instant Tunnel";

        var @event = new ProductDrafted(productId, sku, brand, name);
        _theProduct = new Product(@event);

        var session = Substitute.For<IDocumentSession>();
        var outbox = Substitute.For<IMartenOutbox>();

        await DraftProductHandler.Handle(_command , session, outbox, default);
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
