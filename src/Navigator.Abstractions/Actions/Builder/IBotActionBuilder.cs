namespace Navigator.Abstractions.Actions.Builder;

/// <summary>
///     Interface for building <see cref="BotAction" />.
/// </summary>
public interface IBotActionBuilder
{
    /// <summary>
    ///     Sets an option value given a key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Set(string key, object value);

    /// <summary>
    ///     Retrieves an option value given a key.
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="TValue"></typeparam>
    public TValue? Get<TValue>(string key);

    /// <summary>
    ///     Builds a <see cref="BotAction" />.
    /// </summary>
    /// <returns>An instance of <see cref="BotAction" />.</returns>
    public BotAction Build();
}