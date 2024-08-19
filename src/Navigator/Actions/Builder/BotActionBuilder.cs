namespace Navigator.Actions.Builder;

public class BotActionBuilder : IBotActionBuilder
{
    private readonly Guid _id;
    private readonly Delegate _condition;
    private readonly Delegate _handler;
    private readonly Type[] _conditionInputTypes;
    private readonly Type[] _handlerInputTypes;
    private string _actionType { get; set; }
    private ushort _priority { get; set; }
    private TimeSpan? _cooldown { get; set; } 

    public BotAction Build()
    {
        var information = new BotActionInformation
        {
            ActionType = _actionType,
            ConditionInputTypes = _conditionInputTypes,
            HandlerInputTypes = _handlerInputTypes,
            Priority = _priority,
            Cooldown = _cooldown
        };

        return new BotAction(_id, information, _condition, _handler);
    }
    
    public BotActionBuilder(Delegate condition, Delegate handler)
    {
        _id = Guid.NewGuid();
        _condition = condition;
        _conditionInputTypes = condition.Method.GetParameters().Select(info => info.ParameterType).ToArray();
        _handler = handler;
        _handlerInputTypes = handler.Method.GetParameters().Select(info => info.ParameterType).ToArray();
        _priority = Priority.Default;
    }
    
    public IBotActionBuilder WithPriority(ushort priority)
    {
        _priority = priority;
        return this;
    }

    public IBotActionBuilder WithCooldown(TimeSpan cooldown)
    {
        _cooldown = cooldown;
        return this;
    }

    public IBotActionBuilder SetType(string type)
    {
        _actionType = type;
        return this;
    }
}