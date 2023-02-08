using Ecommerce.Catalog.Products;
using Ecommerce.Core.Testing;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Catalog.Tests.Products;

public class DraftingProductTests
{
    [Fact]
    public void ForValidParams_ShouldCreateProductWithDraftStatus()
    {
        // Given
        var id = new ProductId();
        var sku = new Sku("ABC123");
        var brand = new Brand(10_001, "Acme Inc");
        var name = "Instant Tunnel";

        var @event = new ProductDrafted(id, sku, brand, name);

        // When
        var product = new Product(@event);

        // Then
        product
            .IsDraftedProductWith(id, sku, brand)
            .HasProductDraftedEventWith(id, sku, brand);
    }
}

public static class ProductExtensions
{
    public static Product IsDraftedProductWith(
        this Product product,
        ProductId id,
        Sku sku,
        Brand brand)
    {
        product.Id.Should().Be(id);
        product.Sku.Should().Be(sku);
        product.Brand.Should().Be(brand);
        product.Status.Should().Be(ProductStatus.Drafted);
        product.Tags.Should().BeEmpty();

        return product;
    }

    public static Product HasProductDraftedEventWith(
        this Product product,
        ProductId id,
        Sku sku,
        Brand brand)
    {
        var @event = product.PublishedEvent<ProductDrafted>();

        @event.Should().NotBeNull();
        @event.Should().BeOfType<ProductDrafted>();

        @event!.ProductId.Should().Be(id);
        @event.Sku.Should().Be(sku);
        @event.Brand.Should().Be(brand);

        return product;
    }
}
