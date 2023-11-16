using Navigator.Context.Builder.Options;

namespace Navigator.Bundled.Extensions.Update;

internal static class NavigatorContextBuilderOptionsExtensions
{
    private const string UpdateKey = "_navigator.context.options.update";

    public static void GetUpdate(this INavigatorContextBuilderOptions contextBuilderOptions, global::Telegram.Bot.Types.Update update)
    {
        contextBuilderOptions.TryRegisterOption(UpdateKey, update);

    }

    public static global::Telegram.Bot.Types.Update? GetUpdateOrDefault(this INavigatorContextBuilderOptions contextBuilderOptions)
    {
        return contextBuilderOptions.RetrieveOption<global::Telegram.Bot.Types.Update>(UpdateKey);
    }
}