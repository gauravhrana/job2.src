using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
    public partial class TaskPackageXOwnerXTaskDataManager : BaseDataManager
    {
        static string DataStoreKey = "";       

        static TaskPackageXOwnerXTaskDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskPackageXOwnerXTask");
        }

        #region Create By Task

        public static void Create(int taskId, int[] applicationUserIds, int taskPackageId, RequestProfile requestProfile)
        {
            foreach (int ApplicationUserId in applicationUserIds)
            {
                var sql = "EXEC TaskPackageXOwnerXTaskInsert " +
                          "@TaskId						=" + taskId + ", " +
                          "@ApplicationId				=" + 100 + ", " +
                          "@ApplicationUserId			=" + ApplicationUserId + ", " +
                          "@TaskPackageId				=" + taskPackageId + ", " +
						  "@AuditId						=" + requestProfile.AuditId;

                DBDML.RunSQL("TaskPackageXOwnerXTask_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By ApplicationUsers

		public static void CreateByApplicationUser(int applicationUserId, int[] taskIds, int taskPackageId, RequestProfile requestProfile)
        {
            foreach (int taskId in taskIds)
            {
                var sql = "EXEC TaskPackageXOwnerXTaskInsert " +
                          "@TaskId						=" + taskId + ", " +
                          "@ApplicationId				=" + 100 + ", " +
                          "@ApplicationUserId			=" + applicationUserId + ", " +
                          "@TaskPackageId				=" + taskPackageId + ", " +
						  "@AuditId						=" + requestProfile.AuditId;
                DBDML.RunSQL("TaskPackageXOwnerXTask_Insert", sql, DataStoreKey);
            }
        }
        #endregion

		#region Search

        public static DataTable Search(TaskPackageXOwnerXTaskDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
			var sql = "EXEC dbo.TaskPackageXOwnerXTaskSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId) +
				", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId) +
				 ", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId) +
				 ", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId);

            var oDT = new DBDataTable("TaskPackageXOwnerXTask.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static string ToSQLParameter(TaskPackageXOwnerXTaskDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId:
					if (data.TaskPackageXOwnerXTaskId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId, data.TaskPackageXOwnerXTaskId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId);
					}
					break;
				case TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId:
					if (data.TaskId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId, data.TaskId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId);
					}
					break;
				case TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId:
					if (data.TaskPackageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId, data.TaskPackageId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId);
					}
					break;

				case TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId:
					if (data.ApplicationUserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId);
					}
					break;

								case TaskPackageXOwnerXTaskDataModel.DataColumns.Task:
					if (!string.IsNullOrEmpty(data.Task))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskPackageXOwnerXTaskDataModel.DataColumns.Task, data.Task);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPackageXOwnerXTaskDataModel.DataColumns.Task);
					}
					break;

				case TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackage:
					if (!string.IsNullOrEmpty(data.TaskPackage))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackage, data.TaskPackage);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackage);
					}
					break;

				case TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUser:
					if (!string.IsNullOrEmpty(data.ApplicationUser))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUser, data.ApplicationUser);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUser);
					}
					break;


				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        #endregion

        #region Get By ApplicationUser

		public static DataTable GetByApplicationUser(int applicationUserId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskPackageXOwnerXTaskDetails @ApplicationUserId		=" + applicationUserId + ", " +
						  "@AuditId								=" + requestProfile.AuditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

		#region GetDetails

        public static DataTable GetDetails(TaskPackageXOwnerXTaskDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskPackageXOwnerXTaskSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId);
				
			var oDT = new DBDataTable("TaskPackageXOwnerXTask.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

        public static List<TaskPackageXOwnerXTaskDataModel> GetEntityList(TaskPackageXOwnerXTaskDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskPackageXOwnerXTaskSearch " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					   ", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId);

			var result = new List<TaskPackageXOwnerXTaskDataModel>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new TaskPackageXOwnerXTaskDataModel();

					dataItem.TaskPackageXOwnerXTaskId = (int?)dbReader[TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId];
					dataItem.TaskId = (int?)dbReader[TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId];
					dataItem.TaskPackageId = (int?)dbReader[TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId];
					dataItem.ApplicationUserId = (int?)dbReader[TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId];
					
					SetBaseInfo(dataItem, dbReader);

					result.Add(dataItem);
				}
			}

			return result;
		}	

		#endregion

        #region Get By Task

		public static DataTable GetByTask(int taskId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskPackageXOwnerXTaskDetails @TaskId			=" + taskId + ", " +
						  "@AuditId								=" + requestProfile.AuditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

		#region Create

        public static void Create(TaskPackageXOwnerXTaskDataModel data, RequestProfile requestProfile)
		{
            var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("TaskPackageXOwnerXTask.Insert", sql, DataStoreKey);
		}

		#endregion

		#region CreateOrUpdate

        private static string CreateOrUpdate(TaskPackageXOwnerXTaskDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskPackageXOwnerXTaskInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskPackageXOwnerXTaskUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                         ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId) +
				", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId) +
				 ", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId) +
				 ", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId);
						
			return sql;
		}

		#endregion

		#region Update

        public static void Update(TaskPackageXOwnerXTaskDataModel data, RequestProfile requestProfile)
		{
            var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("TaskPackageXOwnerXTask.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

        public static void Delete(TaskPackageXOwnerXTaskDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskPackageXOwnerXTaskDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId);
				
			DBDML.RunSQL("TaskPackageXOwnerXTask.Delete", sql, DataStoreKey);
		}

		#endregion Delete

        #region Delete By ApplicationUser

		public static void DeleteByApplicationUser(int applicationUserId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskPackageXOwnerXTaskDelete @ApplicationUserId			=" + applicationUserId + ", " +
						  "@AuditId								=" + requestProfile.AuditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }
        #endregion

        #region Delete By TaskPackage

		public static void DeleteByTaskPackage(int taskPackageId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskPackageXOwnerXTaskDelete @TaskPackageId			=" + taskPackageId + ", " +
						  "@AuditId								=" + requestProfile.AuditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By Task

		public static void DeleteByTask(int taskId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskPackageXOwnerXTaskDelete @TaskId			=" + taskId + ", " +
						  "@AuditId								=" + requestProfile.AuditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        //#region DoesExist

        //public static DataTable DoesExist(TaskPackageXOwnerXTask.TaskPackageXOwnerXTaskDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.TaskPackageXOwnerXTaskSearch "			+
        //                    "  @" + Columns.ApplicationUserId			+ "	=  " + data.ApplicationUserId.ToString() +
        //                    "  @" + BaseColumns.ApplicationId	+ "  =  " + ApplicationId +
        //                    ", @" + Columns.TaskId				+ "	=  " + data.TaskId.ToString() +
        //                    ", @" + BaseColumns.AuditId			+ " =  " + auditId.ToString();

        //    var oDT = new Framework.Components.DataAccess.DBDataTable("TaskPackageXOwnerXTask.DoesExist", sql, DataStoreKey);

        //    return oDT.DBTable;
        //}


        //#endregion DoesExist
    }
}
