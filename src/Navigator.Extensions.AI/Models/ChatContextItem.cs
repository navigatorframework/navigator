namespace Navigator.Extensions.AI.Models;

/// <summary>
///     Represents a single content item within a chat message.
/// </summary>
public class ChatContextItem
{
    /// <summary>
    ///     Gets the item type.
    /// </summary>
    public string Type { get; init; } = ChatContextItemTypes.Text;

    /// <summary>
    ///     Gets the text payload when the item is textual.
    /// </summary>
    public string? Text { get; init; }

    /// <summary>
    ///     Gets the binary payload when the item contains media.
    /// </summary>
    public byte[]? Data { get; init; }

    /// <summary>
    ///     Gets the MIME type of the media payload.
    /// </summary>
    public string? MimeType { get; init; }

    /// <summary>
    ///     Gets provider-specific metadata for the item.
    /// </summary>
    public Dictionary<string, string?> Metadata { get; init; } = [];
}

/// <summary>
///     Defines supported chat context item type names.
/// </summary>
public static class ChatContextItemTypes
{
    /// <summary>
    ///     Text content.
    /// </summary>
    public const string Text = "text";

    /// <summary>
    ///     Image content.
    /// </summary>
    public const string Image = "image";

    /// <summary>
    ///     Audio content.
    /// </summary>
    public const string Audio = "audio";
}
