namespace Navigator.Extensions.AI.Services;

/// <summary>
///     Downloads media referenced by chat context items.
/// </summary>
public interface IChatContextMediaDownloader
{
    /// <summary>
    ///     Downloads the binary content for a Telegram file.
    /// </summary>
    /// <param name="fileId">The Telegram file identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The downloaded file bytes.</returns>
    Task<byte[]> DownloadAsync(string fileId, CancellationToken cancellationToken = default);
}
