using Navigator.Abstractions.Client;
using Telegram.Bot;

namespace Navigator.Extensions.AI.Services;

public class ChatContextMediaDownloader : IChatContextMediaDownloader
{
    private readonly INavigatorClient _client;

    public ChatContextMediaDownloader(INavigatorClient client)
    {
        _client = client;
    }

    public async Task<byte[]> DownloadAsync(string fileId, CancellationToken cancellationToken = default)
    {
        using var stream = new MemoryStream();
        await _client.GetInfoAndDownloadFile(fileId, stream, cancellationToken);
        return stream.ToArray();
    }
}
