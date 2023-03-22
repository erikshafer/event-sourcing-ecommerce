namespace Catalog.Categories.Categories;

public sealed class Category
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public string Code { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public Guid ParentId { get; private set; }

    public Category()
    {
    }

    public Category(Guid id, string name, string code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public Category(Guid id, string name, string code, string description = "")
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
    }
}
