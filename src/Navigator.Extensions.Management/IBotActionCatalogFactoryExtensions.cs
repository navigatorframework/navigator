using Navigator.Abstractions.Actions.Builder;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Abstractions.Catalog;
using Navigator.Abstractions.Catalog.Extensions;
using Navigator.Extensions.Management.Actions;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Management;

/// <summary>
///     Extension methods for registering management commands.
/// </summary>
public static class IBotActionCatalogFactoryExtensions
{
    /// <param name="factory">The bot action catalog factory.</param>
    extension(IBotActionCatalogFactory factory)
    {
        /// <summary>
        ///     Registers all management commands.
        /// </summary>
        /// <returns>The factory for chaining.</returns>
        public IBotActionCatalogFactory RegisterManagementCommands()
        {
            factory.RegisterDebugCommand();
            return factory;
        }
    }
    
    /// <summary>
    ///     Registers the debug command and full trace callback.
    /// </summary>
    /// <param name="factory"></param>
    private static void RegisterDebugCommand(this IBotActionCatalogFactory factory)
    {
        factory.OnCommand("debug")
            .SetHandler(DebugCommandActions.HandleDebugCommand)
            .WithName("Navigator.Management.Actions.Debug:Command");
        
        factory.OnCallbackQuery((Update update) => update.CallbackQuery?.Data?.StartsWith("debug_full_trace_") is true)
            .SetHandler(DebugCommandActions.HandleFullTraceCallback)
            .WithName("Navigator.Management.Actions.Debug:FullTraceCallback");
    }
}
