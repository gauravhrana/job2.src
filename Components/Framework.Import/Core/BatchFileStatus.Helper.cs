//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Framework.Components.DataAccess;
//using DataModel.Framework.DataAccess;

//namespace Framework.Components.Import
//{
//	public partial class BatchFileStatus
//	{
//		public class DataColumns : DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns
//		{
//			public const string BatchFileStatusId = "BatchFileStatusId";
//			public const string Name = "Name";
//			public const string Description = "Description";          
//			public const string SortOrder = "SortOrder";
//		}

//		public class Data : DataModel.Framework.DataAccess.BaseDataModel
//		{
//			public int? BatchFileStatusId { get; set; }
//			public string Name { get; set; }
//			public string Description { get; set; }            
//			public int? SortOrder { get; set; }

//			public string ToSQLParameter(string dataColumnName)
//			{
//				var returnValue = "NULL";
//				switch (dataColumnName)
//				{
//					case DataColumns.BatchFileStatusId:
//						if (BatchFileStatusId != null)
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BatchFileStatusId, BatchFileStatusId);

//						}
//						else
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileStatusId);

//						}
//						break;


//					case DataColumns.Name:
//						if (!string.IsNullOrEmpty(Name))
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Name, Name);

//						}
//						else
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Name);

//						}
//						break;

//					case DataColumns.Description:
//						if (!string.IsNullOrEmpty(Description))
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Description, Description);

//						}
//						else
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Description);

//						}
//						break;
                   

//					case DataColumns.SortOrder:
//						if (SortOrder != null)
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.SortOrder, SortOrder);

//						}
//						else
//						{
//							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.SortOrder);

//						}
//						break;

//				}
//				return returnValue;
//			}

//		}

//	}
//}
