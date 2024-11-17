using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Builder;

namespace Navigator.Extensions.Probabilities;

public static class BotActionExtensions
{
    private const string ProbabilitiesKey = "extensions.probabilities";

    public static IBotActionBuilder WithProbabilities(this IBotActionBuilder builder, double probabilities)
    {
        builder.Set(ProbabilitiesKey, probabilities);

        return builder;
    }

    public static double? GetProbabilities(this BotActionInformation information)
    {
        if (information.Options.GetValueOrDefault(ProbabilitiesKey) is double result) return result;

        return null;
    }
}