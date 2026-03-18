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
    /// <summary>
    ///     Registers all management commands.
    /// </summary>
    /// <param name="factory">The bot action catalog factory.</param>
    /// <returns>The factory for chaining.</returns>
    public static IBotActionCatalogFactory RegisterManagementCommands(this IBotActionCatalogFactory factory)
    {
        factory.RegisterDebugCommand();
        factory.RegisterFullTraceCallback();
        return factory;
    }
    
    /// <summary>
    ///     Registers the debug management command.
    /// </summary>
    /// <param name="factory">The bot action catalog factory.</param>
    /// <returns>The configured action builder for further customization.</returns>
    private static IBotActionBuilder RegisterDebugCommand(this IBotActionCatalogFactory factory)
    {
        return factory.OnCommand("debug")
            .SetHandler(DebugCommandAction.HandleDebugCommand)
            .WithName("Navigator.Management.Actions.Debug:Command");
    }
    
    /// <summary>
    ///     Registers the full trace callback handler.
    /// </summary>
    /// <param name="factory">The bot action catalog factory.</param>
    /// <returns>The configured action builder for further customization.</returns>
    private static IBotActionBuilder RegisterFullTraceCallback(this IBotActionCatalogFactory factory)
    {
        return factory.OnCallbackQuery(
            (Update update) => update.CallbackQuery?.Data?.StartsWith("debug_full_trace_") is true)
            .SetHandler(DebugCommandAction.HandleFullTraceCallback)
            .WithName("Navigator.Management.Actions.Debug:FullTraceCallback");
    }
}
