using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class FunctionalityActiveStatusDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FunctionalityActiveStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityActiveStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FunctionalityActiveStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId:
					if (data.FunctionalityActiveStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);
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

		public static List<FunctionalityActiveStatusDataModel> GetEntityDetails(FunctionalityActiveStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityActiveStatusSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FunctionalityActiveStatusId                  = dataQuery.FunctionalityActiveStatusId
				 ,	Name                    = dataQuery.Name
			};

			List<FunctionalityActiveStatusDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityActiveStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FunctionalityActiveStatusDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FunctionalityActiveStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static FunctionalityActiveStatusDataModel GetDetails(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(FunctionalityActiveStatusDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityActiveStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityActiveStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FunctionalityActiveStatus.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FunctionalityActiveStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FunctionalityActiveStatusDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FunctionalityActiveStatusId  = data.FunctionalityActiveStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityActiveStatusDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityActiveStatusRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("FunctionalityActiveStatus.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityActiveStatusChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
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
