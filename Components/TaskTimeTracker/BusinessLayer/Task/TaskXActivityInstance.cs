using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
	public partial class TaskXActivityInstanceDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";
		
        static TaskXActivityInstanceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskXActivityInstance");			
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskXActivityInstanceSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
                + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("TaskXActivityInstance.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(TaskXActivityInstanceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId:
					if (data.TaskXActivityInstanceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId, data.TaskXActivityInstanceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId);
					}
					break;
				case TaskXActivityInstanceDataModel.DataColumns.TaskId:
					if (data.TaskId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXActivityInstanceDataModel.DataColumns.TaskId, data.TaskId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXActivityInstanceDataModel.DataColumns.TaskId);
					}
					break;
				case TaskXActivityInstanceDataModel.DataColumns.ActivityId:
					if (data.ActivityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXActivityInstanceDataModel.DataColumns.ActivityId, data.ActivityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXActivityInstanceDataModel.DataColumns.ActivityId);
					}
					break;

				case TaskXActivityInstanceDataModel.DataColumns.Task:
					if (!string.IsNullOrEmpty(data.Task))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXActivityInstanceDataModel.DataColumns.Task, data.Task);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXActivityInstanceDataModel.DataColumns.Task);
					}
					break;

				case TaskXActivityInstanceDataModel.DataColumns.Activity:
					if (!string.IsNullOrEmpty(data.Activity))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXActivityInstanceDataModel.DataColumns.Activity, data.Activity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXActivityInstanceDataModel.DataColumns.Activity);
					}
					break;


				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;
		}

        public static DataTable Search(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL  
			var sql = "EXEC dbo.TaskXActivityInstanceSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +				
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId) +
				", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.ActivityId) +
				", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskId);


			var oDT = new DBDataTable("TaskXActivityInstance.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion Search

		#region GetDetails

        public static DataTable GetDetails(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskXActivityInstanceSearch " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId);

			var oDT = new DBDataTable("TaskXActivityInstance.GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}

        public static List<TaskXActivityInstanceDataModel> GetEntityDetails(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskXActivityInstanceSearch " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					   ", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId);

			var result = new List<TaskXActivityInstanceDataModel>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new TaskXActivityInstanceDataModel();

					dataItem.TaskXActivityInstanceId	= (int)dbReader[TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId];
					dataItem.TaskId						= (int)dbReader[TaskXActivityInstanceDataModel.DataColumns.TaskId];
					dataItem.ActivityId					= (int)dbReader[TaskXActivityInstanceDataModel.DataColumns.ActivityId];

					SetStandardInfo(dataItem, dbReader);

					SetBaseInfo(dataItem, dbReader);

					result.Add(dataItem);
				}
			}

			return result;
		}

		#endregion GetDetails

		#region CreateOrUpdate
        private static string CreateOrUpdate(TaskXActivityInstanceDataModel data, RequestProfile requestProfile, string action)
		{
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
                    sql += "dbo.TaskXActivityInstanceInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);
						
					break;

				case "Update":
                    sql += "dbo.TaskXActivityInstanceUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                           ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId) +
						", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskId) +
						", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.ActivityId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create
        public static void Create(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
            var sql = CreateOrUpdate(data, requestProfile, "Create");

			DBDML.RunSQL("TaskXActivityInstance.Insert", sql, DataStoreKey);
		}
		#endregion Create

		#region Update
        public static void Update(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
            var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("TaskXActivityInstance.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskXActivityInstanceRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskXActivityInstance.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

        public static void Delete(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskXActivityInstanceDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId);

			DBDML.RunSQL("TaskXActivityInstance.Delete", sql, DataStoreKey);
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(TaskXActivityInstanceDataModel data, RequestProfile requestProfile )
		{
			var sql = "EXEC dbo.TaskXActivityInstanceSearch " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId) +
							", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("TaskXActivityInstance.DoesExist", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion DoesExist

		#region GetChildren

        private static DataSet GetChildren(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskXActivityInstanceChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId);

			var oDT = new DBDataSet("TaskXActivityInstance.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

        public static DataSet DeleteChildren(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskXActivityInstanceChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId);

			var oDT = new DBDataSet("TaskXActivityInstance.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

        public static bool IsDeletable(TaskXActivityInstanceDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;

            var ds = GetChildren(data, requestProfile);

			if (ds != null && ds.Tables.Count > 0)
			{
				if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
				{
					isDeletable = false;
				}
			}

			return isDeletable;
		}

		#endregion
	}
}
