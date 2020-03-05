using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;

namespace Navigator.Hosted
{
    internal class NavigatorBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NavigatorBackgroundService(ILogger<BackgroundService> logger, IServiceScopeFactory serviceScopeFactory) : base(logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var botService = scope.ServiceProvider.GetRequiredService<INavigatorService>();
                
            await botService.Start(stoppingToken);
        }
    }
}