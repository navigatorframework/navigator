using Navigator.Abstractions.Client;
using Telegram.Bot;

namespace Navigator.Extensions.AI.Services;

/// <summary>
///     Downloads Telegram media bytes for chat context storage.
/// </summary>
public class ChatContextMediaDownloader : IChatContextMediaDownloader
{
    private readonly INavigatorClient _client;

    /// <summary>
    ///     Initializes a new downloader instance.
    /// </summary>
    /// <param name="client">The Navigator Telegram client.</param>
    public ChatContextMediaDownloader(INavigatorClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<byte[]> DownloadAsync(string fileId, CancellationToken cancellationToken = default)
    {
        using var stream = new MemoryStream();
        await _client.GetInfoAndDownloadFile(fileId, stream, cancellationToken);
        return stream.ToArray();
    }
}
