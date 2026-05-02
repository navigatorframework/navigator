#pragma warning disable SKEXP0001

using FluentAssertions;
using Microsoft.SemanticKernel;
using Navigator.Extensions.AI.Services;
using Xunit;

namespace Navigator.Testing.Extensions.AI;

public class SemanticKernelChatContextParserTests
{
    [Fact]
    public void ShouldEmitFallbackTextForAudio_WhenProviderIsNotMultimodal()
    {
        var parser = new SemanticKernelChatContextParser(TestHelpers.CreateOptions(isMultimodal: false));
        var context = new global::Navigator.Extensions.AI.Models.ChatContext(15,
        [
            new global::Navigator.Extensions.AI.Models.ChatContextMessage
            {
                Metadata = new Dictionary<string, string?>
                {
                    ["message_id"] = "9"
                },
                Items =
                [
                    new global::Navigator.Extensions.AI.Models.ChatContextItem
                    {
                        Type = global::Navigator.Extensions.AI.Models.ChatContextItemTypes.Audio,
                        Data = [1, 2, 3],
                        MimeType = "audio/ogg"
                    },
                    new global::Navigator.Extensions.AI.Models.ChatContextItem
                    {
                        Type = global::Navigator.Extensions.AI.Models.ChatContextItemTypes.Text,
                        Text = "caption"
                    }
                ]
            }
        ]);

        var history = parser.Parse(context);

        history.Count.Should().Be(1);
        history[0].Items.Should().HaveCount(2);
        history[0].Items[0].Should().BeOfType<TextContent>();
        ((TextContent)history[0].Items[0]).Text.Should().Be("User sent an audio message.");
        history[0].Items[1].Should().BeOfType<TextContent>();
        ((TextContent)history[0].Items[1]).Text.Should().Be("caption");
        history[0].Metadata.Should().ContainKey("message_id");
        history[0].Metadata!["message_id"].Should().Be("9");
    }

    [Fact]
    public void ShouldEmitRealMediaContentAndCaptionText_WhenProviderIsMultimodal()
    {
        var parser = new SemanticKernelChatContextParser(TestHelpers.CreateOptions(isMultimodal: true));
        var context = new global::Navigator.Extensions.AI.Models.ChatContext(15,
        [
            new global::Navigator.Extensions.AI.Models.ChatContextMessage
            {
                AuthorName = "Lucas Lopez",
                Items =
                [
                    new global::Navigator.Extensions.AI.Models.ChatContextItem
                    {
                        Type = global::Navigator.Extensions.AI.Models.ChatContextItemTypes.Image,
                        Data = [4, 5, 6],
                        MimeType = "image/jpeg"
                    },
                    new global::Navigator.Extensions.AI.Models.ChatContextItem
                    {
                        Type = global::Navigator.Extensions.AI.Models.ChatContextItemTypes.Text,
                        Text = "look at this"
                    }
                ]
            }
        ]);

        var history = parser.Parse(context);

        history.Count.Should().Be(1);
        history[0].AuthorName.Should().Be("Lucas Lopez");
        history[0].Items.Should().HaveCount(2);
        history[0].Items[0].Should().BeOfType<ImageContent>();
        ((ImageContent)history[0].Items[0]).MimeType.Should().Be("image/jpeg");
        history[0].Items[1].Should().BeOfType<TextContent>();
        ((TextContent)history[0].Items[1]).Text.Should().Be("look at this");
    }
}
