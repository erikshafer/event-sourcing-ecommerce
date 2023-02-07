using System.Runtime.Serialization;

namespace Ecommerce.Core.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException()
    {
    }

    protected DomainException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }

    protected DomainException(string? message)
        : base(message)
    {
    }

    protected DomainException(string? message, Exception? innerException) 
        : base(message, innerException)
    {
    }
}