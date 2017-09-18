using System;
using System.Reflection;
using System.Threading.Tasks;
using Guardian.Web.Owin;

namespace Guardian.Web.Routing
{
    public class RouteHandler
    {
        private readonly Type _controllerType;
        private readonly MemberInfo _methodInfo;

        public RouteHandler(Type controllerType, MemberInfo methodInfo)
        {
            _controllerType = controllerType;
            _methodInfo = methodInfo;
        }

        public Task Execute(GuardianOwinContext context)
        {
            object controllerInstance = Activator.CreateInstance(
                _controllerType, 
                new object[] {context});

            return (Task)_controllerType.InvokeMember(
                _methodInfo.Name,
                BindingFlags.InvokeMethod,
                null,
                controllerInstance,
                new object[] {}
            );
        }
    }
}
