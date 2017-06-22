using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;

namespace TaskTimeTracker.Components.BusinessLayer
{
	public partial class MilestoneFeatureStateDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MilestoneFeatureStateDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MilestoneFeatureState");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MilestoneFeatureStateDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId:
					if (data.MilestoneFeatureStateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId, data.MilestoneFeatureStateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId);
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

		public static List<MilestoneFeatureStateDataModel> GetEntityDetails(MilestoneFeatureStateDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MilestoneFeatureStateSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	MilestoneFeatureStateId                  = dataQuery.MilestoneFeatureStateId
				 ,	Name                    = dataQuery.Name
			};

			List<MilestoneFeatureStateDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MilestoneFeatureStateDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MilestoneFeatureStateDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MilestoneFeatureStateDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static MilestoneFeatureStateDataModel GetDetails(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(MilestoneFeatureStateDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.MilestoneFeatureStateInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MilestoneFeatureStateUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MilestoneFeatureState.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MilestoneFeatureState.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MilestoneFeatureStateDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MilestoneFeatureStateId  = data.MilestoneFeatureStateId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MilestoneFeatureStateDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.MilestoneFeatureStateRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("MilestoneFeatureState.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.MilestoneFeatureStateChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(MilestoneFeatureStateDataModel data, RequestProfile requestProfile)
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
