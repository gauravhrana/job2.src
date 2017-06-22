using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class DailyTaskItemQueueStatusDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static DailyTaskItemQueueStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DailyTaskItemQueueStatus");
		}

		#region GetList

        public static List<DailyTaskItemQueueStatusDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(DailyTaskItemQueueStatusDataModel.Empty, requestProfile);
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(DailyTaskItemQueueStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId:
					if (data.DailyTaskItemQueueStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId, data.DailyTaskItemQueueStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

        public static DailyTaskItemQueueStatusDataModel GetDetails(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<DailyTaskItemQueueStatusDataModel> GetEntityDetails(DailyTaskItemQueueStatusDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.DailyTaskItemQueueStatusSearch ";

			var parameters =
			new
			{
				    AuditId                     = requestProfile.AuditId
				,   DailyTaskItemQueueStatusId  = dataQuery.DailyTaskItemQueueStatusId
				,   Name                        = dataQuery.Name
				,   ApplicationId               = requestProfile.ApplicationId
				,   ApplicationMode             = requestProfile.ApplicationModeId
			};

			List<DailyTaskItemQueueStatusDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DailyTaskItemQueueStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion GetDetails

		#region ToList

		static private List<DailyTaskItemQueueStatusDataModel> ToList(DataTable dt)
		{
			var list = new List<DailyTaskItemQueueStatusDataModel>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var dataItem = new DailyTaskItemQueueStatusDataModel();

					dataItem.DailyTaskItemQueueStatusId = (int?)dr[DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId];
					dataItem.DateCreated = (DateTime)dr[DailyTaskItemQueueStatusDataModel.DataColumns.DateCreated];
					dataItem.DateModified = (DateTime)dr[DailyTaskItemQueueStatusDataModel.DataColumns.DateModified];
					dataItem.CreatedByAuditId = (int?)dr[BaseDataModel.BaseDataColumns.CreatedByAuditId];
					dataItem.ModifiedByAuditId = (int?)dr[BaseDataModel.BaseDataColumns.ModifiedByAuditId];

					SetStandardInfo(dataItem, dr);

					list.Add(dataItem);
				}
			}
			return list;
		}

		#endregion

		#region GetEntitySearch

		static public List<DailyTaskItemQueueStatusDataModel> GetEntitySearch(DailyTaskItemQueueStatusDataModel obj, RequestProfile requestProfile)
		{
			var dt = Search(obj, requestProfile);

			var list = ToList(dt);

			return list;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.DailyTaskItemQueueStatusInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.DailyTaskItemQueueStatusUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			DBDML.RunSQL("DailyTaskItemQueueStatus.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("DailyTaskItemQueueStatus.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DailyTaskItemQueueStatusRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("DailyTaskItemQueueStatus.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(DailyTaskItemQueueStatusDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.DailyTaskItemQueueStatusDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				DailyTaskItemQueueStatusId = dataQuery.DailyTaskItemQueueStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new DailyTaskItemQueueStatusDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DailyTaskItemQueueStatusChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId);

			var oDT = new DBDataSet("DailyTaskItemQueueStatus.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DailyTaskItemQueueStatusChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId);

			var oDT = new DBDataSet("DailyTaskItemQueueStatus.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(DailyTaskItemQueueStatusDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;

			var ds = GetChildren(data, requestProfile);

			if (ds != null && ds.Tables.Count > 0)
			{
				if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
				{
					isDeletable = false;
				}
			}

			return isDeletable;
		}

		#endregion
	}
}