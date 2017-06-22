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
	public partial class FunctionalityHistoryDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static FunctionalityHistoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityHistory");
		}

		#region Get By Functionality

		public static DataTable GetByFunctionality(int functionalityId, RequestProfile requestProfile)
		{
			var sql = "EXEC FunctionalityHistorySearch " +
					  "@FunctionalityId       =" + functionalityId + ", " +
					  "@AuditId				  =" + requestProfile.AuditId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Search
		public static string ToSQLParameter(FunctionalityHistoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityHistoryDataModel.DataColumns.FunctionalityHistoryId:
					if (data.FunctionalityHistoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityHistoryDataModel.DataColumns.FunctionalityHistoryId, data.FunctionalityHistoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityHistoryDataModel.DataColumns.FunctionalityHistoryId);
					}
					break;
				case FunctionalityHistoryDataModel.DataColumns.FunctionalityId:
					if (data.FunctionalityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityHistoryDataModel.DataColumns.FunctionalityId, data.FunctionalityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityHistoryDataModel.DataColumns.FunctionalityId);
					}
					break;
				case FunctionalityHistoryDataModel.DataColumns.FunctionalityActiveStatusId:
					if (data.FunctionalityActiveStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityHistoryDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityHistoryDataModel.DataColumns.FunctionalityActiveStatusId);
					}
					break;
				case FunctionalityHistoryDataModel.DataColumns.FunctionalityPriorityId:
					if (data.FunctionalityPriorityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityHistoryDataModel.DataColumns.FunctionalityPriorityId, data.FunctionalityPriorityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityHistoryDataModel.DataColumns.FunctionalityPriorityId);
					}
					break;
				case FunctionalityHistoryDataModel.DataColumns.Memo:
					if (data.Memo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityHistoryDataModel.DataColumns.Memo, data.Memo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityHistoryDataModel.DataColumns.Memo);
					}
					break;
				case FunctionalityHistoryDataModel.DataColumns.AcknowledgedBy:
					if (data.AcknowledgedBy != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityHistoryDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityHistoryDataModel.DataColumns.AcknowledgedBy);
					}
					break;
				case FunctionalityHistoryDataModel.DataColumns.KnowledgeDate:
					if (data.KnowledgeDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityHistoryDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityHistoryDataModel.DataColumns.KnowledgeDate);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(FunctionalityHistoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}



		#endregion Search

		#region GetDetails

        public static FunctionalityHistoryDataModel GetDetails(FunctionalityHistoryDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityHistoryDataModel> GetEntityDetails(FunctionalityHistoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityHistorySearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	ApplicationId = requestProfile.ApplicationId
				,	ApplicationMode = requestProfile.ApplicationModeId
				,	FunctionalityHistoryId = dataQuery.FunctionalityHistoryId
				,	Name = dataQuery.Name
				,	FunctionalityActiveStatusId = dataQuery.FunctionalityActiveStatusId
				,	FunctionalityId = dataQuery.FunctionalityId
				,	FunctionalityPriorityId = dataQuery.FunctionalityPriorityId
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityHistoryDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityHistoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region CreateOrUpdate
		private static string CreateOrUpdate(FunctionalityHistoryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityHistoryInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityHistoryUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityHistoryDataModel.DataColumns.FunctionalityId) +
						", " + ToSQLParameter(data, FunctionalityHistoryDataModel.DataColumns.FunctionalityHistoryId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, FunctionalityHistoryDataModel.DataColumns.FunctionalityActiveStatusId) +
						", " + ToSQLParameter(data, FunctionalityHistoryDataModel.DataColumns.FunctionalityPriorityId) +
						", " + ToSQLParameter(data, FunctionalityHistoryDataModel.DataColumns.Memo) +
						", " + ToSQLParameter(data, FunctionalityHistoryDataModel.DataColumns.KnowledgeDate) +
						", " + ToSQLParameter(data, FunctionalityHistoryDataModel.DataColumns.AcknowledgedBy);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(FunctionalityHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			DBDML.RunSQL("FunctionalityHistory.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(FunctionalityHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("FunctionalityHistory.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Delete

		public static void Delete(FunctionalityHistoryDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityHistoryDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityHistoryId = dataQuery.FunctionalityHistoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

	}
}