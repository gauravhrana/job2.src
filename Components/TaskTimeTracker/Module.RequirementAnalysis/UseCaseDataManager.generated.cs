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
	public partial class UseCaseDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static UseCaseDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UseCase");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UseCaseDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case UseCaseDataModel.DataColumns.UseCaseId:
					if (data.UseCaseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseDataModel.DataColumns.UseCaseId, data.UseCaseId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseDataModel.DataColumns.UseCaseId);
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

		public static List<UseCaseDataModel> GetEntityDetails(UseCaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UseCaseSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	UseCaseId                  = dataQuery.UseCaseId
				 ,	Name                    = dataQuery.Name
			};

			List<UseCaseDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UseCaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<UseCaseDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(UseCaseDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(UseCaseDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static UseCaseDataModel GetDetails(UseCaseDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(UseCaseDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UseCaseInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UseCaseUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, UseCaseDataModel.DataColumns.UseCaseId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(UseCaseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("UseCase.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(UseCaseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("UseCase.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(UseCaseDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.UseCaseDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   UseCaseId  = data.UseCaseId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(UseCaseDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new UseCaseDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UseCaseRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("UseCase.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(UseCaseDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UseCaseChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, UseCaseDataModel.DataColumns.UseCaseId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(UseCaseDataModel data, RequestProfile requestProfile)
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
