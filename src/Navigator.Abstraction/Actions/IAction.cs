using MediatR;
using Navigator.Abstraction.Types;

namespace Navigator.Abstraction.Actions
{
    public interface IAction : IRequest
    {
        BotActionType Type { get; }
        IAction Fill(INavigatorContext navigatorContext);
        bool CanHandle(INavigatorContext navigatorContext);
    }
}