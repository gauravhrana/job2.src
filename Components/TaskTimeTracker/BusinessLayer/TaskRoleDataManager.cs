using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class TaskRoleDataManager : StandardDataManager
    {
        private static string DataStoreKey = "";

        static TaskRoleDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskRole");
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskRoleSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
                + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
            var oDT = new DBDataTable("TaskRole.GetList", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion GetList

        #region Search

        public static string ToSQLParameter(TaskRoleDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case TaskRoleDataModel.DataColumns.TaskRoleId:
                    if (data.TaskRoleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskRoleDataModel.DataColumns.TaskRoleId, data.TaskRoleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskRoleDataModel.DataColumns.TaskRoleId);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;
            }

            return returnValue;
        }

        public static DataTable Search(TaskRoleDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion Search

        #region GetDetails

        public static DataTable GetDetails(TaskRoleDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static List<TaskRoleDataModel> GetEntityDetails(TaskRoleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TaskRoleSearch ";

            var parameters =
            new
            {
                    AuditId                     = requestProfile.AuditId
                ,   ApplicationId               = requestProfile.ApplicationId
                ,   ApplicationMode             = requestProfile.ApplicationModeId
                ,   ReturnAuditInfo             = returnAuditInfo
                ,   TaskRoleId                  = dataQuery.TaskRoleId
                ,   Name                        = dataQuery.Name
                ,   Description                 = dataQuery.Description
            };

            List<TaskRoleDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<TaskRoleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<TaskRoleDataModel> GetEntityDetails(TaskRoleDataModel data, int auditId, int applicationModeId = 0)
        //{
        //    var sql = "EXEC dbo.TaskRoleSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //              ", " + ToSQLParameter(data, TaskRoleDataModel.DataColumns.TaskRoleId);

        //    var result = new List<TaskRoleDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new TaskRoleDataModel();

        //            dataItem.TaskRoleId = (int?)dbReader[TaskRoleDataModel.DataColumns.TaskRoleId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion GetDetails

        #region CreateOrUpdate

        private static string CreateOrUpdate(TaskRoleDataModel data, RequestProfile requestProfile, string action)
        {
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TaskRoleInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TaskRoleUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                         ", " + ToSQLParameter(data, TaskRoleDataModel.DataColumns.TaskRoleId) +
                         ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                         ", " + ToSQLParameter(data, TaskRoleDataModel.DataColumns.SortOrder);
            return sql;
        }

        #endregion CreateOrUpdate

        #region Create

        public static int Create(TaskRoleDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var taskRoleId = DBDML.RunScalarSQL("TaskRole.Insert", sql, DataStoreKey);
            return Convert.ToInt32(taskRoleId);
        }

        #endregion Create

        #region Update

        public static void Update(TaskRoleDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("taskRole.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Delete

        public static void Delete(TaskRoleDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TaskRoleDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                TaskRoleId = dataQuery.TaskRoleId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion Delete

        #region DoesExist

        public static DataTable DoesExist(TaskRoleDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new TaskRoleDataModel();
            doesExistRequest.Name = data.Name;

            return Search(doesExistRequest, requestProfile);
        }

        #endregion DoesExist

        #region GetChildren

        private static DataSet GetChildren(TaskRoleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskRoleChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TaskRoleDataModel.DataColumns.TaskRoleId);

            var oDT = new DBDataSet("TaskRole.GetChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region DeleteChildren

        public static DataSet DeleteChildren(TaskRoleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskRoleChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TaskRoleDataModel.DataColumns.TaskRoleId);

            var oDT = new DBDataSet("TaskRole.DeleteChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(TaskRoleDataModel data, RequestProfile requestProfile)
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
