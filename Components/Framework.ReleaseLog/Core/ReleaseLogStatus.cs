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

	public partial class ReleaseLogStatusDataManager : StandardDataManager
	{
		static readonly string DataStoreKey = "";

		static ReleaseLogStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseLogStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ReleaseLogStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId:
					if (data.ReleaseLogStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId, data.ReleaseLogStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId);
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
			var sql = "EXEC dbo.ReleaseLogStatusSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("ReleaseLogStatus.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		public static List<ReleaseLogStatusDataModel> GetEntityDetails(ReleaseLogStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseLogStatusSearch ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId
				,	ApplicationId			= requestProfile.ApplicationId
				,	ReleaseLogStatusId		= dataQuery.ReleaseLogStatusId
				,	Name					= dataQuery.Name
				,	ReturnAuditInfo			= returnAuditInfo
			};

			List<ReleaseLogStatusDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseLogStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseLogStatusDataModel> GetEntityDetails(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.ReleaseLogStatusSearch " +
		//		" "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//		", " + ToSQLParameter(data, ReleaseLogStatusDataModel.DataColumns.ApplicationId) +
		//		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//		", " + ToSQLParameter(data, ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId) +
		//		", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

		//	var result = new List<ReleaseLogStatusDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new ReleaseLogStatusDataModel();

		//			dataItem.ReleaseLogStatusId = (int)dbReader[ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId];
		//			dataItem.ApplicationId		= (int)dbReader[ReleaseLogStatusDataModel.DataColumns.ApplicationId];
		//			dataItem.Application		= (string)dbReader[ReleaseLogStatusDataModel.DataColumns.Application];

		//			SetStandardInfo(dataItem, dbReader);

		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion GetDetails

		#region Create

		public static void Create(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("ReleaseLogStatus.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ReleaseLogStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseLogStatusDelete ";

			var parameters =	new
			{
					AuditId						= requestProfile.AuditId
				,	ReleaseLogStatusId			= data.ReleaseLogStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Search

		public static DataTable Search(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(ReleaseLogStatusDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleaseLogStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleaseLogStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReleaseLogStatusDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleaseLogStatusChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
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

		static public DataSet DeleteChildren(ReleaseLogStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleaseLogStatusChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId);

			var oDT = new DBDataSet("Delete Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

	}

}
