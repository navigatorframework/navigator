namespace Navigator.Actions.Builder;

/// <summary>
///     Builder for <see cref="BotAction" />.
/// </summary>
public class BotActionBuilder
{
    private readonly Delegate _condition;
    private readonly Type[] _conditionInputTypes;
    private readonly Delegate _handler;
    private readonly Type[] _handlerInputTypes;
    private readonly Guid _id;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BotActionBuilder" /> class.
    /// </summary>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />.
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    public BotActionBuilder(Delegate condition, Delegate handler)
    {
        _id = Guid.NewGuid();

        if (!(condition.Method.ReturnType != typeof(Task<bool>) || condition.Method.ReturnType != typeof(bool)))
            throw new NavigatorException("The condition delegate must return Task<bool> or bool");

        _condition = condition;
        _conditionInputTypes = condition.Method.GetParameters().Select(info => info.ParameterType).ToArray();
        _handler = handler;
        _handlerInputTypes = handler.Method.GetParameters().Select(info => info.ParameterType).ToArray();
        Priority = Actions.Priority.Default;
    }

    private UpdateCategory Category { get; set; } = null!;
    private ushort Priority { get; set; }
    private TimeSpan? Cooldown { get; set; }

    /// <summary>
    ///     Builds the bot action.
    /// </summary>
    /// <returns>An instance of <see cref="BotAction" /></returns>
    public BotAction Build()
    {
        var information = new BotActionInformation
        {
            Category = Category,
            ConditionInputTypes = _conditionInputTypes,
            HandlerInputTypes = _handlerInputTypes,
            Priority = Priority,
            Cooldown = Cooldown
        };

        return new BotAction(_id, information, _condition, _handler);
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
    ///     Sets the <see cref="UpdateCategory" /> of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="category">The <see cref="UpdateCategory" /> to be set.</param>
    /// <returns>An instance of <see cref="BotActionBuilder" /> to be able to continue configuring the <see cref="BotAction" />.</returns>
    public BotActionBuilder SetCategory(UpdateCategory category)
    {
        Category = category;
        return this;
    }
}