using System;
using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class DocumentAction : BaseAction
    {
        public override string Type { get; protected set; } = typeof(DocumentAction).FullName!;

        /// <inheritdoc />
        public DocumentAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            //TODO
        }
    }
}