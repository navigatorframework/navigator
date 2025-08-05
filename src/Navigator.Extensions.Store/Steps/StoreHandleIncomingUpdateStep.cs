using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Telegram;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Persistence.Context;
using Telegram.Bot.Types.Enums;

namespace Navigator.Extensions.Store.Steps;

internal class StoreHandleIncomingUpdateStep<TDbContext> : IActionExecutionPipelineStepBefore 
    where TDbContext : NavigatorStoreDbContext
{
    private readonly TDbContext _dbContext;
    private readonly ILogger<StoreHandleIncomingUpdateStep<TDbContext>> _logger;

    public StoreHandleIncomingUpdateStep(TDbContext dbContext, ILogger<StoreHandleIncomingUpdateStep<TDbContext>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        switch (context.Update.Type)
        {
            case UpdateType.Message when context.Update.Message is { Type: MessageType.MigrateToChatId } message:
                await HandleMigrateToChatId(message.Chat.Id, message.MigrateToChatId!.Value);
                break;
            default:
                await TryRegisterEntities(context);
                break;
        }
        
        await next();
    }

    private async Task HandleMigrateToChatId(long currentChatId, long newChatId)
    {
        var currentChat = await _dbContext.Chats.FirstOrDefaultAsync(c => c.ExternalId == currentChatId);
        if (currentChat == null) return;
        
        currentChat.ExternalId = newChatId;

        await _dbContext.SaveChangesAsync();
    }

    private async Task TryRegisterEntities(NavigatorActionExecutionContext context)
    {
        var telegramUser = context.Update.GetUserOrDefault();
        var telegramChat = context.Update.GetChatOrDefault();

        if (telegramUser == null)
        {
            _logger.LogWarning("No user found in update {UpdateId}, skipping conversation registration",
                context.Update.Id);
            return;
        }
        
        var user = await HandleUserEntityAsync(telegramUser);
        var chat = await HandleChatEntityAsync(telegramChat);
        await HandleConversationEntityAsync(user, chat);

        await _dbContext.SaveChangesAsync();
    }

    private async Task<User> HandleUserEntityAsync(Telegram.Bot.Types.User telegramUser)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.ExternalId == telegramUser.Id);

        if (user == null)
        {
            user = new User(telegramUser.Id);
            _dbContext.Users.Add(user);
            _logger.LogInformation("Created new user with external Id: {ExternalId}", telegramUser.Id);
        }

        return user;
    }

    private async Task<Chat?> HandleChatEntityAsync(Telegram.Bot.Types.Chat? telegramChat)
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

        return chat;
    }

    private async Task HandleConversationEntityAsync(User user, Chat? chat)
    {
        var conversation = chat is null
            ? await _dbContext.Conversations.FirstOrDefaultAsync(c => c.User.Id == user.Id && c.Chat == null)
            : await _dbContext.Conversations.FirstOrDefaultAsync(c => c.User.Id == user.Id && c.Chat!.Id == chat.Id);

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
    }
}