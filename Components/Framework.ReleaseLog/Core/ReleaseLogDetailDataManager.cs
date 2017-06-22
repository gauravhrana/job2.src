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
	public partial class ReleaseLogDetailDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static ReleaseLogDetailDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseLogDetail");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ReleaseLogDetailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{
				case ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId:
					if (data.ReleaseLogDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId, data.ReleaseLogDetailId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ApplicationId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ModuleId:
					if (data.ModuleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ModuleId, data.ModuleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ModuleId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ReleaseFeatureId:
					if (data.ReleaseFeatureId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ReleaseFeatureId, data.ReleaseFeatureId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleaseFeatureId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.SystemEntityTypeId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId:
					if (data.ReleaseIssueTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId, data.ReleaseIssueTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId:
					if (data.ReleasePublishCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId, data.ReleasePublishCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ReleaseLogId:
					if (data.ReleaseLogId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ReleaseLogId, data.ReleaseLogId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleaseLogId);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ItemNo:
					if (data.ItemNo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.ItemNo, data.ItemNo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ItemNo);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseLogDetailDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.SortOrder);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.RequestedDate:
					if (data.RequestedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.RequestedDate, data.RequestedDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.RequestedDate);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ReleaseDateMax:
					if (data.ReleaseDateMax != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMax, data.ReleaseDateMax);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMax);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ReleaseDateMin:
					if (data.ReleaseDateMin != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMin, data.ReleaseDateMin);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMin);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMin:
					if (data.UpdatedDateRangeMin != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMin, data.UpdatedDateRangeMin);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMin);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMax:
					if (data.UpdatedDateRangeMax != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMax, data.UpdatedDateRangeMax);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMax);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper:
					if (!string.IsNullOrEmpty(data.PrimaryDeveloper))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper, data.PrimaryDeveloper.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.PrimaryEntity:
					if (!string.IsNullOrEmpty(data.PrimaryEntity))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.PrimaryEntity, data.PrimaryEntity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.PrimaryEntity);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.SystemEntityType:
					if (!string.IsNullOrEmpty(data.SystemEntityType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.SystemEntityType, data.SystemEntityType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.SystemEntityType);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.JIRA:
					if (!string.IsNullOrEmpty(data.JIRA))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.JIRA, data.JIRA);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.JIRA);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.Feature:
					if (!string.IsNullOrEmpty(data.Feature))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.Feature, data.Feature);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.Feature);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.Module:
					if (!string.IsNullOrEmpty(data.Module))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.Module, data.Module);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.Module);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.ReleaseFeature:
					if (!string.IsNullOrEmpty(data.ReleaseFeature))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.ReleaseFeature, data.ReleaseFeature);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.ReleaseFeature);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.Description);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.TimeSpent:
					if (!string.IsNullOrEmpty(data.TimeSpent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.TimeSpent, data.TimeSpent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.TimeSpent);
					}
					break;

				case ReleaseLogDetailDataModel.DataColumns.RequestedBy:
					if (!string.IsNullOrEmpty(data.RequestedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseLogDetailDataModel.DataColumns.RequestedBy, data.RequestedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseLogDetailDataModel.DataColumns.RequestedBy);
					}
					break;
			}

			return returnValue;
		}

		#endregion

		#region GetList

        public static List<ReleaseLogDetailDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ReleaseLogDetailDataModel.Empty, requestProfile, 0);
		}

        public static List<ReleaseLogDetailDataModel> GetList(string prefixText, RequestProfile requestProfile)
		{
            var data = new ReleaseLogDetailDataModel() { Feature = prefixText };
            return GetEntityDetails(data, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ReleaseLogDetailDataModel GetDetails(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ReleaseLogDetailDataModel> GetEntityDetails(ReleaseLogDetailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseLogDetailSearch ";

			var parameters =
			new
			{
				    AuditId                             = requestProfile.AuditId
				,	ApplicationId						= dataQuery.ApplicationId
				,   ReleaseLogId                        = dataQuery.ReleaseLogId
				,   ReleaseLogDetailId                  = dataQuery.ReleaseLogDetailId
				,   Feature                             = dataQuery.Feature
				,   ModuleId							= dataQuery.ModuleId
				,   ReleaseFeatureId                    = dataQuery.ReleaseFeatureId
				,   SystemEntityTypeId                  = dataQuery.SystemEntityTypeId
				,   PrimaryDeveloper                    = dataQuery.PrimaryDeveloper
				,   PrimaryEntity                       = dataQuery.PrimaryEntity
				,   ReleaseIssueTypeId                  = dataQuery.ReleaseIssueTypeId
				,   ReleasePublishCategoryId            = dataQuery.ReleasePublishCategoryId
				,   JIRA                                = dataQuery.JIRA
				,   UpdatedDateRangeMin                 = dataQuery.UpdatedDateRangeMin
				,   UpdatedDateRangeMax                 = dataQuery.UpdatedDateRangeMax
                ,   ReleaseDateMax                      = dataQuery.ReleaseDateMax
                ,   ReleaseDateMin                      = dataQuery.ReleaseDateMin
				//,	RequestedDate						= dataQuery.ReleaseDate
				,   ReturnAuditInfo                     = returnAuditInfo
			};

			List<ReleaseLogDetailDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseLogDetailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseLogDetailDataModel> GetEntityDetails(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.ReleaseLogDetailSearch " +
		//			 " "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ApplicationId) +
		//			 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseLogId) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.Feature) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ModuleId) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.PrimaryEntity) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.JIRA) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMin) +
		//			 ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMax);

		//	var result = new List<ReleaseLogDetailDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new ReleaseLogDetailDataModel();

		//			dataItem.ReleaseLogDetailId				= (int)dbReader[ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId];
		//			dataItem.ApplicationId					= (int)dbReader[ReleaseLogDetailDataModel.DataColumns.ApplicationId];
		//			dataItem.Application					= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.Application];
		//			dataItem.ReleaseLog						= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.ReleaseLog];
		//			dataItem.ReleaseLogId					= (int)dbReader[ReleaseLogDetailDataModel.DataColumns.ReleaseLogId];
		//			dataItem.ItemNo							= (int)dbReader[ReleaseLogDetailDataModel.DataColumns.ItemNo];
		//			dataItem.Description					= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.Description];
		//			dataItem.RequestedBy					= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.RequestedBy];
		//			dataItem.RequestedDate					= (DateTime)dbReader[ReleaseLogDetailDataModel.DataColumns.RequestedDate];
		//			dataItem.PrimaryDeveloper				= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper];
		//			dataItem.SortOrder						= (int)dbReader[ReleaseLogDetailDataModel.DataColumns.SortOrder];
		//			dataItem.ReleaseIssueTypeId				= (int)dbReader[ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId];
		//			dataItem.ReleasePublishCategoryId		= (int)dbReader[ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId];
		//			dataItem.ReleaseIssueType				= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.ReleaseIssueType];
		//			dataItem.ReleasePublishCategory			= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategory];
		//			dataItem.Feature						= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.Feature];
		//			dataItem.Module							= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.Module];
		//			dataItem.JIRA							= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.JIRA];
		//			dataItem.PrimaryEntity					= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.PrimaryEntity];
		//			dataItem.TimeSpent						= (string)dbReader[ReleaseLogDetailDataModel.DataColumns.TimeSpent];

		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}		

		#endregion

		#region GetDevelopmentItemList

		public static List<ReleaseLogDetailDataModel> GetChildReleaseLogDetailItemList(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{

            return GetEntityDetails(data, requestProfile, 0);
			
            // formulate SQL  
            //var sql = "EXEC dbo.ReleaseLogDetailSearch " +
            //    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            //    ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
            //     ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId);

            //var oDt = new DBDataTable("ReleaseLogDetail.GetList", sql, DataStoreKey);

            //return oDt.DBTable;
		}

		#endregion

		#region GetPublishItemList

        public static List<ReleaseLogDetailDataModel> GetParentReleaseLogDetailItemList(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{
            return GetEntityDetails(data, requestProfile, 0);

			// formulate SQL  
            //var sql = "EXEC dbo.ReleaseLogDetailSearch " +
            //    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            //    ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
            //     ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId);

            //var oDt = new DBDataTable("ReleaseLogDetail.GetList", sql, DataStoreKey);

            //return oDt.DBTable;
		}

		#endregion

		//#region Update

		//public static void Update(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = Save(data, requestProfile.AuditId, "Update");
		//	DataAccess.DBDML.RunSQL("ReleaseLogDetail.Update", sql, DataStoreKey);
		//}

		//#endregion

		#region Get By ReleaseLogDetail

		public static DataTable GetByReleaseLogDetail(int releaseLogDetailId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailSearch @ReleaseLogDetailId       =" + releaseLogDetailId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDt = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDt.DBTable;
		}

		#endregion

		#region Get By ReleaseLog

		public static DataTable GetByReleaseLog(int releaseLogId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailSearch @ReleaseLogId       =" + releaseLogId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDt = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDt.DBTable;
		}

		#endregion

		#region Get By Release Log Status

		public static DataTable GetByReleaseLogStatus(int releaseStatusLogId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailSearch @ReleaseLogStatusId       =" + releaseStatusLogId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDt = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDt.DBTable;
		}


		#endregion

		#region Get By Release Log IssueType

		public static DataTable GetByReleaseLogIssueType(int releaseIssueTypeId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailSearch @ReleaseIssueTypeId       =" + releaseIssueTypeId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDt = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDt.DBTable;
		}

		#endregion

		#region Delete

		public static void Delete(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseLogDetailDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ReleaseLogDetailId = data.ReleaseLogDetailId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataSet ReleaseNotesSearch(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.ReleaseNotesSearch" +
				" " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMax) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMin) +
				SearchParameter(data, requestProfile);

			var oDs = new DBDataSet("ReleaseLog.ReleaseNotesSearch", sql, DataStoreKey);
			return oDs.DBDataset;
		}

		public static DataTable Search(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.ReleaseLogDetailSearch " +
				" " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId) +				
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMax) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseDateMin) +
				 SearchParameter(data, requestProfile);

			var oDt = new DBDataTable("ReleaseLogDetail.Search", sql, DataStoreKey);
			return oDt.DBTable;
		}

		public static string SearchParameter(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseLogId) +
                ", " + ToSQLParameter(ReleaseLogDetailDataModel.DataColumns.ApplicationId, data.ApplicationId) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.Feature) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ModuleId) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseFeatureId) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.Description) +				
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.PrimaryEntity) +
				//", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.SystemEntityTypeId) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.JIRA) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMin) +
				", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.UpdatedDateRangeMax);

			return sql;
		}

		#endregion

		#region Save

		public static void Save(ReleaseLogDetailDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Insert":
					data.RequestedBy = "Admin";										 					
					sql += "dbo.ReleaseLogDetailInsert  ";
					break;

				case "Update":
                    sql += "dbo.ReleaseLogDetailUpdate  ";
					break;
			}

			sql +=  "  " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ApplicationId)    +       // Application Id is  needed in both Update and Insert
                    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseLogId) +				
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.Feature) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.Module) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseFeatureId) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.SystemEntityTypeId) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.PrimaryEntity) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.JIRA) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.ItemNo) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.Description) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.SortOrder) +
				 // ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.RequestedDate) + // this will be taken from ReleaseLog tab via procedure
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.TimeSpent) +
				    ", " + ToSQLParameter(data, ReleaseLogDetailDataModel.DataColumns.RequestedBy);

			DBDML.RunSQL("ReleaseLogDetail.Save", sql, DataStoreKey);
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ReleaseLogDetailDataModel data, RequestProfile requestProfile)
		{
            var doesExistRequest = new ReleaseLogDetailDataModel();
            doesExistRequest.ReleaseLogDetailId = data.ReleaseLogDetailId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetStatisticData

		public static decimal GetTotalTimeSpent(DataTable dt, int releaseNotesTimeSpentConstant)
		{
			var series = new decimal[dt.Rows.Count];
			var i = 0;

			foreach (DataRow item in dt.Rows)
			{
				var timeSpent = item[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString();

				var timeSpentValue = 0m;

				if (!Decimal.TryParse(timeSpent, out timeSpentValue))
				{
					timeSpentValue = releaseNotesTimeSpentConstant;
				}

				series[i++] = timeSpentValue;
			}

			var timeSpentForGroup = series.Sum();
			return timeSpentForGroup;
		}

		public static Statistic GetStatisticData(DataTable releaseLogDetails, int releaseNotesTimeSpentConstant, string statisticUnknown)
		{
			var dataItem = new Statistic();


			dataItem.Total = GetTotalTimeSpent(releaseLogDetails, releaseNotesTimeSpentConstant);

			var totalCount = releaseLogDetails.Rows.Count;
			dataItem.Count = totalCount;

			var dt = new DataTable();
			dt.Columns.Add(ReleaseLogDetailDataModel.DataColumns.TimeSpent);
			dt.AcceptChanges();

			var rowT = dt.NewRow();

			var list = releaseLogDetails.AsEnumerable()
					  .Where(x => x[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString().ToLower() != statisticUnknown);

			var list1 = releaseLogDetails.AsEnumerable()
						.Where(x => x[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString().ToLower() == statisticUnknown);

			var rows = from row in list1.AsEnumerable()
					   select row;
			// takes care of the logic of TimeSpent column with UnKnown value to calculate average and median
			foreach (var row in rows)
			{
				if (row[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString().ToLower() == statisticUnknown)
				{
					rowT = dt.NewRow();
					rowT[ReleaseLogDetailDataModel.DataColumns.TimeSpent] = releaseNotesTimeSpentConstant;
					dt.Rows.Add(rowT);
				}
			}

			var list2 = list.Concat(dt.AsEnumerable());

			var dataRows = list2 as DataRow[] ?? list2.ToArray();
			var rowItem = from row in dataRows.AsEnumerable() select row;

			if (rowItem.Any())
			{
				//calculates the average value
				dataItem.Average = dataRows
								  .Where(x => x[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString().ToLower() != statisticUnknown)
								  .Select(x => Convert.ToDecimal(x[ReleaseLogDetailDataModel.DataColumns.TimeSpent])).Average();

				//calculates the max and min values 
				dataItem.Max = dataRows.Max(x => Convert.ToDecimal(x[ReleaseLogDetailDataModel.DataColumns.TimeSpent]));
				dataItem.Min = dataRows.Min(x => Convert.ToDecimal(x[ReleaseLogDetailDataModel.DataColumns.TimeSpent]));

				// gets the ordered list to find the median
				var orderedList = dataRows.OrderBy(p => Convert.ToDecimal(p[ReleaseLogDetailDataModel.DataColumns.TimeSpent]));

				// calculates median for even number list
				if ((totalCount % 2) == 0)
				{
					dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]) + Convert.ToDecimal(orderedList.ElementAt((totalCount - 1) / 2)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]);
					dataItem.Median /= 2;
				}
				else
				{
					// calculating median for odd number list
					if (totalCount == 1)
					{
						dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount - 1)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]);
					}
					else
					{
						dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]);
					}
				}
			}

			return dataItem;
		}

		public static Dictionary<string, decimal> GetStatisticDataSummary(DataTable relaseLogDetails, int releaseNotesTimeSpentConstant, string statisticUnknown)
		{
			var lstResult = new Dictionary<string, decimal>();

			var dt = new DataTable();
			dt.Clear();
			dt.Columns.Add(ReleaseLogDetailDataModel.DataColumns.TimeSpent);
			var rowT = dt.NewRow();
			decimal average = 0;

			var totalTimeSpent = GetTotalTimeSpent(relaseLogDetails, releaseNotesTimeSpentConstant);
			lstResult.Add("TotalTimeSpent", totalTimeSpent);

			//finding total number of records
			var count = relaseLogDetails.AsEnumerable()
						.Select(x => x[ReleaseLogDetailDataModel.DataColumns.TimeSpent]).Count();

			lstResult.Add("Count", count);

			if (count != 0)
			{
				average = totalTimeSpent / count;
			}

			var list = relaseLogDetails.AsEnumerable()
						.Where(x => x[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString().ToLower() != statisticUnknown);

			var list1 = relaseLogDetails.AsEnumerable()
						.Where(x => x[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString().ToLower() == statisticUnknown);


			var rows = from row in list1.AsEnumerable()
					   select row;

			foreach (var row in rows)
			{
				if (row[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString().ToLower() == statisticUnknown)
				{
					rowT = dt.NewRow();
					rowT[ReleaseLogDetailDataModel.DataColumns.TimeSpent] = releaseNotesTimeSpentConstant;
					dt.Rows.Add(rowT);
				}
			}

			var listMedian = list.Concat(dt.AsEnumerable());

			var orderedList = listMedian.OrderBy(p => Convert.ToDecimal(p[ReleaseLogDetailDataModel.DataColumns.TimeSpent]));
			decimal median = 0;

			if (count != 0)
			{
				// calculating median for even number list
				if ((count % 2) == 0)
				{
					median = Convert.ToDecimal(orderedList.ElementAt(count / 2)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]) + Convert.ToDecimal(orderedList.ElementAt((count - 1) / 2)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]);
					median /= 2;
				}
				else
				{
					// calculating median for odd number list
					if (count == 1)
					{
						median = Convert.ToDecimal(orderedList.ElementAt(count - 1)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]);
					}
					else
					{
						median = Convert.ToDecimal(orderedList.ElementAt(count / 2)[ReleaseLogDetailDataModel.DataColumns.TimeSpent]);
					}
				}
			}


			lstResult.Add("Average", average);
			lstResult.Add("Median", median);
			return lstResult;
		}

		#endregion

	}
}
