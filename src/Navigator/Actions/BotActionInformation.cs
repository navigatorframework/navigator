namespace Navigator.Actions;

public record BotActionInformation
{
    public required string ActionType;
    public required Type[] ConditiionInputTypes;
    public required Type[] HandlerInputTypes;
    public required ushort Priority;
    public required TimeSpan? Cooldown;
}