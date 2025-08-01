using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Telegram;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Persistence.Context;

namespace Navigator.Extensions.Store.Steps;

public class RegisterConversationStep : IActionExecutionPipelineStepBefore
{
    private readonly NavigatorStoreDbContext _dbContext;
    private readonly ILogger<RegisterConversationStep> _logger;

    public RegisterConversationStep(NavigatorStoreDbContext dbContext, ILogger<RegisterConversationStep> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        var telegramUser = context.Update.GetUserOrDefault();
        var telegramChat = context.Update.GetChatOrDefault();

        if (telegramUser == null)
        {
            _logger.LogWarning("No user found in update {UpdateId}, skipping conversation registration",
                context.Update.Id);
            await next();
            return;
        }

        var now = TimeProvider.System.GetUtcNow();

        var user = await HandleUserEntityAsync(telegramUser, now);
        var chat = await HandleChatEntityAsync(telegramChat, now);
        await HandleConversationEntityAsync(user, chat, now);

        await _dbContext.SaveChangesAsync();
        await next();
    }

    private async Task<User> HandleUserEntityAsync(Telegram.Bot.Types.User telegramUser, DateTimeOffset now)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.ExternalId == telegramUser.Id);

        if (user == null)
        {
            user = new User(telegramUser.Id);
            _dbContext.Users.Add(user);
            _logger.LogInformation("Created new user with external Id: {ExternalId}", telegramUser.Id);
        }
        else
        {
            user.LastActiveAt = now;
            _logger.LogDebug("Updated LastActiveAt for user with external Id: {ExternalId}", telegramUser.Id);
        }

        return user;
    }

    private async Task<Chat?> HandleChatEntityAsync(Telegram.Bot.Types.Chat? telegramChat, DateTimeOffset now)
    {
        if (telegramChat == null)
            return null;

        var chat = await _dbContext.Chats
            .FirstOrDefaultAsync(c => c.ExternalId == telegramChat.Id);

        if (chat == null)
        {
            chat = new Chat(telegramChat.Id);
            _dbContext.Chats.Add(chat);
            _logger.LogDebug("Created new chat with external ID {ExternalId}", telegramChat.Id);
        }
        else
        {
            chat.LastActiveAt = now;
            _logger.LogDebug("Updated LastActiveAt for chat with external ID {ExternalId}", telegramChat.Id);
        }

        return chat;
    }

    private async Task HandleConversationEntityAsync(User user, Chat? chat, DateTimeOffset now)
    {
        var conversation = chat is null
            ? await _dbContext.Conversations.FirstOrDefaultAsync(c => c.User.Id == user.Id && c.Chat == null)
            : await _dbContext.Conversations.FirstOrDefaultAsync(c => c.User.Id == user.Id && c.Chat!.Id == chat!.Id);

        if (conversation == null)
        {
            conversation = new Conversation(user)
            {
                Chat = chat
            };
            _dbContext.Conversations.Add(conversation);
            _logger.LogDebug("Created new conversation for user {UserId} and chat {ChatId}", user.ExternalId,
                chat?.ExternalId);
        }
        else
        {
            conversation.LastActiveAt = now;
            _logger.LogDebug("Updated LastActiveAt for conversation between user {UserId} and chat {ChatId}",
                user.ExternalId, chat?.ExternalId);
        }
    }
}