using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
	public partial class ProjectXNeedDataManager : BaseDataManager
	{
		static string DataStoreKey = "";
	
        static ProjectXNeedDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ProjectXNeed");
		}

		public static string ToSQLParameter(ProjectXNeedDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{
				case ProjectXNeedDataModel.DataColumns.ProjectXNeedId:
					if (data.ProjectXNeedId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectXNeedDataModel.DataColumns.ProjectXNeedId, data.ProjectXNeedId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXNeedDataModel.DataColumns.ProjectXNeedId);
					}
					break;

				case ProjectXNeedDataModel.DataColumns.ProjectId:
					if (data.ProjectId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectXNeedDataModel.DataColumns.ProjectId, data.ProjectId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXNeedDataModel.DataColumns.ProjectId);
					}
					break;

				case ProjectXNeedDataModel.DataColumns.NeedId:
					if (data.NeedId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectXNeedDataModel.DataColumns.NeedId, data.NeedId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXNeedDataModel.DataColumns.NeedId);
					}
					break;

				case ProjectXNeedDataModel.DataColumns.Project:
					if (!string.IsNullOrEmpty(data.Project))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectXNeedDataModel.DataColumns.Project, data.Project);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXNeedDataModel.DataColumns.Project);
					}
					break;

				case ProjectXNeedDataModel.DataColumns.Need:
					if (!string.IsNullOrEmpty(data.Need))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectXNeedDataModel.DataColumns.Need, data.Need);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXNeedDataModel.DataColumns.Need);
					}
					break;
			}

			return returnValue;
		}

		#region Create By Project

		public static void CreateByProject(int projectId, int[] needIds, RequestProfile requestProfile)
		{
			foreach (int needId in needIds)
			{
				var sql = "EXEC ProjectXNeedInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @ProjectId					=   " + projectId +
							",      @NeedId				=   " + needId;

				DBDML.RunSQL("ProjectXNeedInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By Need

		public static void CreateByNeed(int needId, int[] projectIds, RequestProfile requestProfile)
		{
			foreach (int projectId in projectIds)
			{
				var sql = "EXEC ProjectXNeedInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @ProjectId					=   " + projectId +
							",      @NeedId				=   " + needId;
				DBDML.RunSQL("ProjectXNeedInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Get By Need

		public static DataTable GetByNeed(int needId, RequestProfile requestProfile)
		{
			var sql = "EXEC ProjectXNeedSearch @NeedId     =" + needId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion

		#region Get By Project

		public static DataTable GetByProject(int projectId, int auditId)
		{
			var sql = "EXEC ProjectXNeedSearch @ProjectId       =" + projectId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion	
		
		#region Delete By Need

        public static void DeleteByNeed(int needId, RequestProfile requestProfile)
		{
			var sql = "EXEC ProjectXNeedDelete @NeedId       =" + needId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
			DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Delete By Project

        public static void DeleteByProject(int projectId, RequestProfile requestProfile)
		{
			var sql = "EXEC ProjectXNeedDelete @ProjectId		=" + projectId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
			DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

	}
}
