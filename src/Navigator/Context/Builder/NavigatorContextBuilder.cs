using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Navigator.Bundled.Extensions.ActionType;
using Navigator.Bundled.Extensions.Update;
using Navigator.Client;
using Navigator.Context.Builder.Options;
using Navigator.Entities;
using Navigator.Extensions;
using Navigator.Telegram;

namespace Navigator.Context.Builder;

internal class NavigatorContextBuilder : INavigatorContextBuilder
{
    private readonly ILogger<NavigatorContextBuilder> _logger;
    private readonly IEnumerable<INavigatorContextExtension> _navigatorContextExtensions;
    private readonly INavigatorContextBuilderOptions _options;
    private readonly INavigatorClient _navigatorClient;

    public NavigatorContextBuilder(ILogger<NavigatorContextBuilder> logger, IEnumerable<INavigatorContextExtension> navigatorContextExtensions, INavigatorClient navigatorClient)
    {
        _logger = logger;
        _navigatorContextExtensions = navigatorContextExtensions;
        _navigatorClient = navigatorClient;

        _options = new NavigatorContextBuilderOptions();
    }

    public async Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> optionsAction)
    {
        optionsAction.Invoke(_options);
        var actionType = _options.GetAcitonType() ?? throw new InvalidOperationException();

        var conversation = _options.GetUpdateOrDefault()?.GetConversation() ?? throw new NavigationException(nameof(Conversation));
            
        INavigatorContext context = new NavigatorContext(_navigatorClient, await _navigatorClient.GetProfile(), actionType, conversation);

        foreach (var contextExtension in _navigatorContextExtensions)
        {
            context = await contextExtension.Extend(context, _options);
        }

        return context;
    }
}