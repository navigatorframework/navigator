using Navigator.Entities;

namespace Navigator.Context;

/// <summary>
/// Defines a <see cref="GetConversationAsync"/> source for <see cref="INavigatorContextBuilder"/> to use
/// while building a <see cref="NavigatorContext"/>.
/// </summary>
public interface INavigatorContextBuilderConversationSource
{
    /// <summary>
    /// Returns a <see cref="Conversation"/> in accordance to the original event that spawned this context.
    /// </summary>
    /// <param name="originalEvent"></param>
    /// <returns><see cref="Conversation"/></returns>
    Task<Conversation> GetConversationAsync(object? originalEvent);
}