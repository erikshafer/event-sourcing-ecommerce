namespace Ecommerce.Core.Exceptions;

public class ProjectionNotFoundException : DomainException
{
    private ProjectionNotFoundException(string typeName, string id) 
        : base($"{typeName} with id '{id}' was not found")
    {
    }

    public static ProjectionNotFoundException For<T>(Guid id) => 
        For<T>(id.ToString());

    public static ProjectionNotFoundException For<T>(string id) => 
        new(typeof(T).Name, id);
}