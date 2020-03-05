﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Navigator.Core.Abstractions;
using Navigator.Core.Abstractions.Error;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Navigator.Core
{
    public class NavigatorService : INavigatorService
    {
        protected readonly ILogger<NavigatorService> Logger;
        protected readonly INavigatorClient Navigator;
        protected readonly IServiceScopeFactory ServiceScopeFactory;

        public NavigatorService(ILogger<NavigatorService> logger, INavigatorClient navigator, IServiceScopeFactory serviceScopeFactory)
        {
            Logger = logger;
            Navigator = navigator;
            ServiceScopeFactory = serviceScopeFactory;
        }

        public async Task Start(CancellationToken stoppingToken = default)
        {
            Logger.LogInformation("Starting Navigator Service.");
            
            Navigator.OnCallbackQuery += HandleOnCallbackQuery;
            Navigator.OnInlineQuery += HandleOnInlineQuery;
            Navigator.OnInlineResultChosen += OnInlineResultChosen;
            Navigator.OnMessage += HandleOnMessage;
            Navigator.OnMessageEdited += HandleOnMessageEdited;
            Navigator.OnUpdate += HandleOnUpdate;
            
            await Navigator.Start(stoppingToken);
        }

        protected async void HandleOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            Logger.LogDebug("HandleOnCallbackQuery was triggered");
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();

                await InitializeNavigatorContext(scope, e.CallbackQuery);
                
                var actionClient = scope.ServiceProvider.GetRequiredService<INavigatorActionService>();
                await actionClient.Sail(e.CallbackQuery);
            }
            catch (NavigatorException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing CallbackQuery ({@CallbackQuery}).", e.CallbackQuery);
                throw new NavigatorException("Unhandled error processing CallbackQuery", exception);
            }
        }

        protected async void HandleOnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            Logger.LogDebug("HandleOnInlineQuery was triggered");
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();

                await InitializeNavigatorContext(scope, e.InlineQuery);
                
                var actionClient = scope.ServiceProvider.GetRequiredService<INavigatorActionService>();
                await actionClient.Sail(e.InlineQuery);
            }
            catch (NavigatorException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing InlineQuery ({@InlineQuery}).", e.InlineQuery);
                throw new NavigatorException("Unhandled error processing InlineQuery", exception);
            }
        }

        private async void OnInlineResultChosen(object sender, ChosenInlineResultEventArgs e)
        {
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();

                await InitializeNavigatorContext(scope, e.ChosenInlineResult);
                
                var actionClient = scope.ServiceProvider.GetRequiredService<INavigatorActionService>();
                await actionClient.Sail(e.ChosenInlineResult);
            }
            catch (NavigatorException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing ChosenInlineResult ({@ChosenInlineResult}).", e.ChosenInlineResult);
                throw new NavigatorException("Unhandled error processing ChosenInlineResult", exception);
            }
        }

        protected async void HandleOnMessage(object sender, MessageEventArgs e)
        {
            Logger.LogDebug("HandleOnMessage was triggered");
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();

                await InitializeNavigatorContext(scope, e.Message);
                
                var actionClient = scope.ServiceProvider.GetRequiredService<INavigatorActionService>();
                await actionClient.Sail(e.Message);
            }
            catch (NavigatorException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing Message ({@Message}).", e.Message);
                throw new NavigatorException("Unhandled error processing Message", exception);
            }
        }

        protected async void HandleOnMessageEdited(object sender, MessageEventArgs e)
        {
            Logger.LogDebug("HandleOnMessageEdited was triggered");
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();

                await InitializeNavigatorContext(scope, e.Message);
                
                var actionClient = scope.ServiceProvider.GetRequiredService<INavigatorActionService>();
                await actionClient.Sail(e.Message);
            }
            catch (NavigatorException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing MessageEdited ({@MessageEdited}).", e.Message);
                throw new NavigatorException("Unhandled error processing MessageEdited", exception);
            }
        }

        protected async void HandleOnUpdate(object sender, UpdateEventArgs e)
        {
            Logger.LogDebug("HandleOnUpdate was triggered");
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();

                await InitializeNavigatorContext(scope, e.Update);
                
                var actionClient = scope.ServiceProvider.GetRequiredService<INavigatorActionService>();
                await actionClient.Sail(e.Update);
            }
            catch (NavigatorException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing Update ({@Update}).", e.Update);
                throw new NavigatorException("Unhandled error processing Update", exception);
            }
        }
        
        protected virtual async Task InitializeNavigatorContext(IServiceScope scope, Message message)
        {
            var navigatorContext = scope.ServiceProvider.GetRequiredService<INavigatorContext>();
            await navigatorContext.Populate(message);
        }
        
        protected virtual async Task InitializeNavigatorContext(IServiceScope scope, CallbackQuery callbackQuery)
        {
            var navigatorContext = scope.ServiceProvider.GetRequiredService<INavigatorContext>();
            await navigatorContext.Populate(callbackQuery);
        }
        
        protected virtual async Task InitializeNavigatorContext(IServiceScope scope, InlineQuery inlineQuery)
        {
            var navigatorContext = scope.ServiceProvider.GetRequiredService<INavigatorContext>();
            await navigatorContext.Populate(inlineQuery);
        }
        
        protected virtual async Task InitializeNavigatorContext(IServiceScope scope, ChosenInlineResult chosenInlineResult)
        {
            var navigatorContext = scope.ServiceProvider.GetRequiredService<INavigatorContext>();
            await navigatorContext.Populate(chosenInlineResult);
        }
        
        protected virtual async Task InitializeNavigatorContext(IServiceScope scope, Update update)
        {
            var navigatorContext = scope.ServiceProvider.GetRequiredService<INavigatorContext>();
            await navigatorContext.Populate(update);
        }

        public void Dispose()
        {
            Logger.LogDebug("Disposing TelegramService");
        }
    }
}