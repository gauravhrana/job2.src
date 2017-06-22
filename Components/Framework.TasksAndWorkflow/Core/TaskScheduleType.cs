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
	public partial class TaskScheduleType : StandardDataManager
	{
		private static string DataStoreKey = "";		

		static TaskScheduleType()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskScheduleType");			
		}

		#region GetList

		//#region GetList

		//public static DataTable GetList(int applicationId, int auditId)
		//{
		//	var sql = "EXEC dbo.TaskScheduleTypeList "
		//		+ " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, applicationId)
		//		+ "," + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

		//	var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, DataStoreKey);

		//	return oDT.DBTable;
		//}

		//#endregion GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskScheduleTypeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId)
				+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
			var oDT = new DBDataTable("TaskScheduleType.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(TaskScheduleTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId:
					if (data.TaskScheduleTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId, data.TaskScheduleTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId);
					}
					break;

				case TaskScheduleTypeDataModel.DataColumns.Active:
					if (data.Active != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskScheduleTypeDataModel.DataColumns.Active, data.Active);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskScheduleTypeDataModel.DataColumns.Active);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<TaskScheduleTypeDataModel> GetEntityDetails(TaskScheduleTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TaskScheduleTypeSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= requestProfile.ApplicationId
				,	TaskScheduleTypeId			= dataQuery.TaskScheduleTypeId
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ReturnAuditInfo				= returnAuditInfo
				,	Name						= dataQuery.Name
				,	Active						= dataQuery.Active		
				
			};

			List<TaskScheduleTypeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TaskScheduleTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}
		

		#endregion 

		#region CreateOrUpdate
		private static string CreateOrUpdate(TaskScheduleTypeDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskScheduleTypeInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskScheduleTypeUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, TaskScheduleTypeDataModel.DataColumns.Active);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			Framework.Components.DataAccess.DBDML.RunSQL("TaskScheduleType.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("Clent.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskScheduleTypeRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskScheduleType.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(TaskScheduleTypeDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TaskScheduleTypeDelete ";

			var parameters =
			new
			{
					AuditId				= requestProfile.AuditId
				,	TaskScheduleTypeId	= dataQuery.TaskScheduleTypeId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TaskScheduleTypeDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskScheduleTypeChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId);

			var oDT = new DBDataSet("TaskScheduleType.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskScheduleTypeChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId);

			var oDT = new DBDataSet("TaskScheduleType.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(TaskScheduleTypeDataModel data, RequestProfile requestProfile)
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
