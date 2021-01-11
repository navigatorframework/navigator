using System.Threading;
using System.Threading.Tasks;
using Navigator.Extensions.Shipyard.Abstractions.Model;

namespace Navigator.Extensions.Shipyard.Abstractions
{
    public interface IBotManagementService
    {
        Task<BotInfo> GetBotInfo(CancellationToken cancellationToken = default);
        Task<BotPic> GetBotPic(CancellationToken cancellationToken = default);
        Task<BotCommands> GetBotCommands(CancellationToken cancellationToken = default);
        Task UpdateBotCommands(BotCommands botCommands, CancellationToken cancellationToken = default);
        
    }
}