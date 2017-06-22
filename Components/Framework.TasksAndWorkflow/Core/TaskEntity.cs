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
	public partial class TaskEntity : StandardDataManager
	{
		private static string DataStoreKey = "";		

		static TaskEntity()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskEntity");			
		}

		#region GetList

		#region GetList

		//public static DataTable GetList(RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.TaskEntityList "
		//		+ " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId)
		//		+ "," + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

		//	var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, DataStoreKey);

		//	return oDT.DBTable;
		//}

		#endregion GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntitySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
				+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
			var oDT = new DBDataTable("TaskEntity.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		public static List<TaskEntityDataModel> GetTaskEntityList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntitySearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<TaskEntityDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new TaskEntityDataModel();

					dataItem.TaskEntityId = (int)dbReader[TaskEntityDataModel.DataColumns.TaskEntityId];

					SetStandardInfo(dataItem, dbReader);

					result.Add(dataItem);
				}

			}

			return result;
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(TaskEntityDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskEntityDataModel.DataColumns.TaskEntityId:
					if (data.TaskEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskEntityDataModel.DataColumns.TaskEntityId, data.TaskEntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskEntityDataModel.DataColumns.TaskEntityId);
					}
					break;

				case TaskEntityDataModel.DataColumns.Active:
					if (data.Active != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskEntityDataModel.DataColumns.Active, data.Active);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskEntityDataModel.DataColumns.Active);
					}
					break;

				case TaskEntityDataModel.DataColumns.TaskEntityTypeId:
                    if (data.TaskEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskEntityDataModel.DataColumns.TaskEntityTypeId, data.TaskEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskEntityDataModel.DataColumns.TaskEntityTypeId);
					}
					break;

				case TaskEntityDataModel.DataColumns.TaskEntityType:
                    if (data.TaskEntityType != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskEntityDataModel.DataColumns.TaskEntityType, data.TaskEntityType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskEntityDataModel.DataColumns.TaskEntityType);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(TaskEntityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(TaskEntityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion


		#region GetEntitySearch

		public static List<TaskEntityDataModel> GetEntityDetails(TaskEntityDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TaskEntitySearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= dataQuery.ApplicationId
				,	TaskEntityId				= dataQuery.TaskEntityId
				,	Name						= dataQuery.Name
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	Active						= dataQuery.Active
				,	ReturnAuditInfo				= returnAuditInfo

			};

			List<TaskEntityDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TaskEntityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion 

		#region CreateOrUpdate
		private static string CreateOrUpdate(TaskEntityDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskEntityInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskEntityUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskEntityDataModel.DataColumns.TaskEntityId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, TaskEntityDataModel.DataColumns.Active) +
						", " + ToSQLParameter(data, TaskEntityDataModel.DataColumns.TaskEntityTypeId);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(TaskEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			Framework.Components.DataAccess.DBDML.RunSQL("TaskEntity.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(TaskEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("TaskEntity.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskEntity.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(TaskEntityDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TaskEntityDelete ";

			var parameters =
			new
			{
					AuditId			= requestProfile.AuditId
				,	TaskEntityId	= dataQuery.TaskEntityId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(TaskEntityDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TaskEntityDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(TaskEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskEntityDataModel.DataColumns.TaskEntityId);

			var oDT = new DBDataSet("TaskEntity.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(TaskEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskEntityChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskEntityDataModel.DataColumns.TaskEntityId);

			var oDT = new DBDataSet("TaskEntity.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(TaskEntityDataModel data, RequestProfile requestProfile)
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
