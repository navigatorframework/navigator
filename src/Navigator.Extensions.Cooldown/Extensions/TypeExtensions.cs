namespace Navigator.Extensions.Cooldown.Extensions;

internal static class TypeExtensions
{
    public static TimeSpan? GetCooldown(this Type type)
    {
        if (Attribute.GetCustomAttribute(type, typeof(CooldownAttribute)) is CooldownAttribute cooldownAttribute)
        {
            return TimeSpan.FromSeconds(cooldownAttribute.Seconds);
        }

        return default;
    }
}