namespace Navigator.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        string Id { get; init; }
        
        /// <summary>
        /// Username of the user, if any.
        /// </summary>
        string? Username { get; init; }
    }
}