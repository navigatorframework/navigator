namespace Navigator.Actions.Builder;

public class BotActionBuilder
{
    private readonly Delegate _condition;
    private readonly Type[] _conditionInputTypes;
    private readonly Delegate _handler;
    private readonly Type[] _handlerInputTypes;
    private readonly Guid _id;

    public BotActionBuilder(Delegate condition, Delegate handler)
    {
        _id = Guid.NewGuid();

        if (!(condition.Method.ReturnType != typeof(Task<bool>) || condition.Method.ReturnType != typeof(bool)))
            throw new NavigatorException("The condition delegate must return Task<bool> or bool");

        _condition = condition;
        _conditionInputTypes = condition.Method.GetParameters().Select(info => info.ParameterType).ToArray();
        _handler = handler;
        _handlerInputTypes = handler.Method.GetParameters().Select(info => info.ParameterType).ToArray();
        _priority = Priority.Default;
    }

    private UpdateCategory _category { get; set; }
    private ushort _priority { get; set; }
    private TimeSpan? _cooldown { get; set; }

    /// <summary>
    ///     Builds the bot action.
    /// </summary>
    /// <returns>An instance of <see cref="BotAction" /></returns>
    public BotAction Build()
    {
        var information = new BotActionInformation
        {
            Category = _category,
            ConditionInputTypes = _conditionInputTypes,
            HandlerInputTypes = _handlerInputTypes,
            Priority = _priority,
            Cooldown = _cooldown
        };

        return new BotAction(_id, information, _condition, _handler);
    }

    public BotActionBuilder WithPriority(ushort priority)
    {
        _priority = priority;
        return this;
    }

    public BotActionBuilder WithCooldown(TimeSpan cooldown)
    {
        _cooldown = cooldown;
        return this;
    }

    public BotActionBuilder SetType(UpdateCategory category)
    {
        _category = category;
        return this;
    }
}