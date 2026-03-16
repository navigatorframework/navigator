using Navigator.Abstractions.Actions.Builder;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Extensions.Management.Actions;

namespace Navigator.Extensions.Management;

/// <summary>
///     Extension methods for registering management commands.
/// </summary>
public static class BotActionExtensions
{
    /// <summary>
    ///     Registers all management commands.
    /// </summary>
    /// <param name="factory">The bot action catalog factory.</param>
    /// <returns>The factory for chaining.</returns>
    public static BotActionCatalogFactory RegisterManagementCommands(this BotActionCatalogFactory factory)
    {
        factory.RegisterDebugCommand();
        return factory;
    }
    
    /// <summary>
    ///     Registers the debug management command.
    /// </summary>
    /// <param name="factory">The bot action catalog factory.</param>
    /// <returns>The configured action builder for further customization.</returns>
    private static IBotActionBuilder RegisterDebugCommand(this BotActionCatalogFactory factory)
    {
        return factory.OnCommand("debug")
            .SetHandler(DebugCommandAction.HandleDebugCommand)
            .WithName("Debug Command");
    }
}
