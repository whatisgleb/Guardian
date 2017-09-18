using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Guardian.Web.Attributes;
using Guardian.Web.Controllers;
using Guardian.Web.Helpers;

namespace Guardian.Web.Routing
{
    internal static class GuardianRouter
    {
        public static RouteHandler GetRouteHandler(string path)
        {
            Assembly assembly = ReflectionHelper.GetExecutingAssembly();

            // Methods with RouteAttribute in the current assembly (candidates for routing to)
            IEnumerable<MemberInfo> memberInfos = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(GuardianBaseController)))
                .SelectMany(t => t.GetMembers())
                .Where(m => m.IsDefined(typeof(RouteAttribute), false));

            foreach (MemberInfo memberInfo in memberInfos)
            {
                // If the controller has a RoutePrefix attribute, we need to build the pattern matching prefix
                Type controllerType = memberInfo.ReflectedType;

                RoutePrefixAttribute routePrefixAttribute =
                    (RoutePrefixAttribute)Attribute.GetCustomAttribute(controllerType, typeof(RoutePrefixAttribute));

                string prefix = routePrefixAttribute == null
                    ? string.Empty
                    : $"/{routePrefixAttribute.RoutePrefix}";

                // Build pattern to match controller/method against current path
                RouteAttribute routeAttribute =
                    (RouteAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(RouteAttribute));

                string pattern = $@"^{prefix}/{routeAttribute.Route}";

                Match match = Regex.Match(
                    path,
                    pattern,
                    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (match.Success)
                {
                    return new RouteHandler(controllerType, memberInfo);
                }
            }

            return null;
        }
    }
}
