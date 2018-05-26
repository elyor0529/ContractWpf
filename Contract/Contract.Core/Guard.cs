using System;
using Contract.Core.Helpers;

namespace Contract.Core
{
    public static class Guard
    {
        public static void NotNull(object obj, string argName)
        {
            if (obj == null)
                throw ErrorHelper.ArgNull(argName);
        }

        public static void PropertyNotNull(object obj, string propertyPath)
        {
            if (obj == null)
                throw ErrorHelper.ArgNull(string.Format("Property path {0} cannot be null", propertyPath));
        }

        public static void NotNullOrEmpty(string value, string argName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw ErrorHelper.ArgNull(argName);
        }

        public static void PropertyNotNullOrEmpty(string value, string propertyPath)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw ErrorHelper.ArgNull(string.Format("Property path {0} cannot be null or empty", propertyPath));
        }

        public static void IsTrue<T>(bool value) where T : Exception, new()
        {
            if (!value)
                throw new T();
        }

        public static void IsTrue<T>(bool value, T error) where T : Exception
        {
            if (!value)
                throw error;
        }
    }
}