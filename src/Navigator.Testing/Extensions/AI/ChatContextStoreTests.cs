using FluentAssertions;
using Navigator.Extensions.AI.Services;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Extensions.AI;

public class ChatContextStoreTests
{
    [Fact]
    public async Task ShouldStoreTextMessageMetadataAndSingleTextItem()
    {
        var cache = new InMemoryDistributedCache();
        var downloader = Substitute.For<IChatContextMediaDownloader>();
        var store = new ChatContextStore(cache, TestHelpers.CreateOptions(), downloader);
        var update = TestHelpers.CreateTextUpdate(messageId: 42, text: "hello there");

        await store.AppendIncomingMessageAsync(update.Message!);

        var context = await store.GetForUpdateAsync(update);

        context.Should().ContainSingle();
        context[0].Role.Should().Be(global::Navigator.Extensions.AI.Models.ChatContextRoles.User);
        context[0].AuthorName.Should().Be("Lucas Lopez");
        context[0].Metadata["message_id"].Should().Be("42");
        context[0].Metadata["message_type"].Should().Be("Text");
        context[0].Items.Should().ContainSingle();
        context[0].Items[0].Type.Should().Be(global::Navigator.Extensions.AI.Models.ChatContextItemTypes.Text);
        context[0].Items[0].Text.Should().Be("hello there");
    }

    [Fact]
    public async Task ShouldStorePhotoMessageWithDownloadedBytesRegardlessOfMultimodalSetting()
    {
        var cache = new InMemoryDistributedCache();
        var downloader = Substitute.For<IChatContextMediaDownloader>();
        var store = new ChatContextStore(cache, TestHelpers.CreateOptions(isMultimodal: false), downloader);
        var message = TestHelpers.CreatePhotoMessage();
        var update = new Update
        {
            Message = message
        };

        downloader.DownloadAsync("photo-large", Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new byte[] { 1, 2, 3, 4 }));

        await store.AppendIncomingMessageAsync(message);

        var context = await store.GetForUpdateAsync(update);

        context.Should().ContainSingle();
        context[0].Items.Should().HaveCount(2);
        context[0].Items[0].Type.Should().Be(global::Navigator.Extensions.AI.Models.ChatContextItemTypes.Image);
        context[0].Items[0].Data.Should().Equal(1, 2, 3, 4);
        context[0].Items[0].MimeType.Should().Be("image/jpeg");
        context[0].Items[0].Metadata["file_id"].Should().Be("photo-large");
        context[0].Items[0].Metadata["width"].Should().Be("256");
        context[0].Items[0].Metadata["height"].Should().Be("256");
        context[0].Items[1].Type.Should().Be(global::Navigator.Extensions.AI.Models.ChatContextItemTypes.Text);
        context[0].Items[1].Text.Should().Be("photo caption");
    }
}
