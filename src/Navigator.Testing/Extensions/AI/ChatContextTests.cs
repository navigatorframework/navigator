using FluentAssertions;
using Xunit;

namespace Navigator.Testing.Extensions.AI;

public class ChatContextTests
{
    [Fact]
    public void ShouldDiscardOldestMessage_WhenBufferExceedsConfiguredLength()
    {
        var context = new global::Navigator.Extensions.AI.Models.ChatContext(15);

        for (var i = 1; i <= 16; i++)
        {
            context.Add(new global::Navigator.Extensions.AI.Models.ChatContextMessage
            {
                Metadata = new Dictionary<string, string?>
                {
                    ["message_id"] = i.ToString()
                }
            });
        }

        context.Should().HaveCount(15);
        context[0].Metadata["message_id"].Should().Be("2");
        context[^1].Metadata["message_id"].Should().Be("16");
    }
}
