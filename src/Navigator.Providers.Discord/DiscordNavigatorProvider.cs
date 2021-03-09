using System.Threading.Tasks;

namespace Navigator.Providers.Discord
{
    public class DiscordNavigatorProvider : INavigatorProvider
    {
        public INavigatorClient GetClient()
        {
            throw new System.NotImplementedException();
        }

        public Task HandleReply()
        {
            throw new System.NotImplementedException();
        }

        public string GetActionType(object original)
        {
            throw new System.NotImplementedException();
        }
    }
}