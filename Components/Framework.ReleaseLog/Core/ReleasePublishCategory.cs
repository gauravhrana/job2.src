using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.ReleaseLog;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog
{

	public partial class ReleasePublishCategoryDataManager : StandardDataManager
	{
		static readonly string DataStoreKey = "";

		static ReleasePublishCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleasePublishCategory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ReleasePublishCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId:
					if (data.ReleasePublishCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId, data.ReleasePublishCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#endregion

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleasePublishCategorySearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("ReleasePublishCategory.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		public static List<ReleasePublishCategoryDataModel> GetReleasePublishCategoryList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleasePublishCategorySearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<ReleasePublishCategoryDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new ReleasePublishCategoryDataModel();

					dataItem.ReleasePublishCategoryId = (int)dbReader[ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId];

					SetStandardInfo(dataItem, dbReader);

					result.Add(dataItem);
				}

			}

			return result;
		}


		#endregion

		#region GetDetails

		public static DataTable GetDetails(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		public static List<ReleasePublishCategoryDataModel> GetEntityDetails(ReleasePublishCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleasePublishCategorySearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ReleasePublishCategoryId = dataQuery.ReleasePublishCategoryId
				,
				ApplicationId = requestProfile.ApplicationId
				,
				Name = dataQuery.Name
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<ReleasePublishCategoryDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleasePublishCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleasePublishCategoryDataModel> GetEntityDetails(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.ReleasePublishCategorySearch " +
		//		" "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
		//		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//		", " + ToSQLParameter(data, ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId) +
		//		", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

		//	var result = new List<ReleasePublishCategoryDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new ReleasePublishCategoryDataModel();

		//			dataItem.ReleasePublishCategoryId = (int)dbReader[ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId];

		//			SetStandardInfo(dataItem, dbReader);

		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion GetDetails

		#region Create

		public static void Create(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("ReleasePublishCategory.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ReleasePublishCategory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleasePublishCategoryDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
					,
				ReleasePublishCategoryId = data.ReleasePublishCategoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(ReleasePublishCategoryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleasePublishCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleasePublishCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReleasePublishCategoryDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleasePublishCategoryChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;
			var ds = GetChildren(data, requestProfile);
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

		#region DeleteChildren

		static public DataSet DeleteChildren(ReleasePublishCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleasePublishCategoryChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId);

			var oDT = new DBDataSet("Delete Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion



	}

}
