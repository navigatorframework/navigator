using Navigator.Abstractions.Actions;
using Navigator.Actions;
using Telegram.Bot.Types;

namespace Navigator.Strategy.Classifier;

/// <summary>
///     Defines the contract for classifying the <see cref="Update" /> from the Telegram Bot API.
/// </summary>
public interface IUpdateClassifier
{
    /// <summary>
    ///     Processes an incoming <see cref="Update" /> and returns its category.
    /// </summary>
    /// <param name="update">The <see cref="Update" /> to process.</param>
    /// <returns>An instance of <see cref="UpdateCategory" /></returns>
    Task<UpdateCategory> Process(Update update);
}