using Navigator.Abstractions.Strategies;

namespace Navigator.Strategies.Queued;

/// <summary>
///     Configuration options for the queued navigation strategy.
/// </summary>
public class QueuedStrategyOptions : INavigatorStrategyOptions
{
    /// <summary>
    ///     Maximum number of pending updates per queue.
    ///     Use 0 for an unbounded queue; any positive value applies backpressure
    ///     when the queue is full, causing the producer to wait.
    ///     Default is 0 (unbounded).
    /// </summary>
    public int MaxMessagesPerQueue { get; set; }
}
