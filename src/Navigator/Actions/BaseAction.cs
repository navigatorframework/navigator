using MediatR;
using Navigator.Context;

namespace Navigator.Actions
{
    public abstract class Action : IRequest
    {
        public string Type { get; protected set; }
        f
        protected readonly INavigatorContext NavigatorContext;
        
        public Action(INavigatorContextAccessor navigatorContextAccessor)
        {
            NavigatorContext = navigatorContextAccessor.NavigatorContext;
        }
    }
}