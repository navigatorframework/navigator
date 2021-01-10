using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions;
using Telegram.Bot.Types;

namespace Navigator
{
    public class NavigatorContextBuilder : INavigatorContextBuilder
    {
        protected readonly ILogger<NavigatorContextBuilder> Logger;
        protected readonly IEnumerable<INavigatorContextExtensionProvider> ExtensionProviders;
        protected readonly INavigatorContext Ctx;
        public NavigatorContextBuilder(ILogger<NavigatorContextBuilder> logger, IEnumerable<INavigatorContextExtensionProvider> extensionProviders, INavigatorContext ctx)
        {
            Logger = logger;
            ExtensionProviders = extensionProviders.OrderBy(ep => ep.Order);
            Ctx = ctx;
        }

        public async Task Build(Update update)
        {
            var extensions = new Dictionary<string, object>();

            foreach (var extensionProvider in ExtensionProviders)
            {
                var (type, extension) = await extensionProvider.Process(update);

                if (type != null && extension != null)
                {
                    extensions.Add(type, extension);
                }
            }

            await Ctx.Init(update, extensions);
        }
    }
}