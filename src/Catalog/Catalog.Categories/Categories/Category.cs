using Ecommerce.Core.Aggregates;

namespace Catalog.Categories.Categories;

public sealed class Category : Aggregate
{
    public string Name { get; private set; } = default!;

    public string Code { get; private set; } = default!;

    public Guid ParentId { get; private set; } = Guid.Empty;

    public CategoryStatus Status { get; private set; } = CategoryStatus.Unset;

    public Category()
    {
    }

    public Category(CategoryDrafted @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(CategoryDrafted @event)
    {
        Id = @event.CategoryId;
        Name = @event.Name;
        Code = @event.Code;

        Status = CategoryStatus.Drafted;
    }
}
