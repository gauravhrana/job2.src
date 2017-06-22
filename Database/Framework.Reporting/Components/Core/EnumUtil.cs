using System;
using System.Collections.Generic;

namespace Library.CommonServices.Utils
{
    public static class EnumUtil<T>
    {
        public static T Parse(string stringValue, T defaultValue)
        {
            return Parse(stringValue, defaultValue, false);
        }

        public static T Parse(string stringValue, T defaultValue, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(stringValue) == false)
                try
                {
                    return Parse(stringValue, ignoreCase);
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            return defaultValue;
        }

        public static T Parse(string stringValue)
        {
            return (T)Enum.Parse(typeof(T), stringValue);
        }

        public static T Parse(string stringValue, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), stringValue, ignoreCase);
        }

        public static IEnumerable<T> EnumToList()
        {
            Type enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            var enumValList = new List<T>(enumValArray.Length);
            foreach (int val in enumValArray)
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));

            return enumValList;
        }

        public static List<string> EnumToListStrings()
        {
            var enumslist = EnumToList();

            var output = new List<string>();

            foreach (var enumerable in enumslist)
            {
                output.Add(enumerable.ToString());
            }
            return output;
        }
    }
}