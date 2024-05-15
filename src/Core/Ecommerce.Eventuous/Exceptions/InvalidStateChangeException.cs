using Eventuous;

namespace Ecommerce.Eventuous.Exceptions;

/// <summary>
/// TODO - expand to be easier to use, less syntax if possible, more railguard-like.
/// </summary>
public class InvalidStateChangeException(string message) : DomainException(message)
{
    private InvalidStateChangeException(string typeName, string id)
        : this($"{typeName} with id '{id}' could not perform an operation")
    {
    }

    private InvalidStateChangeException(string typeName, Id id, Enum requiredStatus)
        : this($"{typeName} with id '{id}' is required to be in {nameof(requiredStatus)} status")
    {
    }

    private InvalidStateChangeException(string typeName, Id id, Enum requiredStatus, string eventName)
        : this($"{typeName} with id '{id}' is required to be in {nameof(requiredStatus)} status in order to apply {eventName} event")
    {
    }

    public static InvalidStateChangeException For<T>(Id id) =>
        For<T>(id.ToString());

    public static InvalidStateChangeException For<T>(string id) =>
        new(typeof(T).Name, id);

    public static InvalidStateChangeException For<T>(Id id, Enum requiredStatus) =>
        new(typeof(T).Name, id, requiredStatus);

    public static InvalidStateChangeException For<T, TEvent>(Id id, Enum requiredStatus) =>
        new(typeof(T).Name, id, requiredStatus, typeof(TEvent).Name);
}
