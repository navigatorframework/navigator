﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MihaZupan.TelegramBotClients;
using Navigator.Core.Abstractions;
using Telegram.Bot.Types.Enums;
using SchedulerSettings = MihaZupan.TelegramBotClients.RateLimitedClient.SchedulerSettings;

namespace Navigator.Core
{
    internal class NavigatorClient : RateLimitedTelegramBotClient, INavigatorClient
    {
        protected readonly ILogger<NavigatorClient> Logger;
        public bool Started { get; set; }

        public NavigatorClient(NavigatorClientConfiguration configuration, ILogger<NavigatorClient> logger) : 
            base(configuration.BotToken, configuration.HttpClient, configuration.SchedulerSettings)
        {
            Started = false;
            Logger = logger;
        }

        public async Task Start(CancellationToken cancellationToken = default)
        {
            if (Started == false)
            {
                var me = await GetMeAsync(cancellationToken);

                StartReceiving(new []
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery,
                    UpdateType.InlineQuery,
                    UpdateType.ChosenInlineResult
                }, cancellationToken);

                Started = true;
                
                Logger.LogInformation("Telegram Bot Client is receiving updates for bot: {@BotName}", me.Username);

                var tcs = new TaskCompletionSource<bool>();
                cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
                
                await tcs.Task;
            }
            else
            {
                var me = await GetMeAsync(cancellationToken);
                Logger.LogWarning("Tried to start Telegram Bot Client update receiving when it's already running for bot: {@BotName}", me.Username);
            }
        }
    }

    internal class NavigatorClientConfiguration
    {
        public string BotToken { get; set; }
        public HttpClient HttpClient { get; set; }
        public SchedulerSettings SchedulerSettings { get; set; }
        
        public NavigatorClientConfiguration(string botToken = default, HttpClient httpClient = default, SchedulerSettings schedulerSettings = default)
        {
            BotToken = botToken;
            HttpClient = httpClient;
            SchedulerSettings = schedulerSettings;
        }
    }
}