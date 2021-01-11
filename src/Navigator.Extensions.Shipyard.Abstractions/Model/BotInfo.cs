namespace Navigator.Extensions.Shipyard.Abstractions.Model
{
    /// <summary>
    /// Bot information.
    /// </summary>
    public class BotInfo
    {
        /// <summary>
        /// Bot id.
        /// </summary>
        public long Id { get; init; }
        
        /// <summary>
        /// Bot username.
        /// </summary>
        public string Username { get; init; }
        
        /// <summary>
        /// Bot name.
        /// </summary>
        public string Name { get; init; }
        
        /// <summary>
        /// Bot permissions.
        /// </summary>
        public BotPermissions Permissions { get; init; }

        /// <summary>
        /// Bot permissions.
        /// </summary>
        public class BotPermissions
        {
            /// <summary>
            /// CanJoinGroups
            /// </summary>
            public bool CanJoinGroups { get; init; }
            
            /// <summary>
            /// CanReadAllGroupMessages
            /// </summary>
            public bool CanReadAllGroupMessages { get; init; }
            
            /// <summary>
            /// SupportsInlineQueries
            /// </summary>
            public bool SupportsInlineQueries { get; init; }
        }
    }
}