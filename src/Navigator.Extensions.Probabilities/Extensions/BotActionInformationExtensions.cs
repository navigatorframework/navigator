using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Builder;

namespace Navigator.Extensions.Probabilities.Extensions;

/// <summary>
///     Extensions for Navigator.Extensions.Probabilities .
/// </summary>
public static class BotActionExtensions
{
    private const string ProbabilitiesKey = "extensions.probabilities";

    /// <summary>
    ///     Sets the probabilities of the action being executed.
    /// </summary>
    /// <param name="builder">An instance of <see cref="IBotActionBuilder" />.</param>
    /// <param name="probabilities">The probabilities of the action.</param>
    /// <returns>The same instance of <see cref="IBotActionBuilder" /> supplied as <paramref name="builder" />.</returns>
    public static IBotActionBuilder WithProbabilities(this IBotActionBuilder builder, double probabilities)
    {
        builder.Set(ProbabilitiesKey, probabilities);

        return builder;
    }

    /// <summary>
    ///     Retrieves the probabilities of the action being executed.
    /// </summary>
    /// <param name="information">The information of the action.</param>
    /// <returns>The probabilities of the action or <see langword="null"/> if not set.</returns>
    public static double? GetProbabilities(this BotActionInformation information)
    {
        if (information.Options.GetValueOrDefault(ProbabilitiesKey) is double result) return result;

        return null;
    }
}