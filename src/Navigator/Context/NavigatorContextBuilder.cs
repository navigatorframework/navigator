using Microsoft.Extensions.Logging;
using Navigator.Context.Extensions;

namespace Navigator.Context;

internal class NavigatorContextBuilder : INavigatorContextBuilder
{
    private readonly ILogger<NavigatorContextBuilder> _logger;
    private readonly IEnumerable<INavigatorContextExtension> _navigatorContextExtensions;
    private readonly INavigatorContextBuilderOptions _options;
    private readonly INavigatorContextBuilderConversationSource _conversationSource;
    private readonly INavigatorClient _navigatorClient;

    public NavigatorContextBuilder(ILogger<NavigatorContextBuilder> logger, IEnumerable<INavigatorContextExtension> navigatorContextExtensions, INavigatorContextBuilderConversationSource conversationSource, INavigatorClient navigatorClient)
    {
        _logger = logger;
        _navigatorContextExtensions = navigatorContextExtensions;
        _conversationSource = conversationSource;
        _navigatorClient = navigatorClient;

        _options = new NavigatorContextBuilderOptions();
    }

    public async Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> optionsAction)
    {
        optionsAction.Invoke(_options);
        var actionType = _options.GetAcitonType() ?? throw new InvalidOperationException();

        var conversation = await _conversationSource.GetConversationAsync(_options.GetOriginalEventOrDefault());
            
        INavigatorContext context = new NavigatorContext(_navigatorClient, await _navigatorClient.GetProfile(), actionType, conversation);

        foreach (var contextExtension in _navigatorContextExtensions)
        {
            context = await contextExtension.Extend(context, _options);
        }

        return context;
    }
}