namespace Navigator.Abstractions.Actions;

/// <summary>
///     A <see cref="BotAction" /> is ah representation of an action that can be executed by a navigator bot. It is used to encapsulate a
///     condition and a handler. The condition is a delegate that is checked at runtime and if it evaluates to true, the handler is executed.
///     The condition delegate should return a boolean or a Task that resolves to a boolean. The handler delegate should return void or a Task
///     that resolves to void.
/// </summary>
public sealed record BotAction
{
    private readonly Delegate _condition;
    private readonly Delegate _handler;

    /// <summary>
    ///     The id of the <see cref="BotAction" />.
    /// </summary>
    public readonly Guid Id;

    /// <summary>
    ///     The information about the <see cref="BotAction" />.
    /// </summary>
    public readonly BotActionInformation Information;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BotAction" /> class.
    /// </summary>
    /// <param name="id">The id of the <see cref="BotAction" />.</param>
    /// <param name="information">The information about the <see cref="BotAction" />.</param>
    /// <param name="condition">The condition delegate.</param>
    /// <param name="handler">The handler delegate.</param>
    public BotAction(Guid id, BotActionInformation information, Delegate condition, Delegate handler)
    {
        Id = id;
        Information = information;
        _condition = condition;
        _handler = handler;
    }

    /// <summary>
    ///     Executes the condition of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="args">
    ///     The arguments that are passed to the condition delegate.
    /// </param>
    /// <returns>
    ///     A boolean that indicates if the condition is true, and therefore the handler should be executed.
    /// </returns>
    /// <exception cref="NavigatorException">When the condition delegate returns a something that is not a Task or a boolean.</exception>
    public async Task<bool> ExecuteCondition(object?[] args)
    {
        var result = _condition.Method.Invoke(_condition.Target, args);

        return result switch
        {
            Task<bool> task => await task,
            bool b => b,
            //TODO: specify exception
            _ => throw new NavigatorException()
        };
    }

    /// <summary>
    ///     Executes the handler of the <see cref="BotAction" />.
    /// </summary>
    /// <param name="args">
    ///     The arguments that are passed to the handler delegate.
    /// </param>
    public async Task ExecuteHandler(object?[] args)
    {
        var result = _handler.Method.Invoke(_handler.Target, args);

        if (result is Task task) await task;
    }
}