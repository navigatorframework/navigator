using Microsoft.Extensions.Logging;
using Navigator.Context.Builder;
using Navigator.Context.Builder.Options;

namespace Navigator.Context;

internal class NavigatorContextFactory : INavigatorContextFactory
{
    private readonly ILogger<NavigatorContextFactory> _logger;
    private readonly INavigatorContextBuilder _navigatorContextBuilder;
    private INavigatorContext? NavigatorContext { get; set; }

    public NavigatorContextFactory(ILogger<NavigatorContextFactory> logger, INavigatorContextBuilder navigatorContextBuilder)
    {
        _logger = logger;
        _navigatorContextBuilder = navigatorContextBuilder;
    }

    public async Task Supply(Action<INavigatorContextBuilderOptions> action)
    {
        try
        {
            _logger.LogTrace("Building a new NavigatorContext");
                
            NavigatorContext = await _navigatorContextBuilder.Build(action);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception building NavigatorContext");
            throw;
        }
    }

    public INavigatorContext Retrieve()
    {
        return NavigatorContext ?? throw new NullReferenceException();
    }
}