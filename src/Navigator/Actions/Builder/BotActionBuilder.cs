using Navigator.Abstractions;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Builder;
using Navigator.Abstractions.Priorities;
using Navigator.Actions.Builder.Extensions;
using Telegram.Bot.Types.Enums;

namespace Navigator.Actions.Builder;

/// <summary>
///     Builder for <see cref="BotAction" />.
/// </summary>
public class BotActionBuilder : IBotActionBuilder
{
    private readonly Guid _id;
    private readonly Dictionary<string, object> _options = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="BotActionBuilder" /> class.
    /// </summary>
    public BotActionBuilder()
    {
        _id = Guid.NewGuid();
        // Set default priority using extension method
        this.WithPriority(EPriority.Normal);
    }

    /// <inheritdoc />
    public void Set(string key, object value)
    {
        _options[key] = value;
    }

    /// <inheritdoc />
    public TValue? Get<TValue>(string key)
    {
        return _options.TryGetValue(key, out var value) && value is TValue result ? result : default;
    }

    /// <inheritdoc />
    public BotAction Build()
    {
        var name = this.GetName();
        var condition = this.GetCondition();
        var conditionInputTypes = this.GetConditionInputTypes() ?? [];
        var handler = this.GetHandler();
        var handlerInputTypes = this.GetHandlerInputTypes() ?? [];
        var category = this.GetCategory();
        var priority = this.GetPriority();
        var chatAction = this.GetChatAction();

        if (condition is null || handler is null)
            throw new NavigatorException("Both condition and handler must be set");

        if (!(condition.Method.ReturnType != typeof(Task<bool>) || condition.Method.ReturnType != typeof(bool)))
            throw new NavigatorException("The condition delegate must return Task<bool> or bool");

        if (category is null)
            throw new NavigatorException("The category must be set");
        
        var information = new BotActionInformation
        {
            ChatAction = chatAction,
            Category = category,
            ConditionInputTypes = conditionInputTypes,
            HandlerInputTypes = handlerInputTypes,
            Name = name ?? $"{_id}",
            Priority = priority,
            Options = _options
        };

        return new BotAction(_id, information, condition, handler);
    }
}