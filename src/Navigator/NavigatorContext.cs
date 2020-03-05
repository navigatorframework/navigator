using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Abstraction.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator
{
    public class NavigatorContext<TCUser, TCChat> : INavigatorContext
    {
        protected readonly ILogger<NavigatorContext<TCUser, TCChat>> Logger;
        protected readonly IUserHandler<TCUser> UserHandler;
        protected readonly IChatHandler<TCChat> ChatHandler;
        public INavigatorClient Client { get; }
        public Message Message { get; protected set; }
        public InlineQuery InlineQuery { get; protected set; }
        public CallbackQuery CallbackQuery { get; protected set; }
        public ChosenInlineResult ChosenInlineResult { get; protected set; }
        public Update Update { get; protected set; }
        public string BotName { get; set; }
        public Dictionary<string, INavigatorExtension> Extensions { get; set; }
        
        public NavigatorContext(ILogger<NavigatorContext<TCUser, TCChat>> logger, IUserHandler<TCUser> userHandler, IChatHandler<TCChat> chatHandler, INavigatorClient client)
        {
            Logger = logger;
            UserHandler = userHandler;
            ChatHandler = chatHandler;
            Client = client;
        }

        public async Task<TUser> GetUser<TUser>() where TUser : class
        {
            var user = await UserHandler.Find(Message.From);
            return user as TUser;
        }

        public async Task<TChat> GetChat<TChat>() where TChat : class
        {
            var user = await ChatHandler.Find(Message.Chat);
            return user as TChat;
        }

        public async Task Populate(InlineQuery inlineQuery)
        {
            InlineQuery = inlineQuery;
            await UserHandler.Handle(inlineQuery.From);

            var bot = await Client.GetMeAsync();
            BotName = bot.Username;

            Logger.LogDebug("Populated NavigatorContext with InlineQuery: {@InlineQuery}.", inlineQuery);
        }
        
        public async Task Populate(CallbackQuery callbackQuery)
        {
            CallbackQuery = callbackQuery;
            await UserHandler.Handle(callbackQuery.From);

            var bot = await Client.GetMeAsync();
            BotName = bot.Username;

            Logger.LogDebug("Populated NavigatorContext with CallbackQuery: {@CallbackQuery}.", callbackQuery);
        }
        
        public async Task Populate(ChosenInlineResult chosenInlineResult)
        {
            ChosenInlineResult = chosenInlineResult;
            await UserHandler.Handle(chosenInlineResult.From);

            var bot = await Client.GetMeAsync();
            BotName = bot.Username;

            Logger.LogDebug("Populated NavigatorContext with ChosenInlineResult: {@ChosenInlineResult}.", chosenInlineResult);
        }

        public async Task Populate(Message message)
        {
            Message = message;

            await UserHandler.Handle(message.From, message.Chat);
            await ChatHandler.Handle(message.Chat, message.From);

            var bot = await Client.GetMeAsync();
            BotName = bot.Username;

            Logger.LogDebug("Populated NavigatorContext with Message: {@Message}.", message);
        }
        
        public async Task Populate(Update update)
        {
            Update = update;

            if (update.Type == UpdateType.Message || update.Type == UpdateType.EditedMessage)
            {
                await Populate(update.Message);
            } 
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await Populate(update.CallbackQuery);
            } 
            else if (update.Type == UpdateType.InlineQuery)
            {
                await Populate(update.InlineQuery);
            }
            else if (update.Type == UpdateType.ChosenInlineResult)
            {
                await Populate(update.ChosenInlineResult);
            }

            var bot = await Client.GetMeAsync();
            BotName = bot.Username;

            Logger.LogDebug("Populated NavigatorContext with Update: {@Update}.", update);
        }
    }
    
    public class DefaultChatHandler : IChatHandler<Chat>
    {
        public async Task<Chat> Find(Chat telegramChat, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;

            return telegramChat;
        }

        public async Task Handle(Chat telegramChat, User telegramUser, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
    
    public class DefaultUserHandler : IUserHandler<User>
    {
        public async Task<User> Find(User telegramUser, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;

            return telegramUser;
        }

        public async Task Handle(User telegramUser, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }

        public async Task Handle(User telegramUser, Chat telegramChat, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}