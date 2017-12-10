using System.Reflection;

namespace Guardian.Web.Helpers
{
    internal static class ReflectionHelper
    {
        public static Assembly GetExecutingAssembly()
        {
            return typeof(ReflectionHelper).Assembly;
        }
    }
}
