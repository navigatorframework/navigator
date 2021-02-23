using System.Threading.Tasks;
using MediatR;
using Navigator.Context;

namespace Navigator.Actions
{
    public interface IAction : IRequest
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
        /// Can be used to populate the action before triggering "CanHandle".
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        IAction Init(INavigatorContext ctx);
        
        /// <summary>
        /// This function must return true when the incoming update can be handled by this action.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        bool CanHandle(INavigatorContext ctx);
    }
}