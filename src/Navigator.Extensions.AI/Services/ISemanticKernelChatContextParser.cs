using Microsoft.SemanticKernel.ChatCompletion;
using Navigator.Extensions.AI.Models;

namespace Navigator.Extensions.AI.Services;

public interface ISemanticKernelChatContextParser
{
    ChatHistory Parse(ChatContext context);
}
