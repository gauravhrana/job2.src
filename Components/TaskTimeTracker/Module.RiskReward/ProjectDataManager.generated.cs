using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RiskReward;

namespace TaskTimeTracker.Components.Module.RiskReward
{
	public partial class ProjectDataManager : StandardDataManager
	{

		private static string DataStoreKey = string.Empty;

		static ProjectDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Project");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ProjectDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ProjectDataModel.DataColumns.ProjectId:
					if (data.ProjectId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectDataModel.DataColumns.ProjectId, data.ProjectId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectDataModel.DataColumns.ProjectId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<ProjectDataModel> GetEntityDetails(ProjectDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ProjectSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ProjectId                  = dataQuery.ProjectId
				 ,	Name                    = dataQuery.Name
			};

			List<ProjectDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ProjectDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static DataTable GetEntityDetails(RequestProfile requestProfile)
		{
			var list = GetEntityDetails(ProjectDataModel.Empty, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Search

		public static DataTable Search(ProjectDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ProjectDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static string Save(ProjectDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ProjectInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ProjectUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ProjectDataModel.DataColumns.ProjectId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ProjectDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Project.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ProjectDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Project.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ProjectDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ProjectDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ProjectId  = data.ProjectId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static DataTable DoesExist(ProjectDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ProjectDataModel();
			doesExistRequest.Name = data.Name;
			return Search(doesExistRequest, requestProfile);
		}

		#endregion

	}
}
