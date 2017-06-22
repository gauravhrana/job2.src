using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis
{
	public partial class ProjectUseCaseStatusDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ProjectUseCaseStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ProjectUseCaseStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ProjectUseCaseStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId:
					if (data.ProjectUseCaseStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId, data.ProjectUseCaseStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId);
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

		public static List<ProjectUseCaseStatusDataModel> GetEntityDetails(ProjectUseCaseStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ProjectUseCaseStatusSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ProjectUseCaseStatusId                  = dataQuery.ProjectUseCaseStatusId
				 ,	Name                    = dataQuery.Name
			};

			List<ProjectUseCaseStatusDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ProjectUseCaseStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ProjectUseCaseStatusDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ProjectUseCaseStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static ProjectUseCaseStatusDataModel GetDetails(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(ProjectUseCaseStatusDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ProjectUseCaseStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ProjectUseCaseStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ProjectUseCaseStatus.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ProjectUseCaseStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ProjectUseCaseStatusDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ProjectUseCaseStatusId  = data.ProjectUseCaseStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ProjectUseCaseStatusDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectUseCaseStatusRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("ProjectUseCaseStatus.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectUseCaseStatusChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ProjectUseCaseStatusDataModel data, RequestProfile requestProfile)
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
