namespace Navigator
{
    public static class Provider
    {
        public static TProvider For<TProvider>() where TProvider : IProvider, new()
        {
            return new();
        }
    }
}