using Navigator.Configuration;
using Navigator.Entities;
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
    /// <param name="options"><see cref="INavigatorOptions"/></param>
    public NavigatorClient(INavigatorOptions options) 
        : base(options.GetTelegramToken() ?? throw new ArgumentNullException())
    {
    }

    /// <inheritdoc />
    public async Task<Bot> GetProfile(CancellationToken cancellationToken = default)
    {
        var bot = await this.GetMeAsync(cancellationToken);

        return new Bot()
        {
            Id = bot.Id,
            Username = bot.Username!,
            FirstName = bot.FirstName,
            LastName = bot.LastName,
            CanJoinGroups = bot.CanJoinGroups,
            CanReadAllGroupMessages = bot.CanReadAllGroupMessages,
            SupportsInlineQueries = bot.SupportsInlineQueries
        };
    }
}