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
	public partial class TaskXApplicationUserDataManager : BaseDataManager
	{
		static string DataStoreKey = "";
		
        static TaskXApplicationUserDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskXApplicationUser");
		}

		public static string ToSQLParameter(TaskXApplicationUserDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskXApplicationUserDataModel.DataColumns.TaskXApplicationUserId:
					if (data.TaskXApplicationUserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXApplicationUserDataModel.DataColumns.TaskXApplicationUserId, data.TaskXApplicationUserId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.TaskXApplicationUserId);
					}
					break;
				case TaskXApplicationUserDataModel.DataColumns.TaskId:
					if (data.TaskId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXApplicationUserDataModel.DataColumns.TaskId, data.TaskId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.TaskId);
					}
					break;
				case TaskXApplicationUserDataModel.DataColumns.ApplicationUserId:
					if (data.ApplicationUserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXApplicationUserDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.ApplicationUserId);
					}
					break;

				case TaskXApplicationUserDataModel.DataColumns.TaskStatusTypeId:
					if (data.TaskStatusTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXApplicationUserDataModel.DataColumns.TaskStatusTypeId, data.TaskStatusTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.TaskStatusTypeId);
					}
					break;

				case TaskXApplicationUserDataModel.DataColumns.StartDate:
					if (data.StartDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXApplicationUserDataModel.DataColumns.StartDate, data.StartDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.StartDate);
					}
					break;

				case TaskXApplicationUserDataModel.DataColumns.DueDate:
					if (data.DueDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXApplicationUserDataModel.DataColumns.DueDate, data.DueDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.DueDate);
					}
					break;

				case TaskXApplicationUserDataModel.DataColumns.CompletedDate:
					if (data.CompletedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXApplicationUserDataModel.DataColumns.CompletedDate, data.CompletedDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.CompletedDate);
					}
					break;

				case TaskXApplicationUserDataModel.DataColumns.Task:
					if (!string.IsNullOrEmpty(data.Task))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXApplicationUserDataModel.DataColumns.Task, data.Task);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.Task);
					}
					break;

				case TaskXApplicationUserDataModel.DataColumns.ApplicationUser:
					if (!string.IsNullOrEmpty(data.ApplicationUser))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXApplicationUserDataModel.DataColumns.ApplicationUser, data.ApplicationUser);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXApplicationUserDataModel.DataColumns.ApplicationUser);
					}
					break;


				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#region Create By Task

		public static void Create(int taskId, int[] applicationUserIds, int taskStatusTypeId, int startDate, int dueDate, int completedDate, RequestProfile requestProfile)
		{
			foreach (int applicationUserId in applicationUserIds)
			{
				var sql = "EXEC TaskXApplicationUserInsert " +
						  "@TaskId						=" + taskId + ", " +
						  "@ApplicationId				=" + requestProfile.ApplicationId + ", " +
						  "@ApplicationUserId			=" + applicationUserId + ", " +
						  "@TaskStatusTypeId			=" + taskStatusTypeId + ", " +
						  "@StartDate					=" + startDate + ", " +
						  "@DueDate						=" + dueDate + ", " +
						  "@CompletedDate				=" + completedDate + ", " +
                          "@AuditId						=" + requestProfile.AuditId;

				DBDML.RunSQL("TaskXApplicationUser_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By ApplicationUsers

        public static void CreateByApplicationUser(int applicationUserId, int[] taskIds, int taskStatusTypeId, int startDate, int dueDate, int completedDate, RequestProfile requestProfile)
		{
			foreach (int taskId in taskIds)
			{
				var sql = "EXEC TaskXApplicationUserInsert " +
						  "@TaskId						=" + taskId + ", " +
						  "@ApplicationId				=" + requestProfile.ApplicationId + ", " +
						  "@ApplicationUserId			=" + applicationUserId + ", " +
						  "@TaskStatusTypeId			=" + taskStatusTypeId + ", " +
						  "@StartDate					=" + startDate + ", " +
						  "@DueDate						=" + dueDate + ", " +
						  "@CompletedDate				=" + completedDate + ", " +
                          "@AuditId						=" + requestProfile.AuditId;
				DBDML.RunSQL("TaskXApplicationUser_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By ApplicationUser

        public static DataTable GetByApplicationUser(int applicationUserId, RequestProfile requestProfile)
		{
			var sql = "EXEC TaskXApplicationUserDetails @ApplicationUserId		=" + applicationUserId + ", " +
                          "@AuditId								=" + requestProfile.AuditId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By Task

        public static DataTable GetByTask(int taskId, RequestProfile requestProfile)
		{
			var sql = "EXEC TaskXApplicationUserDetails @TaskId			=" + taskId + ", " +
                          "@AuditId								=" + requestProfile.AuditId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By ApplicationUser

        public static void DeleteByApplicationUser(int ApplicationUserId, RequestProfile requestProfile)
		{
			var sql = "EXEC TaskXApplicationUserDelete @ApplicationUserId			=" + ApplicationUserId + ", " +
                          "@AuditId								=" + requestProfile.AuditId;
			DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Delete By Task

        public static void DeleteByTask(int taskId, RequestProfile requestProfile)
		{
			var sql = "EXEC TaskXApplicationUserDelete @TaskId			=" + taskId + ", " +
                          "@AuditId								=" + requestProfile.AuditId;
			DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Create By Task

        public static void CreateByTask(int taskId, int[] applicationUserIds, RequestProfile requestProfile)
		{
			foreach (int applicationUserId in applicationUserIds)
			{
				var sql = "EXEC TaskXApplicationUserInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @TaskId					=   " + taskId +
							",      @ApplicationUserId				=   " + applicationUserId;
							

				DBDML.RunSQL("TaskXApplicationUserInsert", sql, DataStoreKey);
			}
		}

		#endregion

		//#region DoesExist

		//public static DataTable DoesExist(TaskXApplicationUser.Data data, int auditId)
		//{
		//    var sql = "EXEC dbo.TaskXApplicationUserSearch "			+
		//                    "  @" + Columns.ApplicationUserId			+ "	=  " + data.ApplicationUserId.ToString() +
		//                    "  @" + BaseColumns.ApplicationId	+ "  =  " + ApplicationId +
		//                    ", @" + Columns.TaskId				+ "	=  " + data.TaskId.ToString() +
		//                    ", @" + BaseColumns.AuditId			+ " =  " + auditId.ToString();

		//    var oDT = new Framework.Components.DataAccess.DBDataTable("TaskXApplicationUser.DoesExist", sql, DataStoreKey);

		//    return oDT.DBTable;
		//}


		//#endregion DoesExist
	}
}
