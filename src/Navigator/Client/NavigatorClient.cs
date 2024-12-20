using Microsoft.Extensions.Options;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Entities;
using Navigator.Configuration.Options;
using Telegram.Bot;

namespace Navigator.Client;

/// <summary>
/// Implementation of <see cref="INavigatorClient"/> for Telegram.
/// </summary>
public class NavigatorClient : TelegramBotClient, INavigatorClient
{
    /// <summary>
    /// Builds a <see cref="NavigatorClient"/>.
    /// </summary>
    /// <param name="options"><see cref="NavigatorOptions"/></param>
    /// <exception cref="ArgumentNullException">If telegram token is null</exception>
    public NavigatorClient(IOptions<NavigatorOptions> options) : base(options.Value.GetTelegramToken() ?? throw new ArgumentNullException())
    {
    }

    /// <inheritdoc />
    public async Task<Bot> GetProfile(CancellationToken cancellationToken = default)
    {
        var bot = await this.GetMeAsync(cancellationToken);

        return new Bot(bot.Id, bot.FirstName)
        {
            Username = bot.Username!,
            LastName = bot.LastName,
            CanJoinGroups = bot.CanJoinGroups,
            CanReadAllGroupMessages = bot.CanReadAllGroupMessages,
            SupportsInlineQueries = bot.SupportsInlineQueries
        };
    }
}