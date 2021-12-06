namespace Navigator.Extensions.Cooldown;

[AttributeUsage(AttributeTargets.Class)]
public class CooldownAttribute : Attribute
{
    public int Seconds { get; set; } = 300;
}
