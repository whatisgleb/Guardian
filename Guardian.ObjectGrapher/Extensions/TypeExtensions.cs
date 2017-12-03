using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.ObjectGrapher.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns whether the specified Type is complex
        /// </summary>
        public static bool IsComplexType(this Type type)
        {
            return !type.IsValueType && !type.IsPrimitive && type != typeof(string);
        }

        /// <summary>
        /// Returns whether the specified Type is a collection
        /// </summary>
        public static bool IsCollectionType(this Type type)
        {
            return type.GetInterface("IEnumerable") != null && type != typeof(string);
        }
    }
}
