using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions;
using Navigator.Extensions.Store.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Extensions.Store.Abstractions.Entity.Chat;
using User = Navigator.Extensions.Store.Abstractions.Entity.User;

namespace Navigator.Extensions.Store.Provider
{
    public class UpdateDataContextProvider<TUser, TChat> : INavigatorContextExtensionProvider
        where TUser : User
        where TChat : Chat
    {
        public int Order => 250;

        private readonly ILogger<UpdateDataContextProvider<TUser, TChat>> _logger;
        private readonly IEntityManager<TUser, TChat> _entityManager;

        public UpdateDataContextProvider(ILogger<UpdateDataContextProvider<TUser, TChat>> logger, IEntityManager<TUser, TChat> entityManager)
        {
            _logger = logger;
            _entityManager = entityManager;
        }

        public async Task<(string?, object?)> Process(Update update)
        {
            if (update.Type == UpdateType.Message || update.Type == UpdateType.ChannelPost)
            {
                switch (update.Message.Type)
                {
                    case MessageType.ChatMembersAdded:
                        await AddMembers(update);
                        break;
                    case MessageType.ChatMemberLeft:
                        await _entityManager.ChatMemberLeft(update.Message);
                        break;
                    case MessageType.ChatTitleChanged:
                        await _entityManager.ChatTitleChanged(update.Message);
                        break;
                    case MessageType.MigratedToSupergroup:
                        await _entityManager.MigrateToSupergroup(update.Message);
                        break;
                    case MessageType.MigratedFromGroup:
                        await _entityManager.MigrateFromGroup(update.Message);
                        break;
                }
            }

            return default;
        }

        private async Task AddMembers(Update update)
        {
            foreach (var newUser in update.Message.NewChatMembers)
            {
                await _entityManager.Handle(newUser, update.Message.Chat);
            }
        }
    }
}