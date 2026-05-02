using Navigator.Abstractions.Extensions;

namespace Navigator.Extensions.AI;

public class AIOptions : INavigatorExtensionOptions
{
    private int _chatContextLength = 15;

    public int ChatContextLength
    {
        get => _chatContextLength;
        set => _chatContextLength = value > 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Chat context length must be greater than zero.");
    }

    public AiProvider ChatCompletionProvider { get; set; } = new();
    public AiProvider EmbeddingProvider { get; set; } = new();

    public class AiProvider
    {
        public string Name { get; set; } = string.Empty;
        public string ModelId { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = string.Empty;
        public string? ApiKey { get; set; }
        public bool IsMultimodal { get; set; }

        public string GetClientName() => $"{Name}_client";

        internal bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(Name)
                   && !string.IsNullOrWhiteSpace(ModelId)
                   && !string.IsNullOrWhiteSpace(ApiUrl);
        }
    }
}
