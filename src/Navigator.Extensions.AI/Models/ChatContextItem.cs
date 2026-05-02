namespace Navigator.Extensions.AI.Models;

public class ChatContextItem
{
    public string Type { get; init; } = ChatContextItemTypes.Text;
    public string? Text { get; init; }
    public byte[]? Data { get; init; }
    public string? MimeType { get; init; }
    public Dictionary<string, string?> Metadata { get; init; } = [];
}

public static class ChatContextItemTypes
{
    public const string Text = "text";
    public const string Image = "image";
    public const string Audio = "audio";
}
