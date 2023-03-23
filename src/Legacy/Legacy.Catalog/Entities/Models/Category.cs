namespace Legacy.Catalog.Entities.Models;

public class Category : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int? ParentId { get; set; }

    public Category()
    {
    }

    public Category(int id, string name, string code, string description = "", int? parentId = null)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
        ParentId = parentId;
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException();

        Name = name;
    }

    public void ChangeCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new InvalidOperationException();

        Code = code;
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidOperationException();

        Description = description;
    }

    public void ChangeParentId(int? parentId)
    {
        if (parentId is < 0)
            throw new InvalidOperationException();

        ParentId = parentId;
    }

    public override string ToString()
    {
        return $"{Code} - {Name}";
    }
}
