namespace Navigator.Extensions.AI.Models;

/// <summary>
///     Represents a message stored inside a chat context.
/// </summary>
public class ChatContextMessage
{
    /// <summary>
    ///     Gets the author role for the message.
    /// </summary>
    public string Role { get; init; } = ChatContextRoles.User;

    /// <summary>
    ///     Gets the optional author display name.
    /// </summary>
    public string? AuthorName { get; init; }

    /// <summary>
    ///     Gets the message creation time.
    /// </summary>
    public DateTime? CreatedAt { get; init; }

    /// <summary>
    ///     Gets provider-specific metadata for the message.
    /// </summary>
    public Dictionary<string, string?> Metadata { get; init; } = [];

    /// <summary>
    ///     Gets the content items that make up the message.
    /// </summary>
    public List<ChatContextItem> Items { get; init; } = [];
}

/// <summary>
///     Defines supported chat context role names.
/// </summary>
public static class ChatContextRoles
{
    /// <summary>
    ///     End-user authored content.
    /// </summary>
    public const string User = "user";

    /// <summary>
    ///     Assistant-authored content.
    /// </summary>
    public const string Assistant = "assistant";

    /// <summary>
    ///     System-authored content.
    /// </summary>
    public const string System = "system";

    /// <summary>
    ///     Developer-authored content.
    /// </summary>
    public const string Developer = "developer";

    /// <summary>
    ///     Tool-authored content.
    /// </summary>
    public const string Tool = "tool";
}
