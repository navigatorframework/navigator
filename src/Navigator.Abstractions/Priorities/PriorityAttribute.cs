namespace Navigator.Abstractions.Priorities;

/// <summary>
///     Attribute for setting the priority of something.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class PriorityAttribute : Attribute
{
    /// <summary>
    ///     Sets the priority of something.
    /// </summary>
    /// <param name="level"></param>
    public PriorityAttribute(EPriority level)
    {
        Level = level;
    }

    /// <summary>
    ///     The level of the priority.
    /// </summary>
    public EPriority Level { get; }
}