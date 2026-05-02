namespace Navigator.Extensions.AI.Services;

public interface IChatContextMediaDownloader
{
    Task<byte[]> DownloadAsync(string fileId, CancellationToken cancellationToken = default);
}
