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
	public partial class ReleaseIssueTypeDataManager : StandardDataManager
	{
		static readonly string DataStoreKey = "";

		static ReleaseIssueTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseIssueType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ReleaseIssueTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId:
					if (data.ReleaseIssueTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId, data.ReleaseIssueTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId);
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

        public static List<ReleaseIssueTypeDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ReleaseIssueTypeDataModel.Empty, requestProfile, 0);
		}

		//public static List<ReleaseIssueTypeDataModel> GetReleaseIssueTypeList(RequestProfile requestProfile)
		//{
		//	var list = GetEntityDetails(ReleaseIssueTypeDataModel.Empty, requestProfile);

		//	//var result = list.Select(item => new ReleaseIssueTypeDataModel()
		//	//{
		//	//		Name				= item.Name
		//	//	,	ApplicationId		= item.ApplicationId
		//	//	,	ReleaseIssueTypeId	= item.ReleaseIssueTypeId
		//	//}).ToList();


		//	return list;
		//}


		#endregion

		#region GetDetails

        public static ReleaseIssueTypeDataModel GetDetails(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ReleaseIssueTypeDataModel> GetEntityDetails(ReleaseIssueTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseIssueTypeSearch ";

			var parameters =
			new
			{
				    AuditId                     = requestProfile.AuditId
				,   ReleaseIssueTypeId          = dataQuery.ReleaseIssueTypeId
				,   ApplicationMode             = requestProfile.ApplicationModeId
				,   ApplicationId               = dataQuery.ApplicationId
				,   Name                        = dataQuery.Name
				,   ReturnAuditInfo             = returnAuditInfo
			};

			List<ReleaseIssueTypeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseIssueTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseIssueTypeDataModel> GetEntityDetails(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.ReleaseIssueTypeSearch " +
		//		" "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//		", " + ToSQLParameter(data, ReleaseIssueTypeDataModel.DataColumns.ApplicationId) +
		//		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//		", " + ToSQLParameter(data, ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId) +
		//		", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

		//	var result = new List<ReleaseIssueTypeDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new ReleaseIssueTypeDataModel();

		//			dataItem.ReleaseIssueTypeId = (int)dbReader[ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId];
		//			dataItem.ApplicationId		= (int)dbReader[ReleaseIssueTypeDataModel.DataColumns.ApplicationId];
		//			dataItem.Application		= (string)dbReader[ReleaseIssueTypeDataModel.DataColumns.Application];

		//			SetStandardInfo(dataItem, dbReader);

		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion GetDetails

		#region Create

		public static void Create(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("ReleaseIssueType.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ReleaseIssueType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseIssueTypeDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ReleaseIssueTypeId = data.ReleaseIssueTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(ReleaseIssueTypeDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleaseIssueTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleaseIssueTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest           = new ReleaseIssueTypeDataModel();
			doesExistRequest.Name          = data.Name;
            doesExistRequest.ApplicationId = data.ApplicationId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleaseIssueTypeChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
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

		static public DataSet DeleteChildren(ReleaseIssueTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleaseIssueTypeChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId);

			var oDT = new DBDataSet("Delete Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
		{
			// get all records for old Application Id
			var sql = "EXEC dbo.ReleaseIssueTypeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

			var oDT = new DBDataTable("ReleaseIssueType.Search", sql, DataStoreKey);

			// formaulate a new request Profile which will have new Applicationid
			var newRequestProfile = new RequestProfile();
			newRequestProfile.ApplicationId = newApplicationId;
			newRequestProfile.AuditId = requestProfile.AuditId;

			foreach (DataRow dr in oDT.DBTable.Rows)
			{
				var data = new ReleaseIssueTypeDataModel();
				data.ApplicationId = newApplicationId;
				data.Name = dr[StandardDataModel.StandardDataColumns.Name].ToString();

				// check for existing record in new Application Id
				if(!DoesExist(data, newRequestProfile))
				{
					data.Description = dr[StandardDataModel.StandardDataColumns.Description].ToString();
					data.SortOrder = Convert.ToInt32(dr[StandardDataModel.StandardDataColumns.SortOrder]);

					//create in new application id
					Create(data, newRequestProfile);

				}

			}
		}

	}
}
