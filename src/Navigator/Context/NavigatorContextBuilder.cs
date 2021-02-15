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
        private readonly NavigatorContextBuilderOptions _options;

        public NavigatorContextBuilder(ILogger<NavigatorContextBuilder> logger, IEnumerable<INavigatorClient> navigatorClients)
        {
            _logger = logger;
            _navigatorClients = navigatorClients;
            _options = new NavigatorContextBuilderOptions();
        }

        public INavigatorContextBuilder ForProvider(IProvider provider)
        {
            _options.Provider = provider;

            return this;
        }

        public INavigatorContextBuilder From(IUser user)
        {
            _options.From = user;

            return this;
        }

        public async Task<INavigatorContext> Build()
        {
            var client = _navigatorClients.GetClientFor(_options.Provider);

            if (client is null)
            {
                _logger.LogError("No client found for provider: {@Provider}", _options.Provider);
                //TODO: make NavigatorException
                throw new Exception($"No client found for provider: {_options.Provider}");
            }
            
            var context = new NavigatorContext(client, await client.GetProfile());

            return context;
        }

        private class NavigatorContextBuilderOptions
        {
            public IProvider? Provider { get; set; }
            public IUser? From { get; set; }
        }
    }
}