using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Context.Extensions;
using Navigator.Entities;

namespace Navigator.Context
{
    public class NavigatorContextBuilder : INavigatorContextBuilder
    {
        private readonly ILogger<NavigatorContextBuilder> _logger;
        private readonly IEnumerable<INavigatorProvider> _navigatorProviders;
        private readonly IEnumerable<INavigatorContextExtension> _navigatorContextExtensions;
        private readonly INavigatorContextBuilderOptions _options;

        public NavigatorContextBuilder(ILogger<NavigatorContextBuilder> logger, IEnumerable<INavigatorProvider> navigatorClients, IEnumerable<INavigatorContextExtension> navigatorContextExtensions)
        {
            _logger = logger;
            _navigatorProviders = navigatorClients;
            _navigatorContextExtensions = navigatorContextExtensions;
            
            _options = new NavigatorContextBuilderOptions();
        }

        public async Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> optionsAction)
        {
            optionsAction.Invoke(_options);

            var client = _navigatorProviders.FirstOrDefault(provider => provider.GetType() == _options.GetProvider())?.GetClient();

            if (client is null)
            {
                _logger.LogError("No client found for provider: {@Provider}", _options.GetProvider());
                //TODO: make NavigatorException
                throw new Exception($"No client found for provider: {_options.GetProvider()?.Name}");
            }

            var acitonType = _options.GetAcitonType() ?? throw new InvalidOperationException();
            
            INavigatorContext context = new NavigatorContext(client, await client.GetProfile(), acitonType);

            foreach (var contextExtension in _navigatorContextExtensions)
            {
                context = await contextExtension.Extend(context, _options);
            }

            return context;
        }
    }
}