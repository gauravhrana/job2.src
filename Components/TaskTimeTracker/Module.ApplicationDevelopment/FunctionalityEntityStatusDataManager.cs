using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;


namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public class FunctionalityEntityStatusDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static FunctionalityEntityStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityEntityStatus");
		}

		#region GetList

        public static List<FunctionalityEntityStatusDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityEntityStatusDataModel.Empty, requestProfile, 1);
		}

		#endregion GetGroupbyList

		#region GetGroupbyList

		public static DataTable GetAggregateList(int functionalityid, string groupbyField, string subgroupbyField, DateTime startDate1, DateTime startDate2, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityEntityStatusAggregateList " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", @FunctionalityId = " + functionalityid +
				", @GroupByFieldName = " + groupbyField +
				", @SubGroupByFieldName = " + subgroupbyField +
				", @Date1 = '" + startDate1 + "'" +
				", @Date2 = '" + startDate2 + "'";

			var oDT = new DBDataTable("FunctionalityEntityStatus.AggregateList", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDateRangeList

		public static DataTable GetDateRangeList(string functionality, DateTime startDate1, DateTime endDate)
		{
			var sql = "EXEC dbo.FunctionalityEntityStatusArchiveSearchDateRange" +

				" @Functionality = '" + functionality + "'" +
				", @Date = '" + startDate1 + "'" +
				", @Date2 = '" + endDate + "'";


			var oDT = new DBDataTable("FunctionalityEntityStatus.DateRangeList", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetUniqueIdList

		public static DataTable GetUniqueIdList(string entityName, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityEntityStatusUniqueIdList " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", @EntityName = " + entityName;

			var oDT = new DBDataTable("FunctionalityEntityStatus.UniqueIdList", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion



		#region GetDetails

		public static FunctionalityEntityStatusDataModel GetDetails(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityEntityStatusDataModel> GetEntityDetails(FunctionalityEntityStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityEntityStatusSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	FunctionalityEntityStatusId = dataQuery.FunctionalityEntityStatusId
				,	ReturnAuditInfo = returnAuditInfo
				,	ApplicationId = dataQuery.ApplicationId
			};

			List<FunctionalityEntityStatusDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityEntityStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("FunctionalityEntityStatus.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("FunctionalityEntityStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FunctionalityEntityStatusDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityEntityStatusDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityEntityStatusId = dataQuery.FunctionalityEntityStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(FunctionalityEntityStatusDataModel data, string dataColumnName, object value)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId:
					if (data.FunctionalityEntityStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.ApplicationId);
					}
					break;

				
				case FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId:
					if (data.FunctionalityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId, data.FunctionalityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId:
					if (value != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId, value);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId:
					if (data.FunctionalityPriorityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId, data.FunctionalityPriorityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.FunctionalityActiveStatusId:
					if (data.FunctionalityActiveStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityActiveStatusId);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.Memo:
					if (data.Memo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusDataModel.DataColumns.Memo, data.Memo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.Memo);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.StartDate:
					if (data.StartDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusDataModel.DataColumns.StartDate, data.StartDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.StartDate);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.StartDate2:
					if (data.StartDateR2 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusDataModel.DataColumns.StartDate2, data.StartDateR2);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.StartDate2);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.TargetDate:
					if (data.TargetDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusDataModel.DataColumns.TargetDate, data.TargetDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.TargetDate);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.TargetDate2:
					if (data.TargetDateR2 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusDataModel.DataColumns.TargetDate2, data.TargetDateR2);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.TargetDate2);
					}
					break;

				case FunctionalityEntityStatusDataModel.DataColumns.AssignedTo:
					if (data.AssignedTo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusDataModel.DataColumns.AssignedTo, data.AssignedTo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusDataModel.DataColumns.AssignedTo);
					}
					break;



				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile)
		{
			var mergeddt = new DataTable();
			var procSql = string.Empty;
			var sql = string.Empty;
			
			if (data.FunctionalityActiveStatusId != null)
				procSql = "EXEC dbo.FESModuleIntegratedSearch ";
			else
				procSql = "EXEC dbo.FunctionalityEntityStatusSearch ";

			if (data.FunctionalityStatusIds != null && data.FunctionalityStatusIds.Length >= 1)
			{
				for (int i = 0; i < data.FunctionalityStatusIds.Length; i++)
				{
					sql = procSql +
					" " + BaseDataManager.SetCommonParametersForSearch(requestProfile.AuditId, requestProfile.ApplicationId,requestProfile.ApplicationModeId) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId) +
					//", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.ApplicationId, data.ApplicationId) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId, data.FunctionalityId) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId, data.FunctionalityPriorityId) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId, data.FunctionalityStatusIds[i]) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.AssignedTo, data.AssignedTo) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.StartDate, data.StartDate) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.TargetDate, data.TargetDate) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.StartDate2, data.StartDateR2) +
					 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
					", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.TargetDate2, data.TargetDateR2);

					var oDT = new DBDataTable("FunctionalityEntityStatus.Search", sql, DataStoreKey);
					var dt = new DataTable();
					dt = oDT.DBTable;
					if (mergeddt.Rows.Count == 0)
					{
						mergeddt = dt.Clone();
					}
					foreach (DataRow row in dt.Rows)
					{
						mergeddt.ImportRow(row);
					}

				}
				return mergeddt;
			}
			else
			{
				// formulate SQL
				sql = procSql +
				"  " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.ApplicationId, data.ApplicationId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId, data.FunctionalityId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId, data.FunctionalityPriorityId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId, data.FunctionalityStatusId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.AssignedTo, data.AssignedTo) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.StartDate, data.StartDate) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.TargetDate, data.TargetDate) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.StartDate2, data.StartDateR2) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.TargetDate2, data.TargetDateR2);

				var oDT = new DBDataTable("FunctionalityEntityStatus.Search", sql, DataStoreKey);
				return oDT.DBTable;
			}

		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityEntityStatusInsert  ";
					break;

				case "Update":
					sql += "dbo.FunctionalityEntityStatusUpdate  ";
					break;

				default:
					break;

			}

			sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId, data.FunctionalityId) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId, data.FunctionalityStatusId) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId, data.FunctionalityPriorityId) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.AssignedTo, data.AssignedTo) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.Memo, data.Memo) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.TargetDate, data.TargetDate) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.StartDate, data.StartDate);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityEntityStatusDataModel();
			doesExistRequest.SystemEntityTypeId = data.SystemEntityTypeId;
			doesExistRequest.FunctionalityId = data.FunctionalityId;
			doesExistRequest.FunctionalityStatusId = data.FunctionalityStatusId;
			doesExistRequest.FunctionalityPriorityId = data.FunctionalityPriorityId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityEntityStatusChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityEntityStatusDataModel data, RequestProfile requestProfile)
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

	}
}