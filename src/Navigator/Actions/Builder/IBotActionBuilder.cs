namespace Navigator.Actions.Builder;

public interface IBotActionBuilder
{
    public BotAction Build();
    public IBotActionBuilder WithPriority(ushort priority);
    public IBotActionBuilder WithCooldown(TimeSpan cooldown);
    public IBotActionBuilder SetType(string type);
}