using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Navigator.Configuration;

namespace Navigator.Actions;

internal static class NavigatorOptionsExtensions
{
    #region NavigatorActions
        
    private const string NavigatorActions = "_navigator.options.actions";

    public static void RegisterActions(this NavigatorOptions navigatorOptions, IEnumerable<Type> actions)
    {
        navigatorOptions.TryRegisterOption(NavigatorActions, actions.ToImmutableDictionary(
            type => type.GetActionType(), 
            type => type));
    }

    public static ImmutableDictionary<string,Type> RetrieveActions(this NavigatorOptions navigatorOptions)
    {
        return navigatorOptions.RetrieveOption<ImmutableDictionary<string,Type>>(NavigatorActions) ?? ImmutableDictionary<string, Type>.Empty;
    }

    #endregion

    public static string GetActionType(this Type? type)
    {
        if (type?.CustomAttributes.Any(data => data.AttributeType == typeof(ActionTypeAttribute)) ?? false)
        {
            return type.Name;
        }
        
        if (type?.BaseType is not null && !type.BaseType.IsInterface)
        {
            var action = GetActionType(type.BaseType);

            if (action != string.Empty)
            {
                return action;
            }
        }

        return type?.Name ?? string.Empty;
    }
    
}