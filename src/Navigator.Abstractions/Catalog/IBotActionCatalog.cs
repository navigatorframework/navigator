using Navigator.Abstractions.Actions;
using Telegram.Bot.Types;

namespace Navigator.Abstractions.Catalog;

/// <summary>
///     Represents a catalog of <see cref="BotAction" /> instances. The catalog is used to efficiently retrieve <see cref="BotAction" />
///     instances based on their <see cref="BotActionInformation.Category" /> and <see cref="BotAction.Information" />. The catalog also
///     provides a way to prioritize <see cref="BotAction" /> instances, which is useful when multiple <see cref="BotAction" /> instances
///     can potentially handle the same <see cref="Update" />.
/// </summary>
public interface IBotActionCatalog
{
    /// <summary>
    ///     Retrieves all <see cref="BotAction" /> instances with the specified <see cref="UpdateCategory" />,
    ///     ordered by their priority.
    /// </summary>
    /// <param name="category">The <see cref="UpdateCategory" /> of the <see cref="BotAction" /> instances to retrieve.</param>
    /// <returns>A list of <see cref="BotAction" /> instances.</returns>
    public IEnumerable<BotAction> Retrieve(UpdateCategory category);
}