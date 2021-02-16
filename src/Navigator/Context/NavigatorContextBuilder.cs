using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Entities;

namespace Navigator.Context
{
    public class NavigatorContextBuilder : INavigatorContextBuilder
    {
        private readonly ILogger<NavigatorContextBuilder> _logger;
        private readonly IEnumerable<INavigatorClient> _navigatorClients;
        private readonly INavigatorContextBuilderOptions _options;

        
        public NavigatorContextBuilder(ILogger<NavigatorContextBuilder> logger, IEnumerable<INavigatorClient> navigatorClients)
        {
            _logger = logger;
            _navigatorClients = navigatorClients;
            _options = new NavigatorContextBuilderOptions();
        }

      

        public async Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> optionsAction)
        {
            optionsAction.Invoke(_options);
            
            var client = _navigatorClients.GetClientFor(_options.GetProvider());

            if (client is null)
            {
                _logger.LogError("No client found for provider: {@Provider}", _options.GetProvider());
                //TODO: make NavigatorException
                throw new Exception($"No client found for provider: {_options.GetProvider()?.Name}");
            }
            
            var context = new NavigatorContext(client, await client.GetProfile());

            return context;
        }
    }
}