namespace Navigator.Actions;

/// <summary>
/// Represents the outcome of an action.
/// </summary>
public readonly struct Status
{
    private readonly bool _isSuccess;

    /// <summary>
    /// Is success.
    /// </summary>
    public bool IsSuccess => _isSuccess;
    
    /// <summary>
    /// Is error.
    /// </summary>
    public bool IsError => !_isSuccess;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="isSuccess"></param>
    public Status(bool isSuccess)
    {
        _isSuccess = isSuccess;
    }
}