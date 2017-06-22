using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class DailyTaskItemQueueDataManager : StandardDataManager
	{
        static readonly string DataStoreKey = "";

		static DailyTaskItemQueueDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DailyTaskItemQueue");
		}

		#region GetList

        public static List<DailyTaskItemQueueDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(DailyTaskItemQueueDataModel.Empty, requestProfile);
		}

		#endregion GetList

		#region ToSQLParameter

		public static string ToSQLParameter(DailyTaskItemQueueDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueId:
					if (data.DailyTaskItemQueueId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueId, data.DailyTaskItemQueueId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueId);
					}
					break;

				case DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatusId:
					if (data.DailyTaskItemQueueStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatusId, data.DailyTaskItemQueueStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatusId);
					}
					break;

				case DailyTaskItemQueueDataModel.DataColumns.AssignedBy:
					if (data.AssignedBy != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DailyTaskItemQueueDataModel.DataColumns.AssignedBy, data.AssignedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.AssignedBy);
					}
					break;

				case DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatus:
					if (data.DailyTaskItemQueueStatus != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatus, data.DailyTaskItemQueueStatus);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatus);
					}
					break;

				case DailyTaskItemQueueDataModel.DataColumns.AssignedTo:
					if (data.AssignedTo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DailyTaskItemQueueDataModel.DataColumns.AssignedTo, data.AssignedTo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.AssignedTo);
					}
					break;

				case DailyTaskItemQueueDataModel.DataColumns.BusinessDate:
					if (data.BusinessDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DailyTaskItemQueueDataModel.DataColumns.BusinessDate, data.BusinessDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.BusinessDate);
					}
					break;

				case DailyTaskItemQueueDataModel.DataColumns.BusinessDateMin:
					if (data.BusinessDateMin != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DailyTaskItemQueueDataModel.DataColumns.BusinessDateMin, data.BusinessDateMin);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.BusinessDateMin);
					}
					break;

				case DailyTaskItemQueueDataModel.DataColumns.BusinessDateMax:
					if (data.BusinessDateMax != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DailyTaskItemQueueDataModel.DataColumns.BusinessDateMax, data.BusinessDateMax);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DailyTaskItemQueueDataModel.DataColumns.BusinessDateMax);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#endregion

		#region Search

		public static DataTable Search(DailyTaskItemQueueDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;

		}

		#endregion Search

		#region GetDetails

        public static DailyTaskItemQueueDataModel GetDetails(DailyTaskItemQueueDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<DailyTaskItemQueueDataModel> GetEntityDetails(DailyTaskItemQueueDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.DailyTaskItemQueueSearch ";

			var parameters =
			new
			{
				    AuditId                     = requestProfile.AuditId
				,   ApplicationId               = requestProfile.ApplicationId
				,   ApplicationMode             = requestProfile.ApplicationModeId
				,   DailyTaskItemQueueId        = dataQuery.DailyTaskItemQueueId
				,   DailyTaskItemQueueStatusId  = dataQuery.DailyTaskItemQueueStatusId
				,   BusinessDateMin             = dataQuery.BusinessDateMin
				,   BusinessDateMax             = dataQuery.BusinessDateMax
				,   Description                 = dataQuery.Description
				,   SortOrder                   = dataQuery.SortOrder
				,   Name                        = dataQuery.Name
			};

			List<DailyTaskItemQueueDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DailyTaskItemQueueDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion GetDetails

		#region ToList

		static private List<DailyTaskItemQueueDataModel> ToList(DataTable dt)
		{
			var list = new List<DailyTaskItemQueueDataModel>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var dataItem = new DailyTaskItemQueueDataModel();

					dataItem.DailyTaskItemQueueId = (int?)dr[DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueId];
					dataItem.DailyTaskItemQueueStatusId = (int?)dr[DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatusId];
					dataItem.DailyTaskItemQueueStatus = (string)dr[DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatus];
					dataItem.BusinessDate = (DateTime?)dr[DailyTaskItemQueueDataModel.DataColumns.BusinessDate];
					dataItem.AssignedBy = (string)dr[DailyTaskItemQueueDataModel.DataColumns.AssignedBy];
					dataItem.AssignedTo = (string)dr[DailyTaskItemQueueDataModel.DataColumns.AssignedTo];

					SetStandardInfo(dataItem, dr);

					list.Add(dataItem);
				}
			}
			return list;
		}

		#endregion

		#region GetEntitySearch

		static public List<DailyTaskItemQueueDataModel> GetEntitySearch(DailyTaskItemQueueDataModel obj, RequestProfile requestProfile)
		{
			var dt = Search(obj, requestProfile);

			var list = ToList(dt);

			return list;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(DailyTaskItemQueueDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.DailyTaskItemQueueInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.DailyTaskItemQueueUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueId) +
						", " + ToSQLParameter(data, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueStatusId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, DailyTaskItemQueueDataModel.DataColumns.BusinessDate) +
						", " + ToSQLParameter(data, DailyTaskItemQueueDataModel.DataColumns.AssignedTo) +
						", " + ToSQLParameter(data, DailyTaskItemQueueDataModel.DataColumns.AssignedBy) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(DailyTaskItemQueueDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var Id = DBDML.RunScalarSQL("DailyTaskItemQueue.Insert", sql, DataStoreKey);
			return Convert.ToInt32(Id);
		}

		#endregion Create

		#region Update

		public static void Update(DailyTaskItemQueueDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("DailyTaskItemQueue.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DailyTaskItemQueueRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("DailyTaskItemQueue.Renumber", sql, DataStoreKey);
		}

		#endregion Renumber

		#region Delete

		public static void Delete(DailyTaskItemQueueDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.DailyTaskItemQueueDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				DailyTaskItemQueueId = dataQuery.DailyTaskItemQueueId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(DailyTaskItemQueueDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new DailyTaskItemQueueDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(DailyTaskItemQueueDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DailyTaskItemQueueChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DailyTaskItemQueueDataModel.DataColumns.DailyTaskItemQueueId);

			var oDT = new DBDataSet("DailyTaskItemQueue.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(DailyTaskItemQueueDataModel data, RequestProfile requestProfile)
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
