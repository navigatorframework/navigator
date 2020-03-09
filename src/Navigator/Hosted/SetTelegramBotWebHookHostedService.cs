using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Navigator.Hosted
{
    public class SetTelegramBotWebHookHostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}