using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.TasksAndWorkFlow;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.TasksAndWorkflow
{
	public partial class TaskEntityType : StandardDataManager
	{
		private static string DataStoreKey = "";

		static TaskEntityType()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskEntityType");			
		}

		#region GetList

		#region GetList

		public static DataTable GetList(int applicationId, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityTypeList "
				+ " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId)
				+ "," + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityTypeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId)
				+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
			var oDT = new DBDataTable("TaskEntityType.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(TaskEntityTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId:
					if (data.TaskEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId, data.TaskEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId);
					}
					break;

				case TaskEntityTypeDataModel.DataColumns.Active:
					if (data.Active != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskEntityTypeDataModel.DataColumns.Active, data.Active);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskEntityTypeDataModel.DataColumns.Active);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(TaskEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(TaskEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;

		}

		#endregion

		#region GetEntitySearch

		public static List<TaskEntityTypeDataModel> GetEntityDetails(TaskEntityTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TaskEntityTypeSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= requestProfile.ApplicationId
				,	TaskEntityTypeId			= dataQuery.TaskEntityTypeId
				,	Name						= dataQuery.Name
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	Active						= dataQuery.Active
				,	ReturnAuditInfo				= returnAuditInfo

			};

			List<TaskEntityTypeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TaskEntityTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}
					

		#endregion 

		#region CreateOrUpdate
		private static string CreateOrUpdate(TaskEntityTypeDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskEntityTypeInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskEntityTypeUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, TaskEntityTypeDataModel.DataColumns.Active);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(TaskEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			Framework.Components.DataAccess.DBDML.RunSQL("TaskEntityType.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(TaskEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("Clent.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityTypeRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskEntityType.Renumber", sql, DataStoreKey);
		}

		#endregion Renumber

		#region Delete

		public static void Delete(TaskEntityTypeDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TaskEntityTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	TaskEntityTypeId = dataQuery.TaskEntityTypeId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(TaskEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TaskEntityTypeDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(TaskEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityTypeChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId);

			var oDT = new DBDataSet("TaskEntityType.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(TaskEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityTypeChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId);

			var oDT = new DBDataSet("TaskEntityType.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(TaskEntityTypeDataModel data, RequestProfile requestProfile)
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
