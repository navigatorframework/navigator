using FluentAssertions;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Extensions.AI.Services;
using Navigator.Extensions.AI.Steps;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Extensions.AI;

public class StoreIncomingMessageInChatContextStepTests
{
    [Fact]
    public async Task ShouldIgnoreNonMessageUpdates()
    {
        var store = Substitute.For<IChatContextStore>();
        var step = new StoreIncomingMessageInChatContextStep(store);
        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update
        {
            CallbackQuery = new CallbackQuery
            {
                Id = "callback-id",
                ChatInstance = "chat-instance",
                From = TestHelpers.CreateUser("Lucas", "Lopez")
            }
        }));

        var nextCalled = false;

        await step.InvokeAsync(context, () =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        });

        await store.DidNotReceive().AppendIncomingMessageAsync(Arg.Any<Message>(), Arg.Any<CancellationToken>());
        nextCalled.Should().BeTrue();
    }
}
