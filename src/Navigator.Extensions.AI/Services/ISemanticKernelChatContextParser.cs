using Microsoft.SemanticKernel.ChatCompletion;
using Navigator.Extensions.AI.Models;

namespace Navigator.Extensions.AI.Services;

/// <summary>
///     Converts Navigator chat context into Semantic Kernel chat history.
/// </summary>
public interface ISemanticKernelChatContextParser
{
    /// <summary>
    ///     Converts the supplied chat context into a Semantic Kernel chat history.
    /// </summary>
    /// <param name="context">The chat context to parse.</param>
    /// <returns>The parsed chat history.</returns>
    ChatHistory Parse(ChatContext context);
}
