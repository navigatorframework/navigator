using System.Threading.Channels;
using Telegram.Bot.Types;

namespace Navigator.Strategies.Queued.Queues;

/// <summary>
///     Represents a single per-key update queue.
/// </summary>
public sealed class UpdateQueue
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UpdateQueue" /> class.
    /// </summary>
    /// <param name="key">The partition key for this queue.</param>
    /// <param name="channel">The channel that holds pending updates.</param>
    public UpdateQueue(string key, Channel<Update> channel)
    {
        Key = key;
        Channel = channel;
    }

    /// <summary>
    ///     The partition key for this queue (e.g. "chat:123").
    /// </summary>
    public string Key { get; }

    /// <summary>
    ///     The channel that holds pending updates for this partition.
    /// </summary>
    public Channel<Update> Channel { get; }
}
