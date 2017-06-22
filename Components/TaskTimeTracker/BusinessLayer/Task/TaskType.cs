using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
	public partial class TaskTypeDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";
		
        static TaskTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskType");			
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
            var sql = "EXEC dbo.TaskTypeSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)+
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("TaskType.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(TaskTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskTypeDataModel.DataColumns.TaskTypeId:
					if (data.TaskTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskTypeDataModel.DataColumns.TaskTypeId, data.TaskTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskTypeDataModel.DataColumns.TaskTypeId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(TaskTypeDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL  
			var sql = "EXEC dbo.TaskTypeSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, TaskTypeDataModel.DataColumns.TaskTypeId);


			var oDT = new DBDataTable("TaskType.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion Search

        #region GetDetails

        public static DataTable GetDetails(TaskTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static List<TaskTypeDataModel> GetEntityDetails(TaskTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TaskTypeSearch ";

            var parameters =
            new
            {
                    AuditId             = requestProfile.AuditId
                ,   ApplicationId       = requestProfile.ApplicationId
                ,   ReturnAuditInfo     = returnAuditInfo
                ,   TaskTypeId          = dataQuery.TaskTypeId
                ,   Name                = dataQuery.Name

            };

            List<TaskTypeDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                result = dataAccess.Connection.Query<TaskTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

                
            }

            return result;
        }

        //public static List<TaskTypeDataModel> GetEntityDetails(TaskTypeDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.TaskTypeSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //              ", " + ToSQLParameter(data, TaskTypeDataModel.DataColumns.TaskTypeId);

        //    var result = new List<TaskTypeDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new TaskTypeDataModel();

        //            dataItem.TaskTypeId = (int)dbReader[TaskTypeDataModel.DataColumns.TaskTypeId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
		//}

		#endregion GetDetails

		#region CreateOrUpdate
        private static string CreateOrUpdate(TaskTypeDataModel data, RequestProfile requestProfile, string action)
		{
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskTypeInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskTypeUpdate  " + "\r\n" +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskTypeDataModel.DataColumns.TaskTypeId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create
        public static int Create(TaskTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var taskTypeId = DBDML.RunScalarSQL("TaskType.Insert", sql, DataStoreKey);
            return Convert.ToInt32(taskTypeId);
        }
		#endregion Create

		#region Update
        public static void Update(TaskTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("TaskType.Update", sql, DataStoreKey);
        }
		#endregion Update

		#region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskTypeRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskType.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

        public static void Delete(TaskTypeDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TaskTypeDelete ";

            var parameters =
            new
            {
                    AuditId      = requestProfile.AuditId
                ,   TaskTypeId   = dataQuery.TaskTypeId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                
            }
        }

		#endregion Delete

		#region DoesExist

        public static DataTable DoesExist(TaskTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskTypeSearch " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							", " + ToSQLParameter(data, TaskTypeDataModel.DataColumns.TaskTypeId) +
							", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("TaskType.DoesExist", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion DoesExist

		#region GetChildren

        private static DataSet GetChildren(TaskTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskTypeChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskTypeDataModel.DataColumns.TaskTypeId);

			var oDT = new DBDataSet("TaskType.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

        public static DataSet DeleteChildren(TaskTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskTypeChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskTypeDataModel.DataColumns.TaskTypeId);

			var oDT = new DBDataSet("TaskType.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

        public static bool IsDeletable(TaskTypeDataModel data, RequestProfile requestProfile)
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
