using MediatR;

namespace Navigator.Abstraction.Actions
{
    public interface IActionHandler : IRequestHandler<IAction>
    {
        INavigatorContext NavigatorContext { get; }
    }
}