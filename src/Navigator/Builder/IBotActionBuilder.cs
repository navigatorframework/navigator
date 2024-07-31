using Navigator.Actions;
using Navigator.Configuration;

namespace Navigator.Builder;

public interface IBotActionBuilder
{
    public BotAction BotAction { get; }
    public BotActionBuilderOptions Options { get; }
}