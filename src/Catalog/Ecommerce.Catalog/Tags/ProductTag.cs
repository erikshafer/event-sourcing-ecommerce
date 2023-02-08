namespace Ecommerce.Catalog.Tags;

public record ProductTag(string Tag, int Ordering)
{
    public bool HasTheSameTag(ProductTag productTag) =>
        string.Equals(Tag, productTag.Tag, StringComparison.InvariantCulture);

    public bool HasTheSameOrdering(ProductTag productTag) =>
        Ordering == productTag.Ordering;

    public ProductTag ModifyTag(string tag) =>
        new(tag, Ordering);

    public ProductTag ModifyOrdering(int ordering) =>
        new(Tag, ordering);
}
