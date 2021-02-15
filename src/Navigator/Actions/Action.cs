using MediatR;
using Navigator.Context;

namespace Navigator.Actions
{
    public abstract class Action : IRequest
    {
        protected readonly NavigatorContext NavigatorContext;

        public Action(INavigatorContextAccessor navigatorContextAccessor)
        {
            NavigatorContext = navigatorContextAccessor.NavigatorContext;
        }
    }
}