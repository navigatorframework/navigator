using Telegram.Bot.Types;

namespace Navigator.Strategies.Queued.Queues;

/// <summary>
///     A Telegram update enqueued for background processing, together with the trace
///     identifier it was received with (e.g. the webhook request's <c>TraceIdentifier</c>).
///     Carrying the identifier through the queue keeps introspection traces (<c>/debug</c>)
///     rooted at the same identifier as when the update was processed inline.
/// </summary>
/// <param name="Update">The Telegram update to process.</param>
/// <param name="Identifier">The trace identifier the update was received with.</param>
public sealed record QueuedUpdate(Update Update, string Identifier);
