using FluentAssertions;
using Navigator.Abstractions.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using TelegramUser = Telegram.Bot.Types.User;

namespace Navigator.Testing.Entities;

public class BotExtensionsTest
{
    private readonly Bot _bot = new(123, "TestBot") { Username = "test_bot" };

    #region IsRepliedTo

    [Fact]
    public void ShouldDetectDirectReplyToBot()
    {
        var message = new Message
        {
            ReplyToMessage = new Message
            {
                From = new TelegramUser { Id = 123, FirstName = "TestBot", IsBot = true }
            }
        };

        _bot.IsRepliedTo(message).Should().BeTrue();
    }

    [Fact]
    public void ShouldNotDetectReplyToOtherUser()
    {
        var message = new Message
        {
            ReplyToMessage = new Message
            {
                From = new TelegramUser { Id = 456, FirstName = "Other", IsBot = false }
            }
        };

        _bot.IsRepliedTo(message).Should().BeFalse();
    }

    [Fact]
    public void ShouldNotDetectReplyWhenNoneExists()
    {
        var message = new Message { Text = "Hello" };

        _bot.IsRepliedTo(message).Should().BeFalse();
    }

    #endregion

    #region IsMentioned

    [Fact]
    public void ShouldDetectAtUsernameMention()
    {
        var message = new Message
        {
            Text = "Hey @test_bot how are you?",
            Entities =
            [
                new MessageEntity
                {
                    Type = MessageEntityType.Mention,
                    Offset = 4,
                    Length = 9
                }
            ]
        };

        _bot.IsMentioned(message).Should().BeTrue();
    }

    [Fact]
    public void ShouldDetectTextMention()
    {
        var message = new Message
        {
            Text = "Hey TestBot how are you?",
            Entities =
            [
                new MessageEntity
                {
                    Type = MessageEntityType.TextMention,
                    Offset = 4,
                    Length = 7,
                    User = new TelegramUser { Id = 123, FirstName = "TestBot", IsBot = true }
                }
            ]
        };

        _bot.IsMentioned(message).Should().BeTrue();
    }

    [Fact]
    public void ShouldDetectFirstNameInText()
    {
        var message = new Message { Text = "Hello TestBot, nice to meet you!" };

        _bot.IsMentioned(message).Should().BeTrue();
    }

    [Fact]
    public void ShouldDetectMentionInCaption()
    {
        var message = new Message
        {
            Caption = "Photo with @test_bot",
            CaptionEntities =
            [
                new MessageEntity
                {
                    Type = MessageEntityType.Mention,
                    Offset = 11,
                    Length = 9
                }
            ]
        };

        _bot.IsMentioned(message).Should().BeTrue();
    }

    [Fact]
    public void ShouldNotDetectMentionWhenAbsent()
    {
        var message = new Message { Text = "Hello everyone!" };

        _bot.IsMentioned(message).Should().BeFalse();
    }

    #endregion

    #region Update overloads

    [Fact]
    public void ShouldDetectReplyInUpdate()
    {
        var update = new Update
        {
            Message = new Message
            {
                ReplyToMessage = new Message
                {
                    From = new TelegramUser { Id = 123, FirstName = "TestBot", IsBot = true }
                }
            }
        };

        _bot.IsRepliedTo(update).Should().BeTrue();
    }

    [Fact]
    public void ShouldReturnFalseForUpdateWithoutMessage()
    {
        var update = new Update();

        _bot.IsRepliedTo(update).Should().BeFalse();
        _bot.IsMentioned(update).Should().BeFalse();
    }

    #endregion
}
