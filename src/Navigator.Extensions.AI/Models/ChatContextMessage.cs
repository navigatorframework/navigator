namespace Navigator.Extensions.AI.Models;

public class ChatContextMessage
{
    public string Role { get; init; } = ChatContextRoles.User;
    public string? AuthorName { get; init; }
    public DateTime? CreatedAt { get; init; }
    public Dictionary<string, string?> Metadata { get; init; } = [];
    public List<ChatContextItem> Items { get; init; } = [];
}

public static class ChatContextRoles
{
    public const string User = "user";
    public const string Assistant = "assistant";
    public const string System = "system";
    public const string Developer = "developer";
    public const string Tool = "tool";
}
