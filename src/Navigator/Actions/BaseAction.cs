using System;
using Navigator.Context;

namespace Navigator.Actions
{
    /// <summary>
    /// Base action to use for any action.
    /// </summary>
    public abstract class BaseAction : IAction
    {
        /// <inheritdoc />
        public abstract string Type { get; protected set; }

        /// <inheritdoc />
        public virtual ushort Priority { get; protected set; } = Actions.Priority.Default;
        
        /// <summary>
        /// Timestamp of the request on creation.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Default constructor for <see cref="BaseAction"/>
        /// </summary>
        protected BaseAction()
        {
            Timestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public abstract IAction Init(INavigatorContext navigatorContext);

        /// <inheritdoc />
        public abstract bool CanHandle(INavigatorContext ctx);
    }
}