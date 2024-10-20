namespace Navigator.Abstractions;

/// <summary>
/// Navigator exception.
/// </summary>
public class NavigatorException : Exception
{
    /// <inheritdoc />
    public NavigatorException()
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