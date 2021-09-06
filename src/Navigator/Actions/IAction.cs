using MediatR;
using Navigator.Context;

namespace Navigator.Actions
{
    /// <summary>
    /// Base contract for an action.
    /// </summary>
    public interface IAction : IRequest<Status>
    {
        /// <summary>
        /// Type of the action.
        /// </summary>
        string Type { get; }
        
        /// <summary>
        /// Priority of the action when launching multiple actions.
        /// </summary>
        ushort Priority { get; }
        
        /// <summary>
        /// This function must return true when the incoming update can be handled by this action.
        /// </summary>
        /// <returns></returns>
        bool CanHandleCurrentContext();
    }
}