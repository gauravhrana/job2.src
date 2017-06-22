using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class TaskXDeliverableArtifactDataManager : BaseDataManager
    {
        static string DataStoreKey = "";

        static TaskXDeliverableArtifactDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskXDeliverableArtifact");
        }

        public static string ToSQLParameter(TaskXDeliverableArtifactDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId:
                    if (data.TaskXDeliverableArtifactId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId, data.TaskXDeliverableArtifactId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId);
                    }
                    break;

                case TaskXDeliverableArtifactDataModel.DataColumns.TaskId:
                    if (data.TaskId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXDeliverableArtifactDataModel.DataColumns.TaskId, data.TaskId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXDeliverableArtifactDataModel.DataColumns.TaskId);
                    }
                    break;

                case TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId:
                    if (data.DeliverableArtifactId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId, data.DeliverableArtifactId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);
                    }
                    break;

                case TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId:
                    if (data.DeliverableArtifactStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId, data.DeliverableArtifactStatusId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId);
                    }
                    break;

                case TaskXDeliverableArtifactDataModel.DataColumns.Task:
                    if (!string.IsNullOrEmpty(data.Task))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXDeliverableArtifactDataModel.DataColumns.Task, data.Task);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXDeliverableArtifactDataModel.DataColumns.Task);
                    }
                    break;

                case TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact:
                    if (!string.IsNullOrEmpty(data.DeliverableArtifact))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact, data.DeliverableArtifact);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact);
                    }
                    break;

                case TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatus:
                    if (!string.IsNullOrEmpty(data.DeliverableArtifactStatus))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact, data.DeliverableArtifactStatus);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatus);
                    }
                    break;
            }

            return returnValue;
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXDeliverableArtifactSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new DBDataTable("TaskXDeliverableArtifact.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXDeliverableArtifactSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId);

            var oDT = new DBDataTable("TaskXDeliverableArtifact.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<TaskXDeliverableArtifactDataModel> GetEntityDetails(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXDeliverableArtifactSearch " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                     ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId);

            var result = new List<TaskXDeliverableArtifactDataModel>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new TaskXDeliverableArtifactDataModel();

                    dataItem.TaskXDeliverableArtifactId = (int)dbReader[TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId];
                    dataItem.TaskId = (int)dbReader[TaskXDeliverableArtifactDataModel.DataColumns.TaskId];
                    dataItem.DeliverableArtifactId = (int)dbReader[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId];
                    dataItem.DeliverableArtifactStatusId = (int)dbReader[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId];
                    dataItem.Task = (string)dbReader[TaskXDeliverableArtifactDataModel.DataColumns.Task];
                    dataItem.DeliverableArtifact = (string)dbReader[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact];
                    dataItem.DeliverableArtifactStatus = (string)dbReader[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatus];

                    SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }


        #endregion

        #region Create

        public static void Create(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DBDML.RunSQL("TaskXDeliverableArtifact.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("TaskXDeliverableArtifact.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXDeliverableArtifactDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
               ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

            DBDML.RunSQL("TaskXDeliverableArtifact.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.TaskXDeliverableArtifactSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
               ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

            var oDT = new DBDataTable("TaskXDeliverableArtifact.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TaskXDeliverableArtifactInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TaskXDeliverableArtifactUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(TaskXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXDeliverableArtifactSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId);

            var oDT = new DBDataTable("TaskXDeliverableArtifact.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create By Task

        public static void Create(int TaskId, int[] txdaids, RequestProfile requestProfile)
        {
            foreach (int txdaid in txdaids)
            {
                var sql = "EXEC TaskXDeliverableArtifactInsert " +
                          "@TaskId=" + TaskId + ", " +
                          "@DeliverableArtifactId=" + txdaid + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;

                DBDML.RunSQL("TaskXDeliverableArtifact_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Delete By Task

        public static void DeleteByTask(int TaskId, int auditId)
        {
            var sql = "EXEC TaskXDeliverableArtifactDelete @TaskId=" + TaskId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Get By Task

        public static DataTable GetByTask(int TaskId, int auditId)
        {
            var sql = "EXEC TaskXDeliverableArtifactSearch @TaskId=" + TaskId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion
    }
}
