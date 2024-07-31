using Navigator.Actions;
using Navigator.Configuration;
using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Builder;

public class BotActionBuilder : IBotActionBuilder
{
    public BotAction BotAction { get; set; }
    
    public BotActionBuilderOptions Options { get; }

    public BotActionBuilder(BotAction botAction)
    {
        BotAction = botAction;
        Options = new BotActionBuilderOptions();
    }
}