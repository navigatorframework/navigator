using MediatR;

namespace Navigator.Actions;

/// <summary>
/// Base contract for an action.
/// </summary>
public interface IAction : IRequest<Status>
{
    /// <summary>
    /// This function must return true when the incoming update can be handled by this action.
    /// </summary>
    /// <returns></returns>
    bool CanHandleCurrentContext();
}