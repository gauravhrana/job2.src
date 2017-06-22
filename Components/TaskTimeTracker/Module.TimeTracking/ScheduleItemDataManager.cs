using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class ScheduleItemDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ScheduleItemDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ScheduleItem");
		}

		#region GetList

        public static List<ScheduleItemDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ScheduleItemDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Get By Schedule

		public static DataTable GetBySchedule(int scheduleId, RequestProfile requestProfile)
		{
			var sql = "EXEC ScheduleItemSearch @ScheduleId       =" + scheduleId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}


		#endregion

		#region GetDetails

		public static string ToSQLParameter(ScheduleItemDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ScheduleItemDataModel.DataColumns.ScheduleItemId:
					if (data.ScheduleItemId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleItemDataModel.DataColumns.ScheduleItemId, data.ScheduleItemId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleItemDataModel.DataColumns.ScheduleItemId);
					}
					break;

				case ScheduleItemDataModel.DataColumns.ScheduleId:
					if (data.ScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleItemDataModel.DataColumns.ScheduleId, data.ScheduleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleItemDataModel.DataColumns.ScheduleId);
					}
					break;

				case ScheduleItemDataModel.DataColumns.TaskFormulationId:
					if (data.TaskFormulationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleItemDataModel.DataColumns.TaskFormulationId, data.TaskFormulationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleItemDataModel.DataColumns.TaskFormulationId);
					}
					break;

				case ScheduleItemDataModel.DataColumns.TotalTimeSpent:
					if (data.TotalTimeSpent != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleItemDataModel.DataColumns.TotalTimeSpent, data.TotalTimeSpent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleItemDataModel.DataColumns.TotalTimeSpent);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static ScheduleItemDataModel GetDetails(ScheduleItemDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ScheduleItemDataModel> GetEntityDetails(ScheduleItemDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ScheduleItemSearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,   ApplicationId       = requestProfile.ApplicationId
				,   ScheduleItemId      = dataQuery.ScheduleItemId
				,   ReturnAuditInfo     = returnAuditInfo
				,   ApplicationMode     = requestProfile.ApplicationModeId
				,   ScheduleId          = dataQuery.ScheduleId
				,   TaskFormulationId   = dataQuery.TaskFormulationId
			};

			List<ScheduleItemDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ScheduleItemDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(ScheduleItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("ScheduleItem.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ScheduleItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ScheduleItem.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ScheduleItemDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ScheduleItemDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ScheduleItemId = dataQuery.ScheduleItemId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}

		}

		#endregion

		#region Search

		public static DataTable Search(ScheduleItemDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(ScheduleItemDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ScheduleItemInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ScheduleItemUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ScheduleItemDataModel.DataColumns.ScheduleItemId) +
				", " + ToSQLParameter(data, ScheduleItemDataModel.DataColumns.ScheduleId) +
				", " + ToSQLParameter(data, ScheduleItemDataModel.DataColumns.TotalTimeSpent) +
				", " + ToSQLParameter(data, ScheduleItemDataModel.DataColumns.TaskFormulationId);

			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ScheduleItemDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ScheduleItemDataModel();
			doesExistRequest.ScheduleId = data.ScheduleId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

	}
}
