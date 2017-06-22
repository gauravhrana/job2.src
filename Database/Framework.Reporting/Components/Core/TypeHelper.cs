using System;
using System.Collections.Generic;

namespace Library.CommonServices.Utils
{
    public static class TypeHelper
    {
        private static readonly Dictionary<Type, Object> Cache = new Dictionary<Type, object>();
        private static readonly object CacheLock = new object();

        public static object GetDefaultValue(Type type)
        {
            if (!type.IsValueType)
                return null;

            object returnValue;

            if (Cache.TryGetValue(type, out returnValue))
                return returnValue;

            returnValue = Activator.CreateInstance(type);

            lock (CacheLock)
            {
                Cache[type] = returnValue;
            }

            return returnValue;
        }
    }
}