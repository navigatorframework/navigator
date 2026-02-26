using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Configuration.Options;
using Navigator.Extensions.Store.Steps;

namespace Navigator.Extensions.Store;

/// <summary>
///     Navigator extension that adds persistence for Telegram users, chats, and conversations.
/// </summary>
public class StoreExtension : INavigatorExtension<StoreOptions>
{
    /// <inheritdoc />
    public void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, StoreOptions extensionOptions)
    {
        extensionOptions.RunDbContextConfiguration(services);
    }
}
