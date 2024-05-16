using Eventuous;

namespace Ecommerce.Eventuous.Exceptions;

/// <summary>
/// TODO - expand to be easier to use, less syntax if possible, more railguard-like.
/// </summary>
public class InvalidStateChangeException(string message) : DomainException(message)
{
    private InvalidStateChangeException(string typeName, string id)
        : this($"{typeName} with id '{id}' could not perform an operation.")
    {
    }

    private InvalidStateChangeException(string typeName, string id, string eventName)
        : this($"{typeName} with id '{id}' could not successfully apply {eventName} event.")
    {
    }

    private InvalidStateChangeException(string typeName, Id id, string eventName)
        : this($"{typeName} with id '{id}' could not successfully apply {eventName} event.")
    {
    }

    private InvalidStateChangeException(string typeName, Id id, Enum invalidStatus)
        : this($"{typeName} with id '{id}' could not successfully apply an event while state is in {invalidStatus} status.")
    {
    }

    private InvalidStateChangeException(string typeName, Id id, Enum invalidStatus, string eventName)
        : this($"{typeName} with id '{id}' could not successfully apply {eventName} event while state is in {invalidStatus} status.")
    {
    }

    private InvalidStateChangeException(string typeName, Id id, string eventName, string messageDetail = null!)
        : this($"{typeName} with id '{id}' could not successfully apply {eventName} event | messageDetail: {messageDetail}")
    {
    }

    public static InvalidStateChangeException For<T>(Id id) =>
        For<T>(id.ToString());

    public static InvalidStateChangeException For<T>(string id) =>
        new(typeof(T).Name, id);

    public static InvalidStateChangeException For<T, TEvent>(Id id) =>
        new(typeof(T).Name, id, typeof(TEvent).Name);

    public static InvalidStateChangeException For<T>(Id id, Enum invalidStatus) =>
        new(typeof(T).Name, id, invalidStatus);

    public static InvalidStateChangeException For<T, TEvent>(Id id, Enum invalidStatus) =>
        new(typeof(T).Name, id, invalidStatus, typeof(TEvent).Name);

    public static InvalidStateChangeException For<T, TEvent>(Id id, string messageDetail) =>
        new(typeof(T).Name, id, typeof(TEvent).Name, messageDetail);
}
