namespace Ecommerce.Core.Exceptions;

public class InvalidAggregateOperationException : DomainException
{
    private InvalidAggregateOperationException(string message): base(message)
    {
    }

    public static InvalidAggregateOperationException For(string message) =>
        new(message);

    private InvalidAggregateOperationException(string typeName, string id)
        : base($"{typeName} with id '{id}' could not perform an operation")
    {
    }

    public static InvalidAggregateOperationException For<T>(object id) =>
        For<T>(id.ToString()!);

    public static InvalidAggregateOperationException For<T>(Guid id) =>
        For<T>(id.ToString());

    public static InvalidAggregateOperationException For<T>(string id) =>
        new(typeof(T).Name, id);

    private InvalidAggregateOperationException(string typeName, string id, string operationName)
        : base($"{typeName} with id '{id}' could not perform '{operationName}' operation")
    {
    }

    public static InvalidAggregateOperationException For<T>(object id, string operationName) =>
        For<T>(id.ToString()!, operationName);

    public static InvalidAggregateOperationException For<T>(Guid id, string operationName) =>
        For<T>(id.ToString(), operationName);

    public static InvalidAggregateOperationException For<T>(string id, string operationName) =>
        new(typeof(T).Name, id, operationName);
}
