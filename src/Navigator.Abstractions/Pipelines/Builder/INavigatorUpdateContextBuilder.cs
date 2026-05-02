using Navigator.Abstractions.Pipelines.Context;
using Telegram.Bot.Types;

namespace Navigator.Abstractions.Pipelines.Builder;

/// <summary>
///     Builds <see cref="NavigatorUpdateContext" /> instances from Telegram updates.
/// </summary>
public interface INavigatorUpdateContextBuilder
{
    /// <summary>
    ///     Builds a Navigator update context for the provided update.
    /// </summary>
    /// <param name="update">The Telegram update to wrap.</param>
    /// <returns>The built update context.</returns>
    ValueTask<NavigatorUpdateContext> Build(Update update);
}
