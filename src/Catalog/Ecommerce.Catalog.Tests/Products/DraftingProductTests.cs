using Ecommerce.Catalog.Products;
using Ecommerce.Core.Testing;
using Ecommerce.Domain.Values;
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
        var sku = Sku.From("ABC123");
        var brand = new BrandId(10_001);
        var @event = new ProductDrafted(id, sku, brand);

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
        BrandId brandId)
    {
        product.Id.Should().Be(id);
        product.Sku.Should().Be(sku);
        product.BrandId.Should().Be(brandId);
        product.Status.Should().Be(ProductStatus.Drafted);
        product.Tags.Should().BeEmpty();

        return product;
    }

    public static Product HasProductDraftedEventWith(
        this Product product,
        ProductId id,
        Sku sku,
        BrandId brandId)
    {
        var @event = product.PublishedEvent<ProductDrafted>();

        @event.Should().NotBeNull();
        @event.Should().BeOfType<ProductDrafted>();

        @event!.ProductId.Should().Be(id);
        @event.Sku.Should().Be(sku);
        @event.BrandId.Should().Be(brandId);

        return product;
    }
}
