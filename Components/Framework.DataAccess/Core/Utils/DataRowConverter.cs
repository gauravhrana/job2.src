//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;
//using Framework.CommonServices.BusinessDomain.Utils;

//namespace Framework.CommonServices.Utils
//{
//    public enum DataConverterColumnOption
//    {
//        DoNotIgnoreUnmatchedColumns,
//        IgnoreUnmatchedColumns
//    }

//    public enum DataConverterNullOption
//    {
//        DoNotReplaceNullWithDefault,
//        ReplaceNullWithDefault
//    }

//    public static class DataRowConverterConstants
//    {
//        public const string DbNullErrorPrefix = "[DBNull Error]";
//        public const string ReadOnlyErrorPrefix = "[Readonly Property Error]";
//        public const string TypeConversionErrorPrefix = "[Type Conversion Error]";
//        public const string UnmatchedColumnPrefix = "[Unmatched Column Error]";
//    }

//    public class DataRowConverter<T> where T : new()
//    {
//        private static readonly PropertyInfo[] MyProperties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//        private static readonly List<PropertyInfo> PropertyList = new List<PropertyInfo>(MyProperties);

//        public DataConverterColumnOption ConverterColumnOption { get; set; }
//        public DataConverterNullOption ConverterNullOption { get; set; }
//        public DataRow InputDataRow { get; set; }
//        public List<string> IgnoreColumnList { get; private set; }

//        public DataRowConverter()
//        {
//            IgnoreColumnList = new List<string>();
//        }

//        private bool IgnoreUnmatchedColumns
//        {
//            get { return ConverterColumnOption == DataConverterColumnOption.IgnoreUnmatchedColumns; }
//        }

//        private bool ReplaceNullWithDefaultForValueTypes
//        {
//            get { return ConverterNullOption == DataConverterNullOption.ReplaceNullWithDefault; }
//        }

//        private static PropertyInfo GetProperty(DataColumn column)
//        {
//            if (column == null)
//                throw new ArgumentNullException("column");

//            if (string.IsNullOrEmpty(column.ColumnName))
//                throw new ArgumentException("ColumnName can not be null.", "column");

//            return PropertyList.Where(
//                            propertyInfo => column.ColumnName.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)
//                            ).FirstOrDefault();
//        }

//        public void UpdateObject(T input)
//        {
//            if (InputDataRow == null)
//                throw new InvalidOperationException("You must set InputDataRow Property.");

//            VerifyMapping();

//            foreach (DataColumn column in InputDataRow.Table.Columns)
//            {
//                if(IgnoreColumnList.Contains(column.ColumnName))
//                    continue;

//                var property = GetProperty(column);

//                if (property == null && IgnoreUnmatchedColumns)
//                {
//                    if (IgnoreUnmatchedColumns)
//                        continue;

//                    throw new InvalidOperationException(
//                        string.Format("{2}: Could not find Property with name [{0}] in Type [{1}]",
//                                      column.ColumnName, typeof (T).Name,
//                                      DataRowConverterConstants.UnmatchedColumnPrefix));
//                }

//                var targetValue = GetTargetValue(column, property);

//                if (targetValue == null && !CanAssignNullValueToProperty(property))
//                {
//                    throw GetException(
//                        string.Format("{1}: Can not assign null value to Property [{0}].", property.Name,
//                                      DataRowConverterConstants.DbNullErrorPrefix),
//                        column, targetValue, property, null);
//                }


//                try
//                {
//                    property.SetValue(input, targetValue, null);
//                }
//                catch (Exception e)
//                {
//                    throw GetException(
//                        string.Format("Could not Set value of Property [{0}]. Error: {1}.", property.Name, e.Message),
//                        column, targetValue, property, e);
//                }
//            }
//        }

//        private void VerifyMapping()
//        {
//            if (!IgnoreUnmatchedColumns)
//                VerifyAllPropertiesArePresent();

//            VerifyPropertiesAreNotReadOnly();
//        }

