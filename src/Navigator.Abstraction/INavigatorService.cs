﻿using System.Threading;
using System.Threading.Tasks;

namespace Navigator.Core.Abstractions
{
    public interface INavigatorService
    {
        Task Start(CancellationToken stoppingToken = default);
    }
}