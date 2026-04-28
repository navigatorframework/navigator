using Navigator.Abstractions.Actions.Builder;

namespace Navigator.Abstractions.Catalog;

/// <summary>
///     Factory for creating and managing bot action catalogs.
/// </summary>
public interface IBotActionCatalogFactory
{
    /// <summary>
    ///     Adds a new <see cref="BotActionBuilder" /> to the catalog.
    /// </summary>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />.
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>A configured <see cref="IBotActionBuilder" /> for further customization.</returns>
    IBotActionBuilder OnUpdate(Delegate condition, Delegate? handler = default);

    /// <summary>
    ///     Retrieves the built <see cref="IBotActionCatalog" />.
    /// </summary>
    /// <returns>The built <see cref="IBotActionCatalog" />.</returns>
    IBotActionCatalog Retrieve();
}
