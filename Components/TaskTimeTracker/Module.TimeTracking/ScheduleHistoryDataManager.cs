using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using System.Globalization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class ScheduleHistoryDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ScheduleHistoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ScheduleHistory");
		}

		#region GetDetails

        public static ScheduleHistoryDataModel GetDetails(ScheduleHistoryDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<ScheduleHistoryDataModel> GetEntityDetails(ScheduleHistoryDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ScheduleHistorySearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,   ApplicationId       = requestProfile.ApplicationId
				,   Person              = dataQuery.Person
				,   ScheduleId          = dataQuery.ScheduleId
				,   ScheduleStateName   = dataQuery.ScheduleStateName
				,   ScheduleHistoryId   = dataQuery.ScheduleHistoryId
			};

			List<ScheduleHistoryDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ScheduleHistoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(ScheduleHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("ScheduleHistory.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ScheduleHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ScheduleHistory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ScheduleHistoryDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ScheduleHistoryDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ScheduleHistoryId = dataQuery.ScheduleHistoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(ScheduleHistoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ScheduleHistoryDataModel.DataColumns.ScheduleHistoryId:
					if (data.ScheduleHistoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleHistoryDataModel.DataColumns.ScheduleHistoryId, data.ScheduleHistoryId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.ScheduleHistoryId);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.RecordDate:
					if (data.RecordDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleHistoryDataModel.DataColumns.RecordDate, data.RecordDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.RecordDate);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.Person:
					if (!string.IsNullOrEmpty(data.Person))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleHistoryDataModel.DataColumns.Person, data.Person);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.Person);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.ScheduleStateName:
					if (!string.IsNullOrEmpty(data.ScheduleStateName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleHistoryDataModel.DataColumns.ScheduleStateName, data.ScheduleStateName);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.ScheduleStateName);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.ScheduleId:
					if (data.ScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleHistoryDataModel.DataColumns.ScheduleId, data.ScheduleId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.ScheduleId);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.StartTime:
					if (data.StartTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleHistoryDataModel.DataColumns.StartTime, data.StartTime);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.StartTime);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.EndTime:
					if (data.EndTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleHistoryDataModel.DataColumns.EndTime, data.EndTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.EndTime);
					}
					break;

				case ScheduleHistoryDataModel.DataColumns.TotalHoursWorked:
					if (data.TotalHoursWorked != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleHistoryDataModel.DataColumns.TotalHoursWorked, data.TotalHoursWorked);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.TotalHoursWorked);
					}
					break;

				case ScheduleHistoryDataModel.DataColumns.NextWorkDate:
					if (data.NextWorkDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleHistoryDataModel.DataColumns.NextWorkDate, data.NextWorkDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.NextWorkDate);
					}
					break;

				case ScheduleHistoryDataModel.DataColumns.NextWorkTime:
					if (data.NextWorkTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleHistoryDataModel.DataColumns.NextWorkTime, data.NextWorkTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.NextWorkTime);
					}
					break;

				case ScheduleHistoryDataModel.DataColumns.KnowledgeDate:
					if (data.KnowledgeDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleHistoryDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.KnowledgeDate);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.AcknowledgedById:
					if (data.AcknowledgedById != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleHistoryDataModel.DataColumns.AcknowledgedById, data.AcknowledgedById);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.AcknowledgedById);

					}
					break;

				case ScheduleHistoryDataModel.DataColumns.AcknowledgedBy:
					if (!string.IsNullOrEmpty(data.AcknowledgedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleHistoryDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleHistoryDataModel.DataColumns.AcknowledgedBy);

					}
					break;

			}
			return returnValue;
		}

		public static DataTable Search(ScheduleHistoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		//public static DataTable SearchHistory(ScheduleHistoryDataModel data, int auditId)
		//{
		//	// formulate SQL
		//	var sql = "EXEC dbo.ScheduleHistorySearchHistory " +
		//		" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
		//		", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.ScheduleHistoryId) +
		//		", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.Milestone) +
		//		", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.Feature) +
		//		", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.MilestoneFeatureState) +
		//		", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.MilestoneXFeatureId);

		//	var oDT = new Framework.Components.DataAccess.DBDataTable("ScheduleHistory.Search", sql, DataStoreKey);
		//	return oDT.DBTable;
		//}

		#endregion

		#region Save

		private static string Save(ScheduleHistoryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ScheduleHistoryInsert  ";
					break;

				case "Update":
					sql += "dbo.ScheduleHistoryUpdate  ";
					break;

				default:
					break;

			}

			sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.ScheduleHistoryId) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.RecordDate) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.Person) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.ScheduleStateName) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.EndTime) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.StartTime) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.ScheduleId) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.PersonId) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.WorkDate) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.NextWorkDate) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.NextWorkTime) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.TotalHoursWorked) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.KnowledgeDate) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.AcknowledgedById) +
				", " + ToSQLParameter(data, ScheduleHistoryDataModel.DataColumns.AcknowledgedBy);
			return sql;
		}

		#endregion
	}
}
