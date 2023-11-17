namespace Navigator.Context.Accessor;

/// <summary>
/// Accessor for <see cref="INavigatorContext"/>.
/// </summary>
public interface INavigatorContextAccessor
{
    /// <summary>
    /// Navigator Context.
    /// </summary>
    INavigatorContext NavigatorContext { get; }
}