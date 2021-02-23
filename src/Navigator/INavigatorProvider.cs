namespace Navigator
{
    /// <summary>
    /// Represents a provider for Navigator.
    /// </summary>
    public interface INavigatorProvider
    {
        /// <summary>
        /// Gets the specific client for this provider. 
        /// </summary>
        /// <returns>An implementation of <see cref="INavigatorClient"/></returns>
        INavigatorClient GetClient(); 
    }
}