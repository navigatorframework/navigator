namespace Navigator.Context
{
    public class NavigatorContextAccessor : INavigatorContextAccessor
    {
        private readonly INavigatorContextFactory _navigatorContextFactory;

        public NavigatorContextAccessor(INavigatorContextFactory navigatorContextFactory)
        {
            _navigatorContextFactory = navigatorContextFactory;
        }

        public INavigatorContext NavigatorContext { get; }
    }
}