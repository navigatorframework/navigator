using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions;
using Navigator.Extensions.Shipyard.Abstractions;
using Navigator.Extensions.Shipyard.Abstractions.Model;

namespace Navigator.Extensions.Shipyard
{
    /// <inheritdoc />
    public class BotManagementService : IBotManagementService
    {
        private readonly ILogger<BotManagementService> _logger;
        private readonly IBotClient _botClient;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="botClient"></param>
        public BotManagementService(ILogger<BotManagementService> logger, IBotClient botClient)
        {
            _logger = logger;
            _botClient = botClient;
        }

        /// <inheritdoc />
        public async Task<BotInfo?> GetBotInfo(CancellationToken cancellationToken = default)
        {
            try
            {
                var info = await _botClient.GetMeAsync(cancellationToken);

                return new BotInfo
                {
                    Id = info.Id,
                    Username = info.Username,
                    Name = info.FirstName,
                    Permissions = new BotInfo.BotPermissions
                    {
                        CanJoinGroups = info.CanJoinGroups ?? false,
                        CanReadAllGroupMessages = info.CanReadAllGroupMessages ?? false,
                        SupportsInlineQueries = info.SupportsInlineQueries ?? false
                    }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception requesting bot information.");

                return default;
            }
        }

        /// <inheritdoc />
        public async Task<BotPic?> GetBotPic(CancellationToken cancellationToken = default)
        {
            try
            {
                var photos = await _botClient.GetUserProfilePhotosAsync(_botClient.BotId, cancellationToken: cancellationToken);

                if (photos.TotalCount > 0)
                {
                    var botPicId = photos.Photos.FirstOrDefault()?
                        .OrderByDescending(x => x.FileSize).FirstOrDefault()
                        ?.FileId;

                    if (!string.IsNullOrWhiteSpace(botPicId))
                    {
                        var pictureStream = new MemoryStream();
                        await _botClient.GetInfoAndDownloadFileAsync(botPicId, pictureStream, cancellationToken);

                        return new BotPic
                        {
                            File = pictureStream
                        };
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception requesting bot pic.");
            }

            return default;
        }

        /// <inheritdoc />
        public async Task<BotCommands> GetBotCommands(CancellationToken cancellationToken = default)
        {
            var commands = await _botClient.GetMyCommandsAsync(cancellationToken);

            return new BotCommands
            {
                Commands = commands.ToDictionary(command => command.Command, command => command.Description)
            };
        }

        /// <inheritdoc />
        public Task UpdateBotCommands(BotCommands botCommands, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}