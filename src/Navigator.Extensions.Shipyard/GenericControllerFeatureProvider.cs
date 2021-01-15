using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Navigator.Extensions.Shipyard.Controllers;

namespace Navigator.Extensions.Shipyard
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private Type UserType { get; }
        private Type ChatType { get; }

        public GenericControllerFeatureProvider(Type userType, Type chatType)
        {
            UserType = userType;
            ChatType = chatType;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            // Check to see if there is a "real" controller for this class
            if (!feature.Controllers.Any(t => t.GenericTypeParameters.Any(gp => gp == UserType) && t.GenericTypeParameters.Any(gp => gp == ChatType)))
            {
                // Create a generic controller for this type
                var controllerType = typeof(ConversationController<,>).MakeGenericType(UserType, ChatType).GetTypeInfo();
                feature.Controllers.Add(controllerType);
            }
        }
    }
}