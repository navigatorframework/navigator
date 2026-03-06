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
    ///     Sets the name of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="name">The name to be set.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder WithName(this IBotActionBuilder builder, string name)
    {
        builder.Set(NameKey, name);
        return builder;
    }

    /// <summary>
    ///     Sets the condition of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="condition">The condition to be set.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder SetCondition(this IBotActionBuilder builder, Delegate condition)
    {
        builder.Set(ConditionKey, condition);
        builder.Set(ConditionInputTypesKey, condition.Method.GetParameters().Select(info => info.ParameterType).ToArray());
        return builder;
    }

    /// <summary>
    ///     Sets the handler of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="handler">The handler to be set.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder SetHandler(this IBotActionBuilder builder, Delegate handler)
    {
        builder.Set(HandlerKey, handler);
        builder.Set(HandlerInputTypesKey, handler.Method.GetParameters().Select(info => info.ParameterType).ToArray());
        return builder;
    }

    /// <summary>
    ///     Sets the <see cref="UpdateCategory" /> of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="category">The <see cref="UpdateCategory" /> to be set.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder SetCategory(this IBotActionBuilder builder, UpdateCategory category)
    {
        builder.Set(CategoryKey, category);
        return builder;
    }

    /// <summary>
    ///     Sets the priority of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="priority">The priority to be set.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder WithPriority(this IBotActionBuilder builder, EPriority priority)
    {
        builder.Set(PriorityKey, priority);
        return builder;
    }

    /// <summary>
    ///     Sets the <see cref="ChatAction" /> of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="chatAction">The <see cref="ChatAction" /> to be set.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder WithChatAction(this IBotActionBuilder builder, ChatAction chatAction)
    {
        builder.Set(ChatActionKey, chatAction);
        return builder;
    }

    /// <summary>
    ///     Sets the <see cref="EExclusivityLevel" /> of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="level">The exclusivity level to be set.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder SetExclusivityLevel(this IBotActionBuilder builder, EExclusivityLevel level)
    {
        builder.Set(ExclusivityLevelKey, level);
        return builder;
    }

    /// <summary>
    ///     Convenience method. Sets the exclusivity level to <see cref="EExclusivityLevel.Global" />.
    ///     If this action is the highest-priority matched action overall, all other actions are discarded.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder AsExclusive(this IBotActionBuilder builder)
    {
        return builder.SetExclusivityLevel(EExclusivityLevel.Global);
    }

    /// <summary>
    ///     Convenience method. Sets the exclusivity level to <see cref="EExclusivityLevel.None" />,
    ///     overriding any default exclusivity set by registration helpers such as <c>OnCommand</c> or <c>OnCommandPattern</c>.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <returns>An instance of <see cref="IBotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public static IBotActionBuilder AsNotExclusive(this IBotActionBuilder builder)
    {
        return builder.SetExclusivityLevel(EExclusivityLevel.None);
    }

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
