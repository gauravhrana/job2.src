using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Data;

namespace TaskTimeTracker.Components.Module.Priority
{
	public partial class TaskPriorityXApplicationUserDataManager : BaseDataManager
	{
        static readonly string DataStoreKey = "";
		
		static TaskPriorityXApplicationUserDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskPriorityXApplicationUser");
		}

		#region Get By ApplicationUser

		public static DataTable GetByApplicationUser(int applicationUserId, int auditId)
		{
			var sql = "EXEC TaskPriorityXApplicationUserDetails @ApplicationUserId		=" + applicationUserId + ", " +
						  "@AuditId								=" + auditId;
			var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By Task

		public static DataTable GetByTaskPriority(int taskPriorityId, int auditId)
		{
			var sql = "EXEC TaskPriorityXApplicationUserDetails @TaskId			=" + taskPriorityId + ", " +
						  "@AuditId								=" + auditId;
			var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By ApplicationUser

		public static void DeleteByApplicationUser(int applicationUserId, int auditId)
		{
			var sql = "EXEC TaskPriorityXApplicationUserDelete @ApplicationUserId			=" + applicationUserId + ", " +
						  "@AuditId								=" + auditId;
			Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Delete By Task

		public static void DeleteByTaskPriority(int taskId, int auditId)
		{
			var sql = "EXEC TaskPriorityXApplicationUserDelete @TaskId			=" + taskId + ", " +
						  "@AuditId								=" + auditId;
			Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

        #endregion

        public static string ToSQLParameter(TaskPriorityXApplicationUserDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId:
                    if (data.TaskPriorityXApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId, data.TaskPriorityXApplicationUserId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId);

                    }
                    break;

                case TaskPriorityXApplicationUserDataModel.DataColumns.TaskId:
                    if (data.TaskId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPriorityXApplicationUserDataModel.DataColumns.TaskId, data.TaskId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPriorityXApplicationUserDataModel.DataColumns.TaskId);

                    }
                    break;

                case TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityTypeId:
                    if (data.TaskPriorityTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityTypeId, data.TaskPriorityTypeId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityTypeId);

                    }
                    break;

                case TaskPriorityXApplicationUserDataModel.DataColumns.ApplicationUserId:
                    if (data.ApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPriorityXApplicationUserDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPriorityXApplicationUserDataModel.DataColumns.ApplicationUserId);

                    }
                    break;

            }
            return returnValue;
        }

		#region GetList

        public static List<TaskPriorityXApplicationUserDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskPriorityXApplicationUserDataModel.Empty, requestProfile);
		}

		#endregion

		#region GetDetails

        public static TaskPriorityXApplicationUserDataModel GetDetails(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

        public static List<TaskPriorityXApplicationUserDataModel> GetEntityDetails(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskPriorityXApplicationUserSearch " +
               " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails ) +
               ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId);

			var result = new List<TaskPriorityXApplicationUserDataModel>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new TaskPriorityXApplicationUserDataModel();

					dataItem.TaskPriorityXApplicationUserId  = (int?)dbReader[TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId];
					dataItem.TaskId                          = (int?)dbReader[TaskPriorityXApplicationUserDataModel.DataColumns.TaskId];
					dataItem.TaskPriorityTypeId              = (int?)dbReader[TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityTypeId];
					dataItem.ApplicationUserId               = (int?)dbReader[TaskPriorityXApplicationUserDataModel.DataColumns.ApplicationUserId];				
					

					//SetBaseInfo(dataItem, dbReader);

					result.Add(dataItem);
				}
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile )
		{
            var sql = Save(data, requestProfile, "Create");
			Framework.Components.DataAccess.DBDML.RunSQL("TaskPriorityXApplicationUser.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

        public static void Update(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile)
		{
            var sql = Save(data, requestProfile, "Update");
			Framework.Components.DataAccess.DBDML.RunSQL("TaskPriorityXApplicationUser.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

        public static void Delete(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskPriorityXApplicationUserDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId);

			Framework.Components.DataAccess.DBDML.RunSQL("TaskPriorityXApplicationUser.Delete", sql, DataStoreKey);
		}

		#endregion

		#region Search

        public static DataTable Search(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.TaskPriorityXApplicationUserSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId) +
                ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskId) +
                ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityTypeId) +
                ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.ApplicationUserId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("TaskPriorityXApplicationUser.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

        private static string Save(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskPriorityXApplicationUserInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskPriorityXApplicationUserUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

            sql = sql + ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId) +
                        ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskId) +
                        ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityTypeId) +
                        ", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.ApplicationUserId) +
                        ", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.UpdatedBy) +
                        ", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.UpdatedDate);
			return sql;
		}

		#endregion

		#region Create By TaskPriority

        public static void CreateByTaskPriority(int taskPriorityTypeId, int taskId, int[] ApplicationUserIds, RequestProfile requestProfile)
		{
			foreach (int applicationUserId in ApplicationUserIds)
			{
				var sql = "EXEC TaskPriorityXApplicationUserInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @TaskPriorityTypeId			=   " + taskPriorityTypeId +
							",   @ApplicationUserId				=   " + applicationUserId +				
							",   @TaskId						=	" + taskId;

				Framework.Components.DataAccess.DBDML.RunSQL("TaskPriorityXApplicationUserInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By ApplicationUser

        public static void CreateByApplicationUser(int applicationUserId, int taskId, int[] TaskPriorityTypeIds, RequestProfile requestProfile)
		{
			foreach (int taskPriorityTypeId in TaskPriorityTypeIds)
			{
				var sql = "EXEC TaskPriorityXApplicationUserInsert  " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @TaskPriorityTypeId			=   " + taskPriorityTypeId +
							",   @ApplicationUserId				=   " + applicationUserId +	
							",   @TaskId						=	" + taskId;
				Framework.Components.DataAccess.DBDML.RunSQL("TaskPriorityXApplicationUserInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region DoesExist

        public static bool DoesExist(TaskPriorityXApplicationUserDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskPriorityXApplicationUserSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
			", " + ToSQLParameter(data, TaskPriorityXApplicationUserDataModel.DataColumns.TaskPriorityXApplicationUserId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("TaskPriorityXApplicationUser.DoesExist", sql, DataStoreKey);
			return oDT.DBTable.Rows.Count > 0;
		}

		#endregion

	}
}
