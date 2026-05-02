using FluentAssertions;
using Navigator.Extensions.AI.Resolvers;
using Navigator.Extensions.AI.Services;
using NSubstitute;
using Xunit;

namespace Navigator.Testing.Extensions.AI;

public class ChatContextArgumentResolverTests
{
    [Fact]
    public async Task ShouldReturnEmptyContextWhenCacheEntryDoesNotExist_AndPopulatedContextAfterStorage()
    {
        var cache = new InMemoryDistributedCache();
        var downloader = Substitute.For<IChatContextMediaDownloader>();
        var store = new ChatContextStore(cache, TestHelpers.CreateOptions(), downloader);
        var resolver = new ChatContextArgumentResolver(store);
        var update = TestHelpers.CreateTextUpdate(messageId: 7, text: "cached");
        var action = TestHelpers.CreateAction();

        var emptyResult = await resolver.GetArgument(typeof(global::Navigator.Extensions.AI.Models.ChatContext), update, action);

        emptyResult.Should().BeOfType<global::Navigator.Extensions.AI.Models.ChatContext>();
        ((global::Navigator.Extensions.AI.Models.ChatContext)emptyResult!).Should().BeEmpty();

        await store.AppendIncomingMessageAsync(update.Message!);

        var populatedResult = await resolver.GetArgument(typeof(global::Navigator.Extensions.AI.Models.ChatContext), update, action);

        populatedResult.Should().BeOfType<global::Navigator.Extensions.AI.Models.ChatContext>();
        ((global::Navigator.Extensions.AI.Models.ChatContext)populatedResult!).Should().ContainSingle();
    }
}
