using System;
using System.ComponentModel;

namespace Library.CommonServices.Utils
{
    public static class EnumHelper
    {
        public static object Parse(Type enumType, string enumValue)
        {
            if (string.IsNullOrEmpty(enumValue))
                throw new ArgumentNullException("enumValue");

            if (enumType == null)
                throw new ArgumentNullException("enumType");

            if (!enumType.IsEnum)
                throw new ArgumentException("enumType argument must be an Enum type.");

            try
            {
                var enumOutput = Enum.Parse(enumType, enumValue, true);

                if (Enum.IsDefined(enumType, enumOutput))
                    return enumOutput;

                throw new ArgumentException(string.Format("Enum value {0} could not be converted to {1}", enumValue, enumType.Name));
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is OverflowException)
                {
                    return ParseWithDescription(enumType, enumValue);
                }

                throw;
            }
        }

        private static object ParseWithDescription(Type enumType, string enumValue)
        {
            foreach (var value in Enum.GetValues(enumType))
            {
                if (string.Compare(enumValue, GetDescription(value.ToString(), enumType), StringComparison.InvariantCultureIgnoreCase) == 0)
                    return value;
            }

            throw new ArgumentException(string.Format("Enum value {0} could not be converted to {1}", enumValue, enumType.Name));
        }

        private static string GetDescription(string field, Type enumType)
        {
            var fieldInfo = enumType.GetField(field);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : null;
        }

        private static string GetDescription(Type enumType, string field)
        {
            var fieldInfo = enumType.GetField(field);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : null;
        }

        public static string GetDescription<T>(T field) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("This function is meant to be used with Enum types only.");

            return GetDescription(type, field.ToString());
        }

    }

}