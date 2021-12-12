using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Context.Extensions;
using Navigator.Entities;

namespace Navigator.Context;

internal class NavigatorContextBuilder : INavigatorContextBuilder
{
    private readonly ILogger<NavigatorContextBuilder> _logger;
    private readonly IEnumerable<INavigatorProvider> _navigatorProviders;
    private readonly IEnumerable<INavigatorContextExtension> _navigatorContextExtensions;
    private readonly INavigatorContextBuilderOptions _options;
    private readonly INavigatorContextBuilderConversationSource _conversationSource;

    public NavigatorContextBuilder(ILogger<NavigatorContextBuilder> logger, IEnumerable<INavigatorProvider> navigatorClients, IEnumerable<INavigatorContextExtension> navigatorContextExtensions, INavigatorContextBuilderConversationSource conversationSource)
    {
        _logger = logger;
        _navigatorProviders = navigatorClients;
        _navigatorContextExtensions = navigatorContextExtensions;
        _conversationSource = conversationSource;

        _options = new NavigatorContextBuilderOptions();
    }

    public async Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> optionsAction)
    {
        optionsAction.Invoke(_options);

        var provider = _navigatorProviders.FirstOrDefault(p => p.GetType() == _options.GetProvider());

        if (provider is null)
        {
            _logger.LogError("No provider found for: {@Provider}", _options.GetProvider());
            //TODO: make NavigatorException
            throw new Exception($"No provider found for: {_options.GetProvider()?.Name}");
        }

        var actionType = _options.GetAcitonType() ?? throw new InvalidOperationException();

        var conversation = await _conversationSource.GetConversationAsync(_options.GetOriginalEventOrDefault());
            
        INavigatorContext context = new NavigatorContext(provider, await provider.GetClient().GetProfile(), actionType, conversation);

        foreach (var contextExtension in _navigatorContextExtensions)
        {
            context = await contextExtension.Extend(context, _options);
        }

        return context;
    }
}