﻿using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Navigator.Core.Abstractions
{
    public interface INavigatorClient : ITelegramBotClient
    {
        Task Start(CancellationToken cancellationToken = default);
    }
}