using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions;
using Navigator.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Old
{
    public class ActionLauncher : IActionLauncher
    {
        protected readonly ILogger<ActionLauncher> Logger;
        protected readonly IMediator Mediator;
        protected readonly bool MultipleActionsUsage;
        protected readonly IEnumerable<IAction> Actions;
        protected readonly INavigatorContext Ctx;

        public ActionLauncher(ILogger<ActionLauncher> logger, NavigatorOptions options, IMediator mediator, IEnumerable<IAction> actions, INavigatorContext navigatorContext)
        {
            Logger = logger;
            MultipleActionsUsage = options.MultipleActionsUsageIsEnabled();
            Mediator = mediator;
            Actions = actions;
            Ctx = navigatorContext;
        }

        public async Task Launch()
        {
            Logger.LogTrace("Launching of multiple actions is {MultipleActionLaunchState}.", MultipleActionsUsage ? "active" : "inactive");
            Logger.LogTrace("Starting with action launching.");

            var actions = GetActions(Ctx.Update).ToList();
            
            Logger.LogTrace("Found a total of {NumberOfActionsFound} actions to launch.", actions.Count);

            foreach (var action in actions)
            {
                try
                {                
                    Logger.LogTrace("Launching action {ActionName}", action.GetType().Name);

                    await Mediator.Send(action);
                    
                    Logger.LogTrace("Action {ActionName} successfully launched", action.GetType().Name);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Action {ActionName} finished launch with errors", action.GetType().Name);
                }
            }
        }

        public IEnumerable<IAction> GetActions(Update update)
        {
            var actions = new List<IAction>();
            var actionType = GetActionType(update);

            if (string.IsNullOrWhiteSpace(actionType))
            {
                return actions;
            }

            if (MultipleActionsUsage)
            {
                actions = Actions
                    .Where(a => a.Type == actionType)
                    .Where(a => a.Init(Ctx).CanHandle(Ctx))
                    .OrderBy(a => a.Order)
                    .ToList();
            }
            else
            {
                var action = Actions
                    .Where(a => a.Type == actionType)
                    .OrderBy(a => a.Order)
                    .FirstOrDefault(a => a.Init(Ctx).CanHandle(Ctx));

                if (action != null)
                {
                    actions.Add(action);
                }
            }

            return actions;
        }

        public string? GetActionType(Update update)
        {
            return update.Type switch
            {
                UpdateType.Message when update.Message.Entities?.First()?.Type == MessageEntityType.BotCommand => ActionType.Command,
                UpdateType.Message => update.Message.Type switch
                {
                    MessageType.ChatMembersAdded => ActionType.ChatMembersAdded,
                    MessageType.ChatMemberLeft => ActionType.ChatMemberLeft,
                    MessageType.ChatTitleChanged => ActionType.ChatTitleChanged,
                    MessageType.ChatPhotoChanged => ActionType.ChatPhotoChanged,
                    MessageType.MessagePinned => ActionType.MessagePinned,
                    MessageType.ChatPhotoDeleted => ActionType.ChatPhotoDeleted,
                    MessageType.GroupCreated => ActionType.GroupCreated,
                    MessageType.SupergroupCreated => ActionType.SupergroupCreated,
                    MessageType.ChannelCreated => ActionType.ChannelCreated,
                    MessageType.MigratedToSupergroup => ActionType.MigratedToSupergroup,
                    MessageType.MigratedFromGroup => ActionType.MigratedFromGroup,
                    _ => ActionType.Message
                },
                UpdateType.InlineQuery => ActionType.InlineQuery,
                UpdateType.ChosenInlineResult => ActionType.InlineResultChosen,
                UpdateType.CallbackQuery => ActionType.CallbackQuery,
                UpdateType.EditedMessage => ActionType.EditedMessage,
                UpdateType.ChannelPost => ActionType.ChannelPost,
                UpdateType.EditedChannelPost => ActionType.EditedChannelPost,
                UpdateType.ShippingQuery => ActionType.ShippingQuery,
                UpdateType.PreCheckoutQuery => ActionType.PreCheckoutQuery,
                UpdateType.Poll => ActionType.Poll,
                UpdateType.Unknown => ActionType.Unknown,
                _ => default
            };
        }
    }
}