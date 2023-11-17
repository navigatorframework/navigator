using System.Runtime.Serialization;

namespace Navigator.Extensions.Store;

/// <summary>
/// Navigator Store exception.
/// </summary>
public class NavigatorStoreException : NavigatorException
{
    /// <inheritdoc />
    public NavigatorStoreException()
    {
    }

    /// <inheritdoc />
    protected NavigatorStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <inheritdoc />
    public NavigatorStoreException(string? message) : base(message)
    {
    }

    /// <inheritdoc />
    public NavigatorStoreException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}