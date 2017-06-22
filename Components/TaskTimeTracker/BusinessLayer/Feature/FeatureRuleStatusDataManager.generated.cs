using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
	public partial class FeatureRuleStatusDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FeatureRuleStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FeatureRuleStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FeatureRuleStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId:
					if (data.FeatureRuleStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId, data.FeatureRuleStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId);
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

		public static List<FeatureRuleStatusDataModel> GetEntityDetails(FeatureRuleStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FeatureRuleStatusSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FeatureRuleStatusId                  = dataQuery.FeatureRuleStatusId
				 ,	Name                    = dataQuery.Name
			};

			List<FeatureRuleStatusDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FeatureRuleStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FeatureRuleStatusDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FeatureRuleStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static FeatureRuleStatusDataModel GetDetails(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(FeatureRuleStatusDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FeatureRuleStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FeatureRuleStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FeatureRuleStatus.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FeatureRuleStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FeatureRuleStatusDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FeatureRuleStatusId  = data.FeatureRuleStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FeatureRuleStatusDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FeatureRuleStatusRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("FeatureRuleStatus.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FeatureRuleStatusChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FeatureRuleStatusDataModel data, RequestProfile requestProfile)
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
