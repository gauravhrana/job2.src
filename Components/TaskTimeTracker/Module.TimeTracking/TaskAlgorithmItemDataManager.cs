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
	public partial class TaskAlgorithmItemDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static TaskAlgorithmItemDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskAlgorithmItem");
		}

		#region GetList

        public static List<TaskAlgorithmItemDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskAlgorithmItemDataModel.Empty, requestProfile);
		}

		#endregion

		#region GetDetails

        public static TaskAlgorithmItemDataModel GetDetails(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<TaskAlgorithmItemDataModel> GetEntityDetails(TaskAlgorithmItemDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TaskAlgorithmItemSearch ";

			var parameters =
			new
			{
				    AuditId                 = requestProfile.AuditId
				,   ApplicationId           = requestProfile.ApplicationId
				,   TaskAlgorithmItemId     = dataQuery.TaskAlgorithmItemId
				,   TaskAlgorithmId         = dataQuery.TaskAlgorithmId
				,   ActivityId              = dataQuery.ActivityId
				,   ApplicationMode         = requestProfile.ApplicationModeId

			};


			List<TaskAlgorithmItemDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TaskAlgorithmItemDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

		#endregion GetDetails

		#region Create

		public static void Create(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("TaskAlgorithmItem.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("TaskAlgorithmItem.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TaskAlgorithmItemDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TaskAlgorithmItemDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TaskAlgorithmItemId = dataQuery.TaskAlgorithmItemId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(TaskAlgorithmItemDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId:
					if (data.TaskAlgorithmItemId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId, data.TaskAlgorithmItemId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId);

					}
					break;

				case TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId:
					if (data.TaskAlgorithmId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId, data.TaskAlgorithmId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId);

					}
					break;

				case TaskAlgorithmItemDataModel.DataColumns.ActivityId:
					if (data.ActivityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskAlgorithmItemDataModel.DataColumns.ActivityId, data.ActivityId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskAlgorithmItemDataModel.DataColumns.ActivityId);

					}
					break;

				case TaskAlgorithmItemDataModel.DataColumns.Activity:
					if (!string.IsNullOrEmpty(data.Activity))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskAlgorithmItemDataModel.DataColumns.Activity, data.Activity);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskAlgorithmItemDataModel.DataColumns.Activity);

					}
					break;

				case TaskAlgorithmItemDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskAlgorithmItemDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskAlgorithmItemDataModel.DataColumns.Description);
					}
					break;

				case TaskAlgorithmItemDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskAlgorithmItemDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskAlgorithmItemDataModel.DataColumns.SortOrder);
					}
					break;

			}
			return returnValue;
		}

		public static DataTable Search(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(TaskAlgorithmItemDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskAlgorithmItemInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskAlgorithmItemUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId) +
						", " + ToSQLParameter(data, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId) +
						", " + ToSQLParameter(data, TaskAlgorithmItemDataModel.DataColumns.ActivityId) +
						", " + ToSQLParameter(data, TaskAlgorithmItemDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, TaskAlgorithmItemDataModel.DataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TaskAlgorithmItemDataModel();
			doesExistRequest.TaskAlgorithmItemId = data.TaskAlgorithmItemId;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
		}

		#endregion

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskAlgorithmItemRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskAlgorithmItem.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region GetChildren

		private static DataSet GetChildren(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskAlgorithmItemChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskAlgorithmItemChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId);

			var oDT = new DBDataSet("Delete Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(TaskAlgorithmItemDataModel data, RequestProfile requestProfile)
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
