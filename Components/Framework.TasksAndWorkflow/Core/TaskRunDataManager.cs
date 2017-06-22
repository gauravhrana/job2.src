using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.TasksAndWorkflow
{
	public partial class TaskRunDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static TaskRunDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskRun");
		}

		#region GetList

        public static List<TaskRunDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskRunDataModel.Empty, requestProfile, 0);
		}


		#endregion

		#region GetDetails

		public static string ToSQLParameter(TaskRunDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskRunDataModel.DataColumns.TaskRunId:
					if (data.TaskRunId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskRunDataModel.DataColumns.TaskRunId, data.TaskRunId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRunDataModel.DataColumns.TaskRunId);
					}
					break;

				case TaskRunDataModel.DataColumns.TaskEntityId:
					if (data.TaskEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskRunDataModel.DataColumns.TaskEntityId, data.TaskEntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRunDataModel.DataColumns.TaskEntityId);
					}
					break;

				case TaskRunDataModel.DataColumns.TaskScheduleId:
					if (data.TaskScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskRunDataModel.DataColumns.TaskScheduleId, data.TaskScheduleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRunDataModel.DataColumns.TaskScheduleId);
					}
					break;

				case TaskRunDataModel.DataColumns.BusinessDate:
					if (data.BusinessDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskRunDataModel.DataColumns.BusinessDate, data.BusinessDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRunDataModel.DataColumns.BusinessDate);
					}
					break;

				case TaskRunDataModel.DataColumns.StartTime:
					if (data.StartTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskRunDataModel.DataColumns.StartTime, data.StartTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRunDataModel.DataColumns.StartTime);
					}
					break;

				case TaskRunDataModel.DataColumns.EndTime:
					if (data.EndTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskRunDataModel.DataColumns.EndTime, data.EndTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRunDataModel.DataColumns.EndTime);
					}
					break;

				case TaskRunDataModel.DataColumns.RunBy:
					if (data.RunBy != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskRunDataModel.DataColumns.RunBy, data.RunBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRunDataModel.DataColumns.RunBy);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static TaskRunDataModel GetDetails(TaskRunDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch


		public static List<TaskRunDataModel> GetEntityDetails(TaskRunDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TaskRunSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationId = dataQuery.ApplicationId
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				TaskRunId = dataQuery.TaskRunId				
				,
				TaskEntityId = dataQuery.TaskEntityId
				,
				TaskScheduleId = dataQuery.TaskScheduleId
				,
				RunBy = dataQuery.RunBy
				,
				ReturnAuditInfo = returnAuditInfo

			};

			List<TaskRunDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TaskRunDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(TaskRunDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			var id = Framework.Components.DataAccess.DBDML.RunScalarSQL("TaskRun.Insert", sql, DataStoreKey);
			return Convert.ToInt32(id);
		}


		#endregion

		#region Update

		public static void Update(TaskRunDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			Framework.Components.DataAccess.DBDML.RunSQL("TaskRun.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TaskRunDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TaskRunDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TaskRunId = dataQuery.TaskRunId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(TaskRunDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(TaskRunDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskRunInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskRunUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.TaskRunId) +
						", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.TaskEntityId) +
						", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.TaskScheduleId) +
						", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.StartTime) +
						", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.EndTime) +
						 ", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.RunBy) +
						", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.BusinessDate);

			return sql;
		}

		#endregion

		#region DoesExist


		public static bool DoesExist(TaskRunDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TaskRunDataModel();
			doesExistRequest.TaskEntityId = data.TaskEntityId;
			doesExistRequest.TaskScheduleId = data.TaskScheduleId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(TaskRunDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskRunChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskRunDataModel.DataColumns.TaskRunId);

			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(TaskRunDataModel data, RequestProfile requestProfile)
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
