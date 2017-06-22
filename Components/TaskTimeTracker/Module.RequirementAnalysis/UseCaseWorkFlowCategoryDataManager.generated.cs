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
	public partial class UseCaseWorkFlowCategoryDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static UseCaseWorkFlowCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UseCaseWorkFlowCategory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UseCaseWorkFlowCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case UseCaseWorkFlowCategoryDataModel.DataColumns.UseCaseWorkFlowCategoryId:
					if (data.UseCaseWorkFlowCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseWorkFlowCategoryDataModel.DataColumns.UseCaseWorkFlowCategoryId, data.UseCaseWorkFlowCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseWorkFlowCategoryDataModel.DataColumns.UseCaseWorkFlowCategoryId);
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

		public static List<UseCaseWorkFlowCategoryDataModel> GetEntityDetails(UseCaseWorkFlowCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UseCaseWorkFlowCategorySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	UseCaseWorkFlowCategoryId                  = dataQuery.UseCaseWorkFlowCategoryId
				 ,	Name                    = dataQuery.Name
			};

			List<UseCaseWorkFlowCategoryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UseCaseWorkFlowCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<UseCaseWorkFlowCategoryDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(UseCaseWorkFlowCategoryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static UseCaseWorkFlowCategoryDataModel GetDetails(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(UseCaseWorkFlowCategoryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UseCaseWorkFlowCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UseCaseWorkFlowCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, UseCaseWorkFlowCategoryDataModel.DataColumns.UseCaseWorkFlowCategoryId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("UseCaseWorkFlowCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("UseCaseWorkFlowCategory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.UseCaseWorkFlowCategoryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   UseCaseWorkFlowCategoryId  = data.UseCaseWorkFlowCategoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new UseCaseWorkFlowCategoryDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UseCaseWorkFlowCategoryRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("UseCaseWorkFlowCategory.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UseCaseWorkFlowCategoryChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, UseCaseWorkFlowCategoryDataModel.DataColumns.UseCaseWorkFlowCategoryId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(UseCaseWorkFlowCategoryDataModel data, RequestProfile requestProfile)
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
