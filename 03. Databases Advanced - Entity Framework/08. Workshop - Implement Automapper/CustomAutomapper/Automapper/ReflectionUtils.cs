namespace Automapper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReflectionUtils
    {
        private static readonly HashSet<string> Types = new HashSet<string>
        {
            "System.String", "System.Int32", "System.Decimal",
            "System.Double", "System.Guid", "System.Single", "System.Int64",
            "System.UInt64", "System.Int16", "System.DateTime", "System.String[]",
            "System.Int32[]", "System.Decimal[]", "System.Double[]",
            "System.Guid[]", "System.Single[]", "System.DateTime[]"
        };

        public static bool IsPrimitive(Type sourceType)
        {
            bool result = Types.Contains(sourceType.FullName)
                || sourceType.IsPrimitive
                || ReflectionUtils.IsNullable(sourceType) && IsPrimitive(Nullable.GetUnderlyingType(sourceType))
                || sourceType.IsEnum;

            return result;
        }

        public static bool IsGenericCollection(Type type)
        {
            bool result = (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>)
                                                  || type.GetGenericTypeDefinition() == typeof(ICollection<>)
                                                  || type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                                  || type.GetGenericTypeDefinition() == typeof(IList<>))) ||
                                                  typeof(IList<>).IsAssignableFrom(type) ||
                                                  typeof(HashSet<>).IsAssignableFrom(type);

            return result;
        }

        public static bool IsNonGenericCollection(Type type)
        {
            bool result = type.IsArray || type == typeof(ArrayList) ||
                          typeof(IList).IsAssignableFrom(type);

            return result;
        }

        private static bool IsNullable(Type sourceType)
        {
            bool result = sourceType == null;

            return result;
        }
    }
}
