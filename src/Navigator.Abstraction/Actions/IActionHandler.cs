using MediatR;

namespace Navigator.Core.Abstractions.Actions
{
    public interface IActionHandler : IRequestHandler<IAction>
    {
        INavigatorContext NavigatorContext { get; }
    }
}