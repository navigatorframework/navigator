using System.Threading.Tasks;
using MediatR;
using Navigator.Context;

namespace Navigator.Actions
{
    public interface IAction : IRequest
    {
        string Type { get; }
        
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