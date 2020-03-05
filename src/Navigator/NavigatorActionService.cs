using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Abstraction.Actions;
using Navigator.Abstraction.Error;
using Navigator.Abstraction.Types;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator
{
    public class NavigatorActionService : INavigatorActionService
    {
        protected ILogger<NavigatorActionService> Logger;
        protected IEnumerable<IAction> BotActions;
        protected INavigatorContext NavigatorContext;
        protected INotificationParser NotificationParser;
        protected IMediator Mediator;

        public NavigatorActionService(ILogger<NavigatorActionService> logger, IEnumerable<IAction> botActions, INavigatorContext navigatorContext, INotificationParser notificationParser, IMediator mediator)
        {
            Logger = logger;
            BotActions = botActions;
            NavigatorContext = navigatorContext;
            NotificationParser = notificationParser;
            Mediator = mediator;
        }

        public virtual async Task Sail(Message message)
        {
            await Mediator.Publish(await NotificationParser.Parse(message));

            var action = GetAction(message);

            if (action != null)
            {
                await Mediator.Publish(action);
            }
        }

        public async Task Sail(CallbackQuery callbackQuery)
        {
            await Mediator.Publish(await NotificationParser.Parse(callbackQuery));

            var action = GetAction(callbackQuery);

            if (action != null)
            {
                await Mediator.Publish(action);
            }
        }

        public async Task Sail(InlineQuery inlineQuery)
        {
            await Mediator.Publish(await NotificationParser.Parse(inlineQuery));

            var action = GetAction(inlineQuery);

            if (action != null)
            {
                await Mediator.Publish(action);
            }
        }

        public async Task Sail(ChosenInlineResult chosenInlineResult)
        {
            await Mediator.Publish(await NotificationParser.Parse(chosenInlineResult));

            var action = GetAction(chosenInlineResult);

            if (action != null)
            {
                await Mediator.Publish(action);
            }
        }

        public async Task Sail(Update update)
        {
            await Mediator.Publish(await NotificationParser.Parse(update));

            var action = GetAction(update);

            if (action != null)
            {
                await Mediator.Publish(action);
            }
        }

        protected IAction GetAction<TAction>(TAction action)
        {
            try
            {
                var type = FindBotActionType(action);

                return BotActions.Where(a => a.Type == type).FirstOrDefault(a => a.Fill(NavigatorContext).CanHandle(NavigatorContext));
            }
            catch (Exception e)
            {
                const string errorMessage = "Unknown error searching for a corresponding action.";
                Logger.LogError(e, errorMessage);
                throw new NavigatorException(errorMessage, e);
            }
        }

        private static BotActionType FindBotActionType<TAction>(TAction action)
        {
            return action switch
            {
                CallbackQuery _ => BotActionType.CallbackQuery,
                InlineQuery _ => BotActionType.InlineQuery,
                ChosenInlineResult _ => BotActionType.InlineResultChosen,
                Message message when message.EditDate != default => BotActionType.MessageEdited,
                Message message when message.Entities?.First()?.Type == MessageEntityType.BotCommand => BotActionType.Command,
                Message _ => BotActionType.Text,
                Update _ => BotActionType.Update,
                _ => BotActionType.Other
            };
        }
    }
}