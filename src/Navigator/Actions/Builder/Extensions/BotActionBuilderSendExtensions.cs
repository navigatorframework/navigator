using Navigator.Abstractions.Actions.Builder;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Abstractions.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Abstractions.Entities.Chat;

namespace Navigator.Actions.Builder.Extensions;

/// <summary>
///     Extension methods for <see cref="BotActionBuilder" /> that provide pre-built handlers for sending
///     Telegram content (text, photos, video, stickers, etc.) and their randomized variants.
/// </summary>
public static class BotActionBuilderSendExtensions
{
    private static ReplyParameters? BuildReplyParameters(bool asReply, bool toReply, Message? message)
    {
        if (asReply && message is not null) return message;
        if (toReply && message?.ReplyToMessage is not null) return message.ReplyToMessage;
        
        return null;
    }

    #region Standard Send Extensions

    /// <summary>
    ///     Sets the handler to send a text message.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="text">The text content to send.</param>
    /// <param name="parseMode">Optional parse mode for formatting (HTML, Markdown, MarkdownV2).</param>
    /// <param name="asReply">If true, sends the text as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the text as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendText(this IBotActionBuilder builder, string text,
        ParseMode? parseMode = null, bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendMessage(chat, text, parseMode: parseMode ?? ParseMode.None, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.Typing);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a photo.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="photo">The photo to send (file ID or URL).</param>
    /// <param name="caption">Optional caption for the photo.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="hasSpoiler">If true, the photo is sent with a spoiler animation.</param>
    /// <param name="asReply">If true, sends the photo as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the photo as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendPhoto(this IBotActionBuilder builder, string photo,
        string? caption = null, ParseMode? parseMode = null, bool hasSpoiler = false,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendPhoto(chat, photo, caption: caption, parseMode: parseMode ?? ParseMode.None,
                hasSpoiler: hasSpoiler, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadPhoto);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a video.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="video">The video to send (file ID or URL).</param>
    /// <param name="caption">Optional caption for the video.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="hasSpoiler">If true, the video is sent with a spoiler animation.</param>
    /// <param name="asReply">If true, sends the video as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the video as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendVideo(this IBotActionBuilder builder, string video,
        string? caption = null, ParseMode? parseMode = null, bool hasSpoiler = false,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendVideo(chat, video, caption: caption, parseMode: parseMode ?? ParseMode.None,
                hasSpoiler: hasSpoiler, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadVideo);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a sticker.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="sticker">The sticker to send (file ID or URL).</param>
    /// <param name="asReply">If true, sends the sticker as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the sticker as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendSticker(this IBotActionBuilder builder, string sticker,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendSticker(chat, sticker, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.ChooseSticker);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send an audio file.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="audio">The audio to send (file ID or URL).</param>
    /// <param name="caption">Optional caption for the audio.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="asReply">If true, sends the audio file as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the audio file as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendAudio(this IBotActionBuilder builder, string audio,
        string? caption = null, ParseMode? parseMode = null,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendAudio(chat, audio, caption: caption, parseMode: parseMode ?? ParseMode.None,
                replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadDocument);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send an animation (GIF).
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="animation">The animation to send (file ID or URL).</param>
    /// <param name="caption">Optional caption for the animation.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="hasSpoiler">If true, the animation is sent with a spoiler animation.</param>
    /// <param name="asReply">If true, sends the animation as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the animation as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendAnimation(this IBotActionBuilder builder, string animation,
        string? caption = null, ParseMode? parseMode = null, bool hasSpoiler = false,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendAnimation(chat, animation, caption: caption, parseMode: parseMode ?? ParseMode.None,
                hasSpoiler: hasSpoiler, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadPhoto);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a document.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="document">The document to send (file ID or URL).</param>
    /// <param name="caption">Optional caption for the document.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="asReply">If true, sends the document as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the document as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendDocument(this IBotActionBuilder builder, string document,
        string? caption = null, ParseMode? parseMode = null,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendDocument(chat, document, caption: caption, parseMode: parseMode ?? ParseMode.None,
                replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadDocument);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a voice message.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="voice">The voice message to send (file ID or URL).</param>
    /// <param name="caption">Optional caption for the voice message.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="asReply">If true, sends the voice message as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the voice message as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendVoice(this IBotActionBuilder builder, string voice,
        string? caption = null, ParseMode? parseMode = null,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendVoice(chat, voice, caption: caption, parseMode: parseMode ?? ParseMode.None,
                replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadVoice);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a dice animation.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="emoji">Optional emoji for the dice type (defaults to standard dice).</param>
    /// <param name="asReply">If true, sends the dice as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the dice as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendDice(this IBotActionBuilder builder,
        string? emoji = null, bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendDice(chat, emoji: emoji, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.Typing);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a location.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="latitude">Latitude of the location.</param>
    /// <param name="longitude">Longitude of the location.</param>
    /// <param name="asReply">If true, sends the location as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the location as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    public static IBotActionBuilder SendLocation(this IBotActionBuilder builder,
        double latitude, double longitude, bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendLocation(chat, latitude, longitude, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.FindLocation);

