using Catalog.Products;
using Ecommerce.Core.Testing;
using FluentAssertions;
using Xunit;

namespace Catalog.Tests.Products;

public class DraftingProductTests
{
    [Fact]
    public void ForValidParams_ShouldCreateProductWithDraftStatus()
    {
        // Given
        var id = Guid.NewGuid();
        var sku = "ABC123";
        var @event = new ProductDrafted(id, sku);

        // When
        var product = new Product(@event);

        // Then
        product
            .IsDraftedProductWith(id, sku)
            .HasProductDraftedEventWith(id, sku);
    }
}

public static class ProductExtensions
{
    public static Product IsDraftedProductWith(
        this Product product,
        Guid id,
        string sku)
    {
        product.Id.Should().Be(id);
        product.Sku.Should().Be(sku);
        product.Status.Should().Be(ProductStatus.Drafted);

        return product;
    }

    public static Product HasProductDraftedEventWith(
        this Product product,
        Guid id,
        string sku)
    {
        var @event = product.PublishedEvent<ProductDrafted>();

        @event.Should().NotBeNull();
        @event.Should().BeOfType<ProductDrafted>();

        @event!.ProductId.Should().Be(id);
        @event.Sku.Should().Be(sku);

        return product;
    }
}
