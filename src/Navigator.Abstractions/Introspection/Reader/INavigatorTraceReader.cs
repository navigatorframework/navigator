using Navigator.Abstractions.Introspection.Sink;

namespace Navigator.Abstractions.Introspection.Reader;

/// <summary>
///     Reads persisted Navigator traces.
/// </summary>
public interface INavigatorTraceReader
{
    /// <summary>
    ///     Retrieves a trace entry by identifier.
    /// </summary>
    /// <param name="identifier">The trace identifier.</param>
    /// <returns>The matching trace entry, or <see langword="null" /> when none exists.</returns>
    public Task<NavigatorTraceEntry?> Retrieve(string identifier);

    /// <summary>
    ///     Retrieves all stored trace entries.
    /// </summary>
    /// <returns>All stored trace entries.</returns>
    public Task<IReadOnlyCollection<NavigatorTraceEntry>> RetrieveAll();

    /// <summary>
    ///     Retrieves trace entries associated with a chat and message.
    /// </summary>
    /// <param name="chatId">The chat identifier.</param>
    /// <param name="messageId">The message identifier.</param>
    /// <param name="findRoot">Whether to return the root trace for a matching child trace.</param>
    /// <returns>The matching trace entries.</returns>
    public Task<IReadOnlyCollection<NavigatorTraceEntry>> RetrieveByChatAndMessage(long chatId, int messageId, bool findRoot = false);
}
