using Navigator.Abstractions.Actions.Builder;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Abstractions.Catalog;
using Navigator.Abstractions.Catalog.Extensions;
using Navigator.Extensions.Management.Actions;

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
            .WithName("Debug Command");
    }
}
