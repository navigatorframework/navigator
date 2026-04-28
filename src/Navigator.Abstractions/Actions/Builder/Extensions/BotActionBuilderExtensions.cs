using Navigator.Abstractions.Actions.Builder;
using Navigator.Abstractions.Priorities;
using Telegram.Bot.Types.Enums;

namespace Navigator.Abstractions.Actions.Builder.Extensions;

/// <summary>
///     Extension methods for <see cref="IBotActionBuilder" />.
/// </summary>
public static class BotActionBuilderExtensions
{
    /// <summary>
    ///     Key for the name option.
    /// </summary>
    public const string NameKey = "builder.name";

    /// <summary>
    ///     Key for the condition option.
    /// </summary>
    public const string ConditionKey = "builder.condition";

    /// <summary>
    ///     Key for the condition input types option.
    /// </summary>
    public const string ConditionInputTypesKey = "builder.condition_input_types";

    /// <summary>
    ///     Key for the handler option.
    /// </summary>
    public const string HandlerKey = "builder.handler";

    /// <summary>
    ///     Key for the handler input types option.
    /// </summary>
    public const string HandlerInputTypesKey = "builder.handler_input_types";

    /// <summary>
    ///     Key for the category option.
    /// </summary>
    public const string CategoryKey = "builder.category";

    /// <summary>
    ///     Key for the priority option.
    /// </summary>
    public const string PriorityKey = "builder.priority";

    /// <summary>
    ///     Key for the chat action option.
    /// </summary>
    public const string ChatActionKey = "builder.chat_action";

    /// <summary>
    ///     Key for the exclusivity level option.
    /// </summary>
    public const string ExclusivityLevelKey = "builder.exclusivity_level";

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
}