//        private void VerifyPropertiesAreNotReadOnly()
//        {
//            var readOnlyPropertyList = (from DataColumn column in InputDataRow.Table.Columns
//                                        select GetProperty(column)
//                                        into property where property != null || !IgnoreUnmatchedColumns
//                                            where property != null && !property.CanWrite select property.Name).ToList();

//            readOnlyPropertyList.RemoveAll(propertyName => IgnoreColumnList.Contains(propertyName));

//            if (readOnlyPropertyList.Count > 0)
//            {
//                throw new InvalidOperationException(
//                    string.Format(
//                        "{0}: Following properties are ReadOnly and can not be updated from the DataRow. Column List {1}",
//                        DataRowConverterConstants.ReadOnlyErrorPrefix, string.Join(", ", readOnlyPropertyList.ToArray())));
//            }
//        }

//        private void VerifyAllPropertiesArePresent()
//        {
//            var missingPropertyList = (from DataColumn column in InputDataRow.Table.Columns
//                                       let property = GetProperty(column)
//                                       where property == null
//                                       select column.ColumnName).ToList();

//            missingPropertyList.RemoveAll(propertyName => IgnoreColumnList.Contains(propertyName));

//            if (missingPropertyList.Count > 0)
//            {
//                throw new InvalidOperationException(
//                    string.Format(
//                        "{2}: Following columns are present in DataRow but corresponding properties are either missing or not public in Type {1} [DataRowConverter can not set Protected or Private properties]. Column List [{0}]",
//                        string.Join(", ", missingPropertyList.ToArray()), typeof (T).Name,
//                        DataRowConverterConstants.UnmatchedColumnPrefix));
//            }
//        }

//        public T GetObject()
//        {
//            var output = new T();
//            UpdateObject(output);
//            return output;
//        }

//        private static bool CanAssignNullValueToProperty(PropertyInfo property)
//        {
//            return DataHelper.IsNullableType(property.PropertyType) || property.PropertyType.IsClass;
//        }

//        private Exception GetException(string message, DataColumn column, object columnValue,
//                                       PropertyInfo property, Exception e)
//        {
//            var info =
//                string.Format(
//                    "[Info: (Column Name: {0} Value: {1} Type: {2}) (Property Name: {3} Type: {4} ReadOnly?: {5}",
//                    column.ColumnName, columnValue, InputDataRow[column.ColumnName].GetType().Name, property.Name,
//                    property.PropertyType.Name, !property.CanWrite);

//            message = message + info;

//            return new InvalidOperationException(message, e);
//        }

//        private object GetTargetValue(DataColumn column, PropertyInfo property)
//        {
//            var columnValue = InputDataRow[column.ColumnName];

//            if (Convert.IsDBNull(columnValue))
//                return FindNullTargetValue(property);

//            try
//            {
//                return DataHelper.ConvertTo(property.PropertyType, columnValue);
//            }
//            catch (Exception e)
//            {
//                throw GetException(
//                    string.Format("{3}: Can not convert value {0} to type {1} for property {2}", columnValue,
//                                  property.PropertyType.Name, property.Name,
//                                  DataRowConverterConstants.TypeConversionErrorPrefix), column,
//                    columnValue, property, e);
//            }
//        }

//        private object FindNullTargetValue(PropertyInfo property)
//        {
//            //There are two options to convert DB NULL to our target property value.

//            //If our target is NON value type, DB NULL maps to C# null.
//            if (! property.PropertyType.IsValueType)
//                return null;

//            //For value types, it's a litte more trickier. 
//            //If user has specified that DB NULL should be replaced with Value type default,
//            //we will get the default value for the value type, otherwise we will return NULL.
//            //If user hasn't specified option, returning NULL will lead to an exception
//            //if we encounter NULL values in DB.
//            return ReplaceNullWithDefaultForValueTypes
//                       ? TypeHelper.GetDefaultValue(property.PropertyType)
//                       : null;
//        }
//    }
//}