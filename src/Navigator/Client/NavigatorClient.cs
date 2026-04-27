using Microsoft.Extensions.Options;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Entities;
using Navigator.Abstractions.Introspection;
using Navigator.Configuration.Options;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

namespace Navigator.Client;

/// <summary>
/// Implementation of <see cref="INavigatorClient"/> for Telegram.
/// </summary>
public class NavigatorClient : TelegramBotClient, INavigatorClient
{
    private readonly INavigatorTracerFactory<NavigatorClient> _tracerFactory;
    
    /// <summary>
    /// Builds a <see cref="NavigatorClient"/>.
    /// </summary>
    /// <param name="options"><see cref="NavigatorOptions"/></param>
    /// <exception cref="ArgumentNullException">If telegram token is null</exception>
    public NavigatorClient(IOptions<NavigatorOptions> options, INavigatorTracerFactory<NavigatorClient> tracerFactory) 
        : base(options.Value.GetTelegramToken() ?? throw new ArgumentNullException())
    {
        _tracerFactory = tracerFactory;
    }

    /// <inheritdoc />
    public async Task<Bot> GetProfile(CancellationToken cancellationToken = default)
    {
        var bot = await this.GetMe(cancellationToken);

        return new Bot(bot.Id, bot.FirstName)
        {
            Username = bot.Username!,
            LastName = bot.LastName,
            CanJoinGroups = bot.CanJoinGroups,
            CanReadAllGroupMessages = bot.CanReadAllGroupMessages,
            SupportsInlineQueries = bot.SupportsInlineQueries
        };
    }

    #region Overrides of TelegramBotClient

    /// <inheritdoc />
    public override async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!_tracerFactory.HasActiveTrace())
        {
            return await base.SendRequest(request, cancellationToken);
        }

        await using var tracer = _tracerFactory.Get();
            
        tracer.AddTag(NavigatorTraceKeys.TelegramApiCall, $"{request.HttpMethod}: {request.MethodName}");

        var response = await base.SendRequest(request, cancellationToken);

        if (response is Message message)
        {
            tracer.AddTag(NavigatorTraceKeys.UpdateChatId, $"{message.Chat.Id}");
            tracer.AddTag(NavigatorTraceKeys.UpdateMessageId, $"{message.Id}");
        }
        
        return response;
    }

    #endregion
}