namespace Navigator.Actions;

/// <summary>
/// Action launcher interface.
/// </summary>
public interface IActionLauncher
{
    /// <summary>
    /// Launches one or more actions.
    /// </summary>
    /// <returns></returns>
    Task Launch();
}