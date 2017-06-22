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
	public partial class WorkTicketDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static WorkTicketDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("WorkTicket");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(WorkTicketDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case WorkTicketDataModel.DataColumns.WorkTicketId:
					if (data.WorkTicketId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, WorkTicketDataModel.DataColumns.WorkTicketId, data.WorkTicketId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, WorkTicketDataModel.DataColumns.WorkTicketId);
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

		public static List<WorkTicketDataModel> GetEntityDetails(WorkTicketDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.WorkTicketSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	WorkTicketId                  = dataQuery.WorkTicketId
				 ,	Name                    = dataQuery.Name
			};

			List<WorkTicketDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<WorkTicketDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<WorkTicketDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(WorkTicketDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(WorkTicketDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static WorkTicketDataModel GetDetails(WorkTicketDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(WorkTicketDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.WorkTicketInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.WorkTicketUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, WorkTicketDataModel.DataColumns.WorkTicketId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(WorkTicketDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("WorkTicket.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(WorkTicketDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("WorkTicket.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(WorkTicketDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.WorkTicketDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   WorkTicketId  = data.WorkTicketId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(WorkTicketDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new WorkTicketDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.WorkTicketRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("WorkTicket.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(WorkTicketDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.WorkTicketChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, WorkTicketDataModel.DataColumns.WorkTicketId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(WorkTicketDataModel data, RequestProfile requestProfile)
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