        return builder;
    }

    #endregion

    #region Random Send Extensions

    /// <summary>
    ///     Sets the handler to send a randomly selected text message from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="texts">A non-empty list of text messages to choose from at random.</param>
    /// <param name="parseMode">Optional parse mode for formatting (HTML, Markdown, MarkdownV2).</param>
    /// <param name="asReply">If true, sends the random text as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random text as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="texts" /> is empty.</exception>
    public static IBotActionBuilder SendRandomText(this IBotActionBuilder builder, IReadOnlyList<string> texts,
        ParseMode? parseMode = null, bool asReply = false, bool toReply = false)
    {
        if (texts.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(texts));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var text = texts[Random.Shared.Next(texts.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendMessage(chat, text, parseMode: parseMode ?? ParseMode.None, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.Typing);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a randomly selected photo from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="photos">A non-empty list of photos (file IDs or URLs) to choose from at random.</param>
    /// <param name="caption">Optional caption for the photo.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="hasSpoiler">If true, the photo is sent with a spoiler animation.</param>
    /// <param name="asReply">If true, sends the random photo as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random photo as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="photos" /> is empty.</exception>
    public static IBotActionBuilder SendRandomPhoto(this IBotActionBuilder builder, IReadOnlyList<string> photos,
        string? caption = null, ParseMode? parseMode = null, bool hasSpoiler = false,
        bool asReply = false, bool toReply = false)
    {
        if (photos.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(photos));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var photo = photos[Random.Shared.Next(photos.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendPhoto(chat, photo, caption: caption, parseMode: parseMode ?? ParseMode.None,
                hasSpoiler: hasSpoiler, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadPhoto);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a randomly selected video from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="videos">A non-empty list of videos (file IDs or URLs) to choose from at random.</param>
    /// <param name="caption">Optional caption for the video.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="hasSpoiler">If true, the video is sent with a spoiler animation.</param>
    /// <param name="asReply">If true, sends the random video as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random video as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="videos" /> is empty.</exception>
    public static IBotActionBuilder SendRandomVideo(this IBotActionBuilder builder, IReadOnlyList<string> videos,
        string? caption = null, ParseMode? parseMode = null, bool hasSpoiler = false,
        bool asReply = false, bool toReply = false)
    {
        if (videos.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(videos));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var video = videos[Random.Shared.Next(videos.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendVideo(chat, video, caption: caption, parseMode: parseMode ?? ParseMode.None,
                hasSpoiler: hasSpoiler, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadVideo);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a randomly selected sticker from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="stickers">A non-empty list of stickers (file IDs or URLs) to choose from at random.</param>
    /// <param name="asReply">If true, sends the random sticker as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random sticker as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="stickers" /> is empty.</exception>
    public static IBotActionBuilder SendRandomSticker(this IBotActionBuilder builder, IReadOnlyList<string> stickers,
        bool asReply = false, bool toReply = false)
    {
        if (stickers.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(stickers));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var sticker = stickers[Random.Shared.Next(stickers.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendSticker(chat, sticker, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.ChooseSticker);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a randomly selected audio file from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="audios">A non-empty list of audio files (file IDs or URLs) to choose from at random.</param>
    /// <param name="caption">Optional caption for the audio.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="asReply">If true, sends the random audio file as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random audio file as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="audios" /> is empty.</exception>
    public static IBotActionBuilder SendRandomAudio(this IBotActionBuilder builder, IReadOnlyList<string> audios,
        string? caption = null, ParseMode? parseMode = null,
        bool asReply = false, bool toReply = false)
    {
        if (audios.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(audios));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var audio = audios[Random.Shared.Next(audios.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendAudio(chat, audio, caption: caption, parseMode: parseMode ?? ParseMode.None,
                replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadDocument);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a randomly selected animation (GIF) from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="animations">A non-empty list of animations (file IDs or URLs) to choose from at random.</param>
    /// <param name="caption">Optional caption for the animation.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="hasSpoiler">If true, the animation is sent with a spoiler animation.</param>
    /// <param name="asReply">If true, sends the random animation as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random animation as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="animations" /> is empty.</exception>
    public static IBotActionBuilder SendRandomAnimation(this IBotActionBuilder builder, IReadOnlyList<string> animations,
        string? caption = null, ParseMode? parseMode = null, bool hasSpoiler = false,
        bool asReply = false, bool toReply = false)
    {
        if (animations.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(animations));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var animation = animations[Random.Shared.Next(animations.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendAnimation(chat, animation, caption: caption, parseMode: parseMode ?? ParseMode.None,
                hasSpoiler: hasSpoiler, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadPhoto);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a randomly selected document from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="documents">A non-empty list of documents (file IDs or URLs) to choose from at random.</param>
    /// <param name="caption">Optional caption for the document.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="asReply">If true, sends the random document as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random document as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="documents" /> is empty.</exception>
    public static IBotActionBuilder SendRandomDocument(this IBotActionBuilder builder, IReadOnlyList<string> documents,
        string? caption = null, ParseMode? parseMode = null,
        bool asReply = false, bool toReply = false)
    {
        if (documents.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(documents));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var document = documents[Random.Shared.Next(documents.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendDocument(chat, document, caption: caption, parseMode: parseMode ?? ParseMode.None,
                replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadDocument);

        return builder;
    }

    /// <summary>
    ///     Sets the handler to send a randomly selected voice message from the provided list.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="voices">A non-empty list of voice messages (file IDs or URLs) to choose from at random.</param>
    /// <param name="caption">Optional caption for the voice message.</param>
    /// <param name="parseMode">Optional parse mode for the caption.</param>
    /// <param name="asReply">If true, sends the random voice message as a reply to the incoming message.</param>
    /// <param name="toReply">If true, sends the random voice message as a reply to the message being replied to.</param>
    /// <returns>The same <see cref="BotActionBuilder" /> instance for further chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="voices" /> is empty.</exception>
    public static IBotActionBuilder SendRandomVoice(this IBotActionBuilder builder, IReadOnlyList<string> voices,
        string? caption = null, ParseMode? parseMode = null,
        bool asReply = false, bool toReply = false)
    {
        if (voices.Count == 0) throw new ArgumentException("Collection must not be empty.", nameof(voices));

        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var voice = voices[Random.Shared.Next(voices.Count)];
            var replyParameters = BuildReplyParameters(asReply, toReply, message);

            await client.SendVoice(chat, voice, caption: caption, parseMode: parseMode ?? ParseMode.None,
                replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadVoice);

        return builder;
    }

    #endregion
}
