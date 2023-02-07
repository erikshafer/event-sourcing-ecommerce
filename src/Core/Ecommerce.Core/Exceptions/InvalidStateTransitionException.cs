namespace Ecommerce.Core.Exceptions;

public class InvalidStateTransitionException : DomainException
{
    private InvalidStateTransitionException(string typeName, string id, string currentState, string expectedState)
        : base($"{typeName} with id '{id}' could not transition from '{currentState}' state to '{expectedState}'")
    {
    }

    public static InvalidStateTransitionException For<T, TState>(Guid id, string currentState, string expectedState)
        where TState : Enum
    {
        return For<T, TState>(id.ToString(), currentState, expectedState);
    }

    public static InvalidStateTransitionException For<T, TState>(string id, string currentState, string expectedState)
        where TState : Enum
    {
        return new(typeof(T).Name, id, currentState, expectedState);
    }
}
