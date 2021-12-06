using System;
using Navigator.Context;

namespace Navigator.Actions
{
    /// <summary>
    /// Base action to use for any action.
    /// </summary>
    public abstract class BaseAction : IAction
    {
        /// <summary>
        /// Used to access <see cref="INavigatorContext"/> inside the action.
        /// </summary>
        protected readonly INavigatorContextAccessor NavigatorContextAccessor;

        /// <inheritdoc />
        public virtual ushort Priority { get; protected set; } = Actions.Priority.Default;
        
        /// <summary>
        /// Timestamp of the request on creation.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Default constructor for <see cref="BaseAction"/>
        /// </summary>
        public BaseAction(INavigatorContextAccessor navigatorContextAccessor)
        {
            NavigatorContextAccessor = navigatorContextAccessor;
            Timestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public abstract bool CanHandleCurrentContext();
    }
}