using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Navigator.Actions.Attributes;
using Navigator.Configuration;

namespace Navigator.Actions;

public static class NavigatorOptionsExtensions
{
    #region NavigatorActionTypeCollection
        
    private const string NavigatorActionTypeCollection = "_navigator.options.action_type_collection";

    /// <summary>
    /// Pseudo-internal call, don't use it unless you know what you are doing.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <param name="actions"></param>
    public static void RegisterActionsCore(this NavigatorOptions navigatorOptions, IEnumerable<Type> actions)
    {
        var actionsDictionary = actions.GroupBy(type => type.GetActionType())
            .ToImmutableDictionary(types => types.Key, types => types.ToArray());
        navigatorOptions.TryRegisterOption(NavigatorActionTypeCollection, actionsDictionary);
    }

    /// <summary>
    /// Pseudo-internal call, don't use it unless you know what you are doing.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static ImmutableDictionary<string,Type[]> RetrieveActions(this NavigatorOptions navigatorOptions)
    {
        return navigatorOptions.RetrieveOption<ImmutableDictionary<string,Type[]>>(NavigatorActionTypeCollection) ?? ImmutableDictionary<string, Type[]>.Empty;
    }

    #endregion
    
    #region NavigatorActionPriorityCollection
        
    private const string NavigatorActionPriorityCollection = "_navigator.options.action_priority_collection";

    /// <summary>
    /// Pseudo-internal call, don't use it unless you know what you are doing.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static void RegisterPriority(this NavigatorOptions navigatorOptions, IEnumerable<Type> actions)
    {
        navigatorOptions.TryRegisterOption(NavigatorActionPriorityCollection, actions.ToImmutableDictionary(
            type => type.FullName ?? string.Empty, 
            type => type.GetActionPriority()));
    }

    /// <summary>
    /// Pseudo-internal call, don't use it unless you know what you are doing.
    /// </summary>
    /// <param name="navigatorOptions"></param>
    /// <returns></returns>
    public static ImmutableDictionary<string, ushort> RetrievePriorities(this NavigatorOptions navigatorOptions)
    {
        return navigatorOptions.RetrieveOption<ImmutableDictionary<string, ushort>>(NavigatorActionPriorityCollection) ?? ImmutableDictionary<string, ushort>.Empty;
    }

    #endregion
    
    internal static string GetActionType(this Type? type)
    {
        ActionTypeAttribute? actionTypeAttribute = default;
        
        if (type?.CustomAttributes.Any(data => data.AttributeType == typeof(ActionTypeAttribute)) ?? false)
        {
            actionTypeAttribute = Attribute.GetCustomAttribute(type, typeof(ActionTypeAttribute)) as ActionTypeAttribute;

            return actionTypeAttribute?.ActionType ?? string.Empty;
        }
        
        if (type?.BaseType is not null && !type.BaseType.IsInterface)
        {
            var action = GetActionType(type.BaseType);

            if (action != string.Empty)
            {
                return action;
            }
        }

        if (type is not null)
        {
            actionTypeAttribute = Attribute.GetCustomAttribute(type, typeof(ActionTypeAttribute)) as ActionTypeAttribute;
        }

        return actionTypeAttribute?.ActionType ?? string.Empty;
    }
    internal static ushort GetActionPriority(this Type? type)
    {
        if (type?.CustomAttributes.Any(data => data.AttributeType == typeof(ActionPriorityAttribute)) ?? false)
        {
            var priorityAttribute = (ActionPriorityAttribute) Attribute.GetCustomAttribute(type, typeof(ActionPriorityAttribute))!;

            return priorityAttribute.Priority;
        }
        
        if (type?.BaseType is not null && !type.BaseType.IsInterface)
        {
            var priority = GetActionPriority(type.BaseType);

            return priority;
        }

        return Priority.Default;
    }
}