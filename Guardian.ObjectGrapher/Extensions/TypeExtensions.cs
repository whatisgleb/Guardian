using System;

namespace Guardian.ObjectGrapher.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns whether the specified Type is complex
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsComplexType(this Type type)
        {
            return !type.IsValueType && !type.IsPrimitive && type != typeof(string);
        }

        /// <summary>
        /// Returns whether the specified Type is a collection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCollectionType(this Type type)
        {
            return type.GetInterface("IEnumerable") != null && type != typeof(string);
        }
    }
}
