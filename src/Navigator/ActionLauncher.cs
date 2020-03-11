﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Abstraction;
using Navigator.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator
{
    public class ActionLauncher : IActionLauncher
    {
        protected readonly ILogger<ActionLauncher> Logger;
        protected readonly IMediator Mediator;
        protected readonly bool MultipleLaunchEnabled;
        protected readonly IEnumerable<IAction> Actions;

        public ActionLauncher(ILogger<ActionLauncher> logger, IOptions<NavigatorOptions> options, IMediator mediator, IEnumerable<IAction> actions)
        {
            Logger = logger;
            MultipleLaunchEnabled = options.Value.MultipleLaunchEnabled;
            Mediator = mediator;
            Actions = actions;
        }

        public async Task Launch(Update update)
        {
            Logger.LogTrace("Launching of multiple actions is {MultipleActionLaunchState}.", MultipleLaunchEnabled ? "active" : "inactive");
            Logger.LogTrace("Starting with action launching.");

            var actions = GetActions(update);

            Logger.LogTrace("Found a total of {NumberOfActionsFound} actions to launch.");

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

            if (MultipleLaunchEnabled)
            {
                actions = Actions.Where(a => a.Type == actionType).ToList();
            }
            else
            {
                actions.Add(Actions.Where(a => a.Type == actionType).OrderBy(a => a.Order).FirstOrDefault());
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
                _ => null
            };
        }
    }
}