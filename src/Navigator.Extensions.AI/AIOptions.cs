using Navigator.Abstractions.Extensions;

namespace Navigator.Extensions.AI;

/// <summary>
///     Configures the AI extension services and provider settings.
/// </summary>
public class AIOptions : INavigatorExtensionOptions
{
    private int _chatContextLength = 15;

    /// <summary>
    ///     Gets or sets the maximum number of messages preserved in each chat context.
    /// </summary>
    public int ChatContextLength
    {
        get => _chatContextLength;
        set => _chatContextLength = value > 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Chat context length must be greater than zero.");
    }

    /// <summary>
    ///     Gets or sets the provider used for chat completions.
    /// </summary>
    public AiProvider ChatCompletionProvider { get; set; } = new();

    /// <summary>
    ///     Gets or sets the provider used for embeddings.
    /// </summary>
    public AiProvider EmbeddingProvider { get; set; } = new();

    /// <summary>
    ///     Describes a single AI provider registration.
    /// </summary>
    public class AiProvider
    {
        /// <summary>
        ///     Gets or sets the logical provider name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the model identifier used for requests.
        /// </summary>
        public string ModelId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the base API URL for the provider.
        /// </summary>
        public string ApiUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the API key used to authenticate requests.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the provider accepts multimodal content.
        /// </summary>
        public bool IsMultimodal { get; set; }

        /// <summary>
        ///     Gets the registered HTTP client name for this provider.
        /// </summary>
        /// <returns>The HTTP client name.</returns>
        public string GetClientName() => $"{Name}_client";

        internal bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(Name)
                   && !string.IsNullOrWhiteSpace(ModelId)
                   && !string.IsNullOrWhiteSpace(ApiUrl);
        }
    }
}
