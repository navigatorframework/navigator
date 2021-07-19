namespace Navigator.Actions
{
    public static class ActionsHelper
    {
        public static class Type
        {
            public static string For<TProvider>(string name) where TProvider : INavigatorProvider
            {
                return $"_navigator.actions.{typeof(TProvider).Name}.{name}";
            }
        }
    }
}