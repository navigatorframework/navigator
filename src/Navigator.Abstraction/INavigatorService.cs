using System.Threading;
using System.Threading.Tasks;

namespace Navigator.Abstraction
{
    public interface INavigatorService
    {
        Task Start(CancellationToken stoppingToken = default);
    }
}