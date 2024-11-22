using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Builder;

namespace Navigator.Extensions.Cooldown.Extensions;

/// <summary>
///     Extensions for Navigator.Extensions.Probabilities .
/// </summary>
public static class BotActionExtensions
{
    private const string CooldownKey = "extensions.cooldown";

    /// <summary>
    ///     Sets the cooldown for an action.
    /// </summary>
    /// <param name="builder">An instance of <see cref="IBotActionBuilder" />.</param>
    /// <param name="cooldown">The cooldown to be set.</param>
    /// <returns>The same instance of <see cref="IBotActionBuilder" /> supplied as <paramref name="builder" />.</returns>
    public static IBotActionBuilder WithCooldown(this IBotActionBuilder builder, TimeSpan cooldown)
    {
        builder.Set(CooldownKey, cooldown);

        return builder;
    }

    /// <summary>
    ///     Retrieves the cooldown for an action.
    /// </summary>
    /// <param name="information">The information of the action.</param>
    /// <returns>The cooldown of the action or <see cref="TimeSpan.Zero"/> if not set.</returns>
    public static TimeSpan GetCooldown(this BotActionInformation information)
    {
        if (information.Options.GetValueOrDefault(CooldownKey) is TimeSpan result) return result;

        return TimeSpan.Zero;
    }
}