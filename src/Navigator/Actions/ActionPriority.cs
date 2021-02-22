namespace Navigator.Actions
{
    public static class Action
    {
        public static class Priority
        {
            public static ushort Low = 15000;
            public static ushort Default = 10000;
            public static ushort High = 5000;
        }

        public static class Type
        {
            public static string For<TProvider>(string name) where TProvider : IProvider
            {
                return $"_navigator.actions.{typeof(TProvider).Name}.{name}";
            }
        }
    }
}