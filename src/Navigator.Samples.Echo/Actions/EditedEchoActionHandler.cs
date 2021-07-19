using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;

namespace Navigator.Samples.Echo.Actions
{
    public class EditedEchoActionHandler : ActionHandler<EditedEchoAction>
    {
        public EditedEchoActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
        }

        public override async Task<Status> Handle(EditedEchoAction action, CancellationToken cancellationToken)
        {
            await NavigatorContext.Provider.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat(), 
                action.EditedMessage.Text, cancellationToken: cancellationToken);

            return Success();
        }
    }
}