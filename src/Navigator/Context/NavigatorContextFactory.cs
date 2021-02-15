using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Navigator.Context
{
    public class NavigatorContextFactory : INavigatorContextFactory
    {
        private readonly ILogger<NavigatorContextFactory> _logger;
        private readonly INavigatorContextBuilder _navigatorContextBuilder;
        private INavigatorContext NavigatorContext { get; set; }

        public NavigatorContextFactory(ILogger<NavigatorContextFactory> logger, INavigatorContextBuilder navigatorContextBuilder)
        {
            _logger = logger;
            _navigatorContextBuilder = navigatorContextBuilder;
        }

        public async Task Supply(Action<INavigatorContextBuilder> action)
        {
            
            action.Invoke(_navigatorContextBuilder);

            try
            {
                _logger.LogTrace("Building a new NavigatorContext");
                
                NavigatorContext = await _navigatorContextBuilder.Build();
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
}