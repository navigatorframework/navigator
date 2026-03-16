namespace Navigator.Abstractions.Introspection;

/// <summary>
///     Common tag keys emitted by navigator traces.
/// </summary>
public static class NavigatorTraceKeys
{
    /// <summary>
    ///     The Telegram update identifier.
    /// </summary>
    public const string UpdateId = "navigator.update.id";

    /// <summary>
    ///     The Telegram chat identifier extracted from the update.
    /// </summary>
    public const string UpdateChatId = "navigator.update.chat.id";

    /// <summary>
    ///     The Telegram message identifier extracted from the update.
    /// </summary>
    public const string UpdateMessageId = "navigator.update.message.id";

    /// <summary>
    ///     The Telegram user identifier extracted from the update.
    /// </summary>
    public const string UpdateUserId = "navigator.update.user.id";

    /// <summary>
    ///     The Telegram update type.
    /// </summary>
    public const string UpdateType = "navigator.update.type";

    /// <summary>
    ///     The resolved update category.
    /// </summary>
    public const string UpdateCategory = "navigator.update.category";

    /// <summary>
    ///     The action name involved in the trace.
    /// </summary>
    public const string ActionName = "navigator.action.name";

    /// <summary>
    ///     An action that matched during resolution.
    /// </summary>
    public const string ActionMatched = "navigator.action.matched";

    /// <summary>
    ///     An action kept by a pipeline step.
    /// </summary>
    public const string ActionKept = "navigator.action.kept";

    /// <summary>
    ///     An action discarded by a pipeline step.
    /// </summary>
    public const string ActionDiscarded = "navigator.action.discarded";

    /// <summary>
    ///     The chat action sent before execution.
    /// </summary>
    public const string ExecutionChatAction = "navigator.execution.chat_action";
}
