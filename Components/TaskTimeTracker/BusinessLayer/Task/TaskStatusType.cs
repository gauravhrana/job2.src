using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
	public partial class TaskStatusTypeDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";
		
        static TaskStatusTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskStatusType");			
		}

		#region GetList

        public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskStatusTypeSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
                + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("TaskStatusType.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(TaskStatusTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId:
					if (data.TaskStatusTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId, data.TaskStatusTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(TaskStatusTypeDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL  
			var sql = "EXEC dbo.TaskStatusTypeSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId);


			var oDT = new DBDataTable("TaskStatusType.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion Search

		#region GetDetails

        public static DataTable GetDetails(TaskStatusTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static List<TaskStatusTypeDataModel> GetEntityDetails(TaskStatusTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TaskStatusTypeSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId
                ,   ReturnAuditInfo      = returnAuditInfo
                ,   TaskStatusTypeId     = dataQuery.TaskStatusTypeId
                ,   Name                 = dataQuery.Name
            };

            List<TaskStatusTypeDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                result = dataAccess.Connection.Query<TaskStatusTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

                
            }

            return result;
        }

        //public static List<TaskStatusTypeDataModel> GetEntityDetails(TaskStatusTypeDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.TaskStatusTypeSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //              ", " + ToSQLParameter(data, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId);

        //    var result = new List<TaskStatusTypeDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new TaskStatusTypeDataModel();

        //            dataItem.TaskStatusTypeId = (int)dbReader[TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

		#endregion GetDetails

		#region CreateOrUpdate
        private static string CreateOrUpdate(TaskStatusTypeDataModel data, RequestProfile requestProfile, string action)
		{
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskStatusTypeInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskStatusTypeUpdate  " + "\r\n" +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create
        public static int Create(TaskStatusTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var taskStatusTypeId = DBDML.RunScalarSQL("TaskStatusType.Insert", sql, DataStoreKey);
            return Convert.ToInt32(taskStatusTypeId);
        }

		#endregion Create

		#region Update
        public static void Update(TaskStatusTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("TaskStatusType.Update", sql, DataStoreKey);
        }

		#endregion Update

		#region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskStatusTypeRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskStatusType.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

        public static void Delete(TaskStatusTypeDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TaskStatusTypeDelete ";

            var parameters =
            new
            {
                    AuditId          = requestProfile.AuditId
                ,   TaskStatusTypeId = dataQuery.TaskStatusTypeId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                
            }
        }

		#endregion Delete

		#region DoesExist

        public static DataTable DoesExist(TaskStatusTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskStatusTypeSearch " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							", " + ToSQLParameter(data, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId) +
							", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("TaskStatusType.DoesExist", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion DoesExist

		#region GetChildren

        private static DataSet GetChildren(TaskStatusTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskStatusTypeChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId);

			var oDT = new DBDataSet("TaskStatusType.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

        public static DataSet DeleteChildren(TaskStatusTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskStatusTypeChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId);

			var oDT = new DBDataSet("TaskStatusType.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

        public static bool IsDeletable(TaskStatusTypeDataModel data, RequestProfile requestProfile)
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
