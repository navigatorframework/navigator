using System;
using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    /// <inheritdoc />
    public abstract class Action : IAction
    {
        /// <inheritdoc />
        public virtual int Order => 1000;

        /// <inheritdoc />
        public abstract string Type { get; }

        /// <inheritdoc />
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public abstract IAction Init(INavigatorContext ctx);

        /// <inheritdoc />
        public abstract bool CanHandle(INavigatorContext ctx);

    }
}