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
    public partial class TaskDataManager : StandardDataManager
    {
        private static readonly string DataStoreKey = "";

        static TaskDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("Task");
        }

        #region GetList

        public static List<TaskDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskDataModel.Empty, requestProfile, 1);
        }

        #endregion GetList

        #region Search

        public static string ToSQLParameter(TaskDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case TaskDataModel.DataColumns.TaskId:
                    if (data.TaskId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskDataModel.DataColumns.TaskId, data.TaskId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskDataModel.DataColumns.TaskId);
                    }
                    break;
                case TaskDataModel.DataColumns.TaskTypeId:
                    if (data.TaskTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskDataModel.DataColumns.TaskTypeId, data.TaskTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskDataModel.DataColumns.TaskTypeId);
                    }
                    break;
                case TaskDataModel.DataColumns.TaskType:
                    if (data.TaskType != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskDataModel.DataColumns.TaskType, data.TaskType);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskDataModel.DataColumns.TaskType);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(TaskDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL  
            var sql = "EXEC dbo.TaskSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +                
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskId) +
                ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskTypeId);


            var oDT = new DBDataTable("Task.GetList", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion Search

        #region GetDetails

		public static TaskDataModel GetDetails(TaskDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            return list.FirstOrDefault();
        }

        public static List<TaskDataModel> GetEntityDetails(TaskDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TaskSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId              
                ,   TaskId               = dataQuery.TaskId
                ,   TaskTypeId           = dataQuery.TaskTypeId
                ,   Name                 = dataQuery.Name
                ,   Description          = dataQuery.Description
            };

            List<TaskDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                result = dataAccess.Connection.Query<TaskDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


            }

            return result;
        }

        //public static List<TaskDataModel> GetEntityDetails(TaskDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.TaskSearch " +
        //        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
        //        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
        //        ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskId) +
        //        ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskTypeId) +
        //        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);

        //    var result = new List<TaskDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new TaskDataModel();

        //            dataItem.TaskId = (int)dbReader[TaskDataModel.DataColumns.TaskId];
        //            dataItem.TaskTypeId = (int)dbReader[TaskDataModel.DataColumns.TaskTypeId];
        //            dataItem.TaskType = (string)dbReader[TaskDataModel.DataColumns.TaskType];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion GetDetails

        #region CreateOrUpdate
        private static string CreateOrUpdate(TaskDataModel data, RequestProfile requestProfile, string action)
        {
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TaskInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +                        
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TaskUpdate  " + "\r\n" +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);                        
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskId) +
                        ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskTypeId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;
        }

        #endregion CreateOrUpdate

        #region Create
        public static int Create(TaskDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var taskId = DBDML.RunScalarSQL("Task.Insert", sql, DataStoreKey);
            return Convert.ToInt32(taskId);
        }
        #endregion Create

        #region Update
        public static void Update(TaskDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("Task.Update", sql, DataStoreKey);
        }
        #endregion Update

        #region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ",@Seed = " + seed +
                      ",@Increment = " + increment;

            DBDML.RunSQL("Task.Renumber", sql, DataStoreKey);
        }
        #endregion Renumber

        #region Delete

        public static void Delete(TaskDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TaskDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                TaskId = dataQuery.TaskId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion Delete

        #region DoesExist

        public static DataTable DoesExist(TaskDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskSearch " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskId) +
                            ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

            var oDT = new DBDataTable("Task.DoesExist", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion DoesExist

        #region GetChildren

        private static DataSet GetChildren(TaskDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskId);

            var oDT = new DBDataSet("Task.GetChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region DeleteChildren

        public static DataSet DeleteChildren(TaskDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TaskDataModel.DataColumns.TaskId);

            var oDT = new DBDataSet("Task.DeleteChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(TaskDataModel data, RequestProfile requestProfile)
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
