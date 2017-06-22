using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class FunctionalityImageXFunctionalityImageAttributeDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";
		static readonly int ApplicationId;

		static FunctionalityImageXFunctionalityImageAttributeDataManager()
		{
			ApplicationId = SetupConfiguration.ApplicationId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityImageXFunctionalityImageAttribute");
		}

		#region GetList

		public static DataTable GetList(int auditId)
		{
			var sql = "EXEC dbo.FunctionalityImageXFunctionalityImageAttributeList" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("FunctionalityImageXFunctionalityImageAttribute.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			var sql = "EXEC dbo.FunctionalityImageXFunctionalityImageAttributeDetails " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("FunctionalityImageXFunctionalityImageAttribute.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		public static List<FunctionalityImageXFunctionalityImageAttributeDataModel> GetEntityDetails(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
        {
            var sql = "EXEC dbo.FunctionalityImageXFunctionalityImageAttributeDetails " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
					  ", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId);

			var result = new List<FunctionalityImageXFunctionalityImageAttributeDataModel>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
					var dataItem = new FunctionalityImageXFunctionalityImageAttributeDataModel();

					dataItem.FunctionalityImageXFunctionalityImageAttributeId = (int)dbReader[FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId];
					dataItem.FunctionalityImageId = (int)dbReader[FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId];
					dataItem.FunctionalityImage = dbReader[FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage].ToString();
					dataItem.FunctionalityImageAttributeId = (int)dbReader[FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId];
					dataItem.FunctionalityImageAttribute = dbReader[FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId].ToString();

                    SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }

            }
            return result;
        }
		#endregion

		#region Create

		public static void Create(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			var sql = Save(data, auditId, "Create");
			Framework.Components.DataAccess.DBDML.RunSQL("FunctionalityImageXFunctionalityImageAttribute.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			var sql = Save(data, auditId, "Update");
			Framework.Components.DataAccess.DBDML.RunSQL("FunctionalityImageXFunctionalityImageAttribute.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			var sql = "EXEC dbo.FunctionalityImageXFunctionalityImageAttributeDelete " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data,FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId);

			Framework.Components.DataAccess.DBDML.RunSQL("FunctionalityImageXFunctionalityImageAttribute.Delete", sql, DataStoreKey);
		}

		#endregion

		#region Search

		public static string ToSQLParameter(FunctionalityImageXFunctionalityImageAttributeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId:
					if (data.FunctionalityImageXFunctionalityImageAttributeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId, data.FunctionalityImageXFunctionalityImageAttributeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId);

					}
					break;

				case FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId:
					if (data.FunctionalityImageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId, data.FunctionalityImageId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId);

					}
					break;

				case FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId:
					if (data.FunctionalityImageAttributeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId, data.FunctionalityImageAttributeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);

					}
					break;

				case FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage:
					if (!string.IsNullOrEmpty(data.FunctionalityImage))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage, data.FunctionalityImage.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage);

					}
					break;
				case FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttribute:
					if (!string.IsNullOrEmpty(data.FunctionalityImageAttribute))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttribute, data.FunctionalityImageAttribute.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttribute);

					}
					break;

			}
			return returnValue;
		}


		public static DataTable Search(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			// formulate SQL
			var sql = "EXEC dbo.FunctionalityImageXFunctionalityImageAttributeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId) +
				", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId) +
				", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("FunctionalityImageXFunctionalityImageAttribute.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityImageXFunctionalityImageAttributeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityImageXFunctionalityImageAttributeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data,FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId) +
						", " + ToSQLParameter(data,FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId) +
						", " + ToSQLParameter(data,FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			var sql = "EXEC dbo.FunctionalityImageXFunctionalityImageAttributeDoesExist " +
			" " +  ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
			", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
			", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId) +
			", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId) +
			", " + ToSQLParameter(data, FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("FunctionalityImageXFunctionalityImageAttribute.DoesExist", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			var sql = "EXEC dbo.FunctionalityImageXFunctionalityImageAttributeChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
							", " + ToSQLParameter(data,FunctionalityImageXFunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageXFunctionalityImageAttributeId);

			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityImageXFunctionalityImageAttributeDataModel data, int auditId)
		{
			var isDeletable = true;
			var ds = GetChildren(data, auditId);
			if (ds != null && ds.Tables.Count > 0)
			{
				foreach (DataTable dt in ds.Tables)
				{
					if (dt.Rows.Count > 0)
					{
						isDeletable = false;
						break;
					}
				}
			}
			return isDeletable;
		}

		#endregion

	}
}
