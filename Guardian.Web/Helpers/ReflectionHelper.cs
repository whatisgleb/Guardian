using System.Reflection;

namespace Guardian.Web.Helpers
{
    public static class ReflectionHelper
    {
        public static Assembly GetExecutingAssembly()
        {
            return typeof(ReflectionHelper).Assembly;
        }
    }
}
