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


    public static InvalidAggregateOperationException Because<T>(object id, string operationName, string message) =>
        Because<T>(id.ToString()!, operationName, message);

    public static InvalidAggregateOperationException Because<T>(Guid id, string operationName, string message) =>
        Because<T>(id.ToString(), operationName, message);

    public static InvalidAggregateOperationException Because<T>(string id, string operationName, string message) =>
        new(typeof(T).Name, id, operationName, message);

    private InvalidAggregateOperationException(string typeName, string id, string operationName, string message)
        : base($"{typeName} with id '{id}' could not perform '{operationName}' operation due to '{message}'")
    {
    }
}
