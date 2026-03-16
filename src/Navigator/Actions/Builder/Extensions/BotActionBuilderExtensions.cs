using Navigator.Abstractions;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Builder;
using Navigator.Abstractions.Priorities;
using Telegram.Bot.Types.Enums;

namespace Navigator.Actions.Builder.Extensions;

/// <summary>
///     Extension methods for <see cref="IBotActionBuilder" />.
/// </summary>
public static class BotActionBuilderExtensions
{
    private const string NameKey = "builder.name";
    private const string ConditionKey = "builder.condition";
    private const string ConditionInputTypesKey = "builder.condition_input_types";
    private const string HandlerKey = "builder.handler";
    private const string HandlerInputTypesKey = "builder.handler_input_types";
    private const string CategoryKey = "builder.category";
    private const string PriorityKey = "builder.priority";
    private const string ChatActionKey = "builder.chat_action";
    private const string ExclusivityLevelKey = "builder.exclusivity_level";

    /// <summary>
    ///     Gets the name from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The name or null if not set.</returns>
    internal static string? GetName(this IBotActionBuilder builder)
    {
        return builder.Get<string?>(NameKey);
    }

    /// <summary>
    ///     Gets the condition from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The condition or null if not set.</returns>
    internal static Delegate? GetCondition(this IBotActionBuilder builder)
    {
        return builder.Get<Delegate?>(ConditionKey);
    }

    /// <summary>
    ///     Gets the condition input types from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The condition input types or null if not set.</returns>
    internal static Type[]? GetConditionInputTypes(this IBotActionBuilder builder)
    {
        return builder.Get<Type[]?>(ConditionInputTypesKey);
    }

    /// <summary>
    ///     Gets the handler from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The handler or null if not set.</returns>
    internal static Delegate? GetHandler(this IBotActionBuilder builder)
    {
        return builder.Get<Delegate?>(HandlerKey);
    }

    /// <summary>
    ///     Gets the handler input types from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The handler input types or null if not set.</returns>
    internal static Type[]? GetHandlerInputTypes(this IBotActionBuilder builder)
    {
        return builder.Get<Type[]?>(HandlerInputTypesKey);
    }

    /// <summary>
    ///     Gets the category from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The category or null if not set.</returns>
    internal static UpdateCategory? GetCategory(this IBotActionBuilder builder)
    {
        return builder.Get<UpdateCategory?>(CategoryKey);
    }

    /// <summary>
    ///     Gets the priority from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The priority or the default priority if not set.</returns>
    internal static EPriority GetPriority(this IBotActionBuilder builder)
    {
        return builder.Get<EPriority?>(PriorityKey) ?? EPriority.Normal;
    }

    /// <summary>
    ///     Gets the chat action from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The chat action or null if not set.</returns>
    internal static ChatAction? GetChatAction(this IBotActionBuilder builder)
    {
        return builder.Get<ChatAction?>(ChatActionKey);
    }

    /// <summary>
    ///     Gets the exclusivity level from the builder dictionary.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>The exclusivity level or <see cref="EExclusivityLevel.None" /> if not set.</returns>
    internal static EExclusivityLevel GetExclusivityLevel(this IBotActionBuilder builder)
    {
        return builder.Get<EExclusivityLevel?>(ExclusivityLevelKey) ?? EExclusivityLevel.None;
    }
}
