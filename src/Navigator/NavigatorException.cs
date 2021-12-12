using System;
using System.Runtime.Serialization;

namespace Navigator;

/// <summary>
/// TODO
/// </summary>
public class NavigatorException : Exception
{
    /// <inheritdoc />
    public NavigatorException()
    {
    }

    /// <inheritdoc />
    protected NavigatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <inheritdoc />
    public NavigatorException(string? message) : base(message)
    {
    }

    /// <inheritdoc />
    public NavigatorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}