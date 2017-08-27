using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Guardian.Data.Tests.EntityFramework
{
    internal class EntityComparer<T> : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Instance);

            return propertyInfos.All(pi => pi.GetValue(x) == pi.GetValue(y)) ? 0 : -1;
        }
    }
}
