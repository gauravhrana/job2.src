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
	public partial class DeliverableArtifactDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static DeliverableArtifactDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DeliverableArtifact");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(DeliverableArtifactDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId:
					if (data.DeliverableArtifactId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId, data.DeliverableArtifactId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);
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

		public static List<DeliverableArtifactDataModel> GetEntityDetails(DeliverableArtifactDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.DeliverableArtifactSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	DeliverableArtifactId                  = dataQuery.DeliverableArtifactId
				 ,	Name                    = dataQuery.Name
			};

			List<DeliverableArtifactDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DeliverableArtifactDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<DeliverableArtifactDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(DeliverableArtifactDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(DeliverableArtifactDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static DeliverableArtifactDataModel GetDetails(DeliverableArtifactDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(DeliverableArtifactDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.DeliverableArtifactInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.DeliverableArtifactUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(DeliverableArtifactDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("DeliverableArtifact.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(DeliverableArtifactDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("DeliverableArtifact.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DeliverableArtifactDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.DeliverableArtifactDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   DeliverableArtifactId  = data.DeliverableArtifactId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(DeliverableArtifactDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new DeliverableArtifactDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DeliverableArtifactRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("DeliverableArtifact.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(DeliverableArtifactDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DeliverableArtifactChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(DeliverableArtifactDataModel data, RequestProfile requestProfile)
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
