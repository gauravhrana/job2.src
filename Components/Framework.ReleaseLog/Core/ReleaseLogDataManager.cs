using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.ReleaseLog;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog
{
	public partial class ReleaseLogDataManager : StandardDataManager
	{

		static readonly string DataStoreKey = "";

		static ReleaseLogDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseLog");
		}

		public static string ToSQLParameter(ReleaseLogDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{
				case ReleaseLogDataModel.DataColumns.ReleaseLogId:
					if (data.ReleaseLogId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDataModel.DataColumns.ReleaseLogId, data.ReleaseLogId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDataModel.DataColumns.ReleaseLogId);
					}
					break;

				case BaseDataModel.BaseDataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.ApplicationId);
					}
					break;

				case ReleaseLogDataModel.DataColumns.ReleaseLogStatusId:
					if (data.ReleaseLogStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDataModel.DataColumns.ReleaseLogStatusId, data.ReleaseLogStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDataModel.DataColumns.ReleaseLogStatusId);
					}
					break;

				case ReleaseLogDataModel.DataColumns.ReleaseDateMax:
					if (data.ReleaseDateMax != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDataModel.DataColumns.ReleaseDateMax, data.ReleaseDateMax);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDataModel.DataColumns.ReleaseDateMax);
					}
					break;

				case ReleaseLogDataModel.DataColumns.ReleaseDateMin:
					if (data.ReleaseDateMin != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDataModel.DataColumns.ReleaseDateMin, data.ReleaseDateMin);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDataModel.DataColumns.ReleaseDateMin);
					}
					break;

				case ReleaseLogDataModel.DataColumns.ReleaseDate:
					if (data.ReleaseDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDataModel.DataColumns.ReleaseDate, data.ReleaseDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDataModel.DataColumns.ReleaseDate);
					}
					break;

				case ReleaseLogDataModel.DataColumns.VersionNo:
					if (!string.IsNullOrEmpty(data.VersionNo))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDataModel.DataColumns.VersionNo, data.VersionNo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDataModel.DataColumns.VersionNo);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;
		}

		#region GetList

        public static List<ReleaseLogDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ReleaseLogDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ReleaseLogDataModel GetDetails(ReleaseLogDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ReleaseLogDataModel> GetEntityDetails(ReleaseLogDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseLogSearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,	ApplicationId		= requestProfile.ApplicationId
				,   ReleaseLogId        = dataQuery.ReleaseLogId
				,   ReleaseLogStatusId  = dataQuery.ReleaseLogStatusId
				,   ReleaseDateMin      = dataQuery.ReleaseDateMin
				,   ReleaseDateMax      = dataQuery.ReleaseDateMax
				,   Name                = dataQuery.Name
				,   ApplicationMode     = requestProfile.ApplicationModeId
				,   ReturnAuditInfo     = returnAuditInfo
			};

			List<ReleaseLogDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseLogDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseLogDataModel> GetEntityDetails(ReleaseLogDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.ReleaseLogSearch " +
		//		" "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//		", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.ApplicationId) +
		//		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//		", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseLogId) +
		//		", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseLogStatusId) +
		//		", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseDateMin) +
		//		", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseDateMax) +
		//		", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

		//	var result = new List<ReleaseLogDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new ReleaseLogDataModel();

		//			dataItem.ReleaseLogId		= (int?)dbReader[ReleaseLogDataModel.DataColumns.ReleaseLogId];
		//			dataItem.ApplicationId		= (int?)dbReader[BaseDataModel.BaseDataColumns.ApplicationId];
		//			dataItem.Application		= (string)dbReader[ReleaseLogDataModel.DataColumns.Application];
		//			dataItem.ReleaseLogStatus	= (string)dbReader[ReleaseLogDataModel.DataColumns.ReleaseLogStatus];
		//			dataItem.VersionNo			= (string)dbReader[ReleaseLogDataModel.DataColumns.VersionNo];
		//			dataItem.ReleaseDate		= (DateTime?)dbReader[ReleaseLogDataModel.DataColumns.ReleaseDate];

		//			SetStandardInfo(dataItem, dbReader);

		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion

		#region Get By Release Log Status

		public static DataTable GetByReleaseLogStatus(int releaseStatusLogId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogSearch @ReleaseLogStatusId       =" + releaseStatusLogId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}


		#endregion

		#region Create

		public static void Create(ReleaseLogDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("ReleaseLog.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ReleaseLogDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ReleaseLog.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ReleaseLogDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseLogDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ReleaseLogId = data.ReleaseLogId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ReleaseLogDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		//public static DataSet ReleaseNotesSearch(ReleaseLogDataModel data, RequestProfile requestProfile)
		//{
		//    // formulate SQL
		//    var sql = "EXEC dbo.ReleaseNotesSearch" +
		//        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +

		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.Feature) +
		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.PrimaryDeveloper) +
		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.PrimaryEntity) +
		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseIssueTypeId) +
		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleasePublishCategoryId) +
		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseDateMax) +
		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseDateMin) +
		//        ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.JIRA);

		//    var oDS = new DataAccess.DBDataSet("ReleaseLog.ReleaseNotesSearch", sql, DataStoreKey);
		//    return oDS.DBDataset;
		//}

		#endregion

		#region Save

		private static string Save(ReleaseLogDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleaseLogInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleaseLogUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseLogId) +
				", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseLogStatusId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
				", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.VersionNo) +
				", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseDate) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ReleaseLogDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReleaseLogDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		public static DataSet GetChildren(ReleaseLogDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleaseLogChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReleaseLogDataModel.DataColumns.ReleaseLogId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ReleaseLogDataModel data, RequestProfile requestProfile)
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

        public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
        {
            // get all records for old Application Id
            var sql = "EXEC dbo.ReleaseLogSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

            var oDT = new DBDataTable("ReleaseLog.Search", sql, DataStoreKey);

            // formaulate a new request Profile which will have new Applicationid
            var newRequestProfile = new RequestProfile();
            newRequestProfile.ApplicationId = newApplicationId;
            newRequestProfile.AuditId = requestProfile.AuditId;

            var releaseLogStatusData = new ReleaseLogStatusDataModel();
            var releaseLogStatusList = ReleaseLogStatusDataManager.GetEntityDetails(releaseLogStatusData, newRequestProfile, 0);

            foreach (DataRow dr in oDT.DBTable.Rows)
            {
                var fcModeName = dr[ReleaseLogDataModel.DataColumns.ReleaseLogStatus].ToString();

                //get new release log status id based on release log status name
                var newReleaseLogStatusId = releaseLogStatusList.Find(x => x.Name == fcModeName).ReleaseLogStatusId;

                var data                  = new ReleaseLogDataModel();
                data.ApplicationId        = newApplicationId;
                data.Name                 = dr[ReleaseLogDataModel.DataColumns.Name].ToString();
                data.ReleaseLogStatusId   = newReleaseLogStatusId;

                // check for existing record in new Application Id
                if(!DoesExist(data, newRequestProfile))
                {
                    data.VersionNo   = dr[ReleaseLogDataModel.DataColumns.VersionNo].ToString();
                    data.ReleaseDate = Convert.ToDateTime(dr[ReleaseLogDataModel.DataColumns.ReleaseDate]);
                    data.Description = dr[ReleaseLogDataModel.DataColumns.Description].ToString();
                    data.SortOrder   = Convert.ToInt32(dr[ReleaseLogDataModel.DataColumns.SortOrder]);
                   
                    //create in new application id
                    Create(data, newRequestProfile);
                }

            }
        }


	}
}
