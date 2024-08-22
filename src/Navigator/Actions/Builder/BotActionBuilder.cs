using Telegram.Bot.Types.Enums;

namespace Navigator.Actions.Builder;

/// <summary>
///     Builder for <see cref="BotAction" />.
/// </summary>
public class BotActionBuilder
{
    private readonly Guid _id;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BotActionBuilder" /> class.
    /// </summary>
    public BotActionBuilder()
    {
        _id = Guid.NewGuid();
        Priority = Actions.Priority.Default;
    }

    private string? Name { get; set; }
    private Delegate? Condition { get; set; }
    private Type[] ConditionInputTypes { get; set; } = null!;
    private Delegate? Handler { get; set; }
    private Type[] HandlerInputTypes { get; set; } = null!;
    private UpdateCategory Category { get; set; } = null!;
    private ushort Priority { get; set; }
    private TimeSpan? Cooldown { get; set; }
    private ChatAction? ChatAction { get; set; }

    /// <summary>
    ///     Builds the bot action.
    /// </summary>
    /// <returns>An instance of <see cref="BotAction" /></returns>
    public BotAction Build()
    {
        var information = new BotActionInformation
        {
            ChatAction = ChatAction,
            Category = Category,
            ConditionInputTypes = ConditionInputTypes,
            HandlerInputTypes = HandlerInputTypes,
            Name = Name ?? $"{_id}",
            Priority = Priority,
            Cooldown = Cooldown
        };

        if (Condition is null || Handler is null)
            throw new NavigatorException("Both condition and handler must be set");

        if (!(Condition.Method.ReturnType != typeof(Task<bool>) || Condition.Method.ReturnType != typeof(bool)))
            throw new NavigatorException("The condition delegate must return Task<bool> or bool");

        if (Category is null)
            throw new NavigatorException("The category must be set");

        return new BotAction(_id, information, Condition, Handler);
    }

    public BotActionBuilder WithName(string name)
    {
        Name = name;

        return this;
    }

    /// <summary>
    ///     Sets the condition of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="condition">The condition to be set.</param>
    /// <returns>An instance of <see cref="BotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public BotActionBuilder SetCondition(Delegate condition)
    {
        Condition = condition;
        ConditionInputTypes = condition.Method.GetParameters().Select(info => info.ParameterType).ToArray();

        return this;
    }

    /// <summary>
    ///     Sets the handler of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="handler">The handler to be set.</param>
    /// <returns>An instance of <see cref="BotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public BotActionBuilder SetHandler(Delegate handler)
    {
        Handler = handler;
        HandlerInputTypes = handler.Method.GetParameters().Select(info => info.ParameterType).ToArray();

        return this;
    }

    /// <summary>
    ///     Sets the <see cref="UpdateCategory" /> of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="category">The <see cref="UpdateCategory" /> to be set.</param>
    /// <returns>An instance of <see cref="BotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public BotActionBuilder SetCategory(UpdateCategory category)
    {
        Category = category;
        return this;
    }

    /// <summary>
    ///     Sets the priority of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="priority">The priority to be set.</param>
    /// <returns>An instance of <see cref="BotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public BotActionBuilder WithPriority(ushort priority)
    {
        Priority = priority;
        return this;
    }

    /// <summary>
    ///     Sets the cooldown of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="cooldown">The cooldown to be set.</param>
    /// <returns>An instance of <see cref="BotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public BotActionBuilder WithCooldown(TimeSpan cooldown)
    {
        Cooldown = cooldown;
        return this;
    }

    /// <summary>
    ///     Sets the <see cref="ChatAction" /> of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="chatAction">The <see cref="ChatAction" /> to be set.</param>
    /// <returns>An instance of <see cref="BotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public BotActionBuilder WithChatAction(ChatAction chatAction)
    {
        ChatAction = chatAction;

        return this;
    }
}