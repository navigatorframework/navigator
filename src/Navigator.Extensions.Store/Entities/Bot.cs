namespace Navigator.Extensions.Store.Entities;

/// <summary>
/// Bot.
/// </summary>
public class Bot : User
{
    /// <summary>
    /// Internal constructor.
    /// </summary>
    protected Bot()
    {
    }

    /// <summary>
    /// Bot constructor from <see cref="Navigator.Entities.Bot"/>
    /// </summary>
    /// <param name="bot"></param>
    public Bot(Navigator.Entities.Bot bot) : base(bot)
    {
    }
}