//using System;
//using System.Data;

//namespace Framework.CommonServices.Utils
//{
//    public static class DataRowExtensions
//    {
//        public static T Get<T>(this DataRow inputRow) where T : new()
//        {
//            return Get<T>(inputRow, DataConverterColumnOption.DoNotIgnoreUnmatchedColumns, DataConverterNullOption.DoNotReplaceNullWithDefault);
//        }

//        public static T Get<T>(this DataRow inputRow, DataConverterColumnOption converterColumnOption, DataConverterNullOption converterNullOption) where T : new()
//        {
//            var converter = new DataRowConverter<T> {InputDataRow = inputRow, ConverterColumnOption = converterColumnOption, ConverterNullOption =  converterNullOption};
//            return converter.GetObject();
//        }

//        public static void Update<T>(this DataRow inputRow, T targetObject) where T : class, new()
//        {
//            Update(inputRow, targetObject, DataConverterColumnOption.DoNotIgnoreUnmatchedColumns, DataConverterNullOption.DoNotReplaceNullWithDefault);
//        }
        
//        public static void Update<T>(this DataRow inputRow, T targetObject, DataConverterColumnOption converterColumnOption, DataConverterNullOption converterNullOption) where T : class, new()
//        {
//            if (targetObject == null)
//                throw new ArgumentNullException("targetObject");
	
//            var converter = new DataRowConverter<T> {InputDataRow = inputRow, ConverterColumnOption = converterColumnOption, ConverterNullOption =  converterNullOption};
//            converter.UpdateObject(targetObject);
//        }
//    }
//}