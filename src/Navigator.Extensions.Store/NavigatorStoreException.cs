using System.Runtime.Serialization;

namespace Navigator.Extensions.Store;

public class NavigatorStoreException : NavigatorException
{
    public NavigatorStoreException()
    {
    }

    protected NavigatorStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NavigatorStoreException(string? message) : base(message)
    {
    }

    public NavigatorStoreException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}