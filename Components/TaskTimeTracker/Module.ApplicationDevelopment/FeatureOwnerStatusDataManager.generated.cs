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
	public partial class FeatureOwnerStatusDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FeatureOwnerStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FeatureOwnerStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FeatureOwnerStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId:
					if (data.FeatureOwnerStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId, data.FeatureOwnerStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId);
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

		public static List<FeatureOwnerStatusDataModel> GetEntityDetails(FeatureOwnerStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FeatureOwnerStatusSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FeatureOwnerStatusId                  = dataQuery.FeatureOwnerStatusId
				 ,	Name                    = dataQuery.Name
			};

			List<FeatureOwnerStatusDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FeatureOwnerStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FeatureOwnerStatusDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FeatureOwnerStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static FeatureOwnerStatusDataModel GetDetails(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(FeatureOwnerStatusDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FeatureOwnerStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FeatureOwnerStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FeatureOwnerStatus.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FeatureOwnerStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FeatureOwnerStatusDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FeatureOwnerStatusId  = data.FeatureOwnerStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FeatureOwnerStatusDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FeatureOwnerStatusRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("FeatureOwnerStatus.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FeatureOwnerStatusChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FeatureOwnerStatusDataModel data, RequestProfile requestProfile)
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
