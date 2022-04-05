using Navigator.Actions.Attributes;
using Navigator.Context;

namespace Navigator.Actions;

/// <summary>
/// Base action for provider agnostic actions.
/// </summary>
[ActionType(nameof(ProviderAgnosticAction))]
public abstract class ProviderAgnosticAction : BaseAction
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="navigatorContextAccessor"></param>
    protected ProviderAgnosticAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
}