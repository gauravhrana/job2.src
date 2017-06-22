using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.Competency;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.Competency
{
    public partial class TaskXCompetencyDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static TaskXCompetencyDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskXCompetency");
        }

        public static string ToSQLParameter(TaskXCompetencyDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId:
                    if (data.TaskXCompetencyId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId, data.TaskXCompetencyId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId);
                    }
                    break;

                case TaskXCompetencyDataModel.DataColumns.TaskId:
                    if (data.TaskId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXCompetencyDataModel.DataColumns.TaskId, data.TaskId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXCompetencyDataModel.DataColumns.TaskId);
                    }
                    break;

                case TaskXCompetencyDataModel.DataColumns.CompetencyId:
                    if (data.CompetencyId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskXCompetencyDataModel.DataColumns.CompetencyId, data.CompetencyId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXCompetencyDataModel.DataColumns.CompetencyId);
                    }
                    break;

                case TaskXCompetencyDataModel.DataColumns.Task:
                    if (!string.IsNullOrEmpty(data.Task))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXCompetencyDataModel.DataColumns.Task, data.Task);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXCompetencyDataModel.DataColumns.Task);
                    }
                    break;

                case TaskXCompetencyDataModel.DataColumns.Competency:
                    if (!string.IsNullOrEmpty(data.Competency))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskXCompetencyDataModel.DataColumns.Competency, data.Competency);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskXCompetencyDataModel.DataColumns.Competency);
                    }
                    break;
            }

            return returnValue;
        }

        #region GetList

        public static List<TaskXCompetencyDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskXCompetencyDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static TaskXCompetencyDataModel GetDetails(TaskXCompetencyDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<TaskXCompetencyDataModel> GetEntityDetails(TaskXCompetencyDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXCompetencySearch " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                       ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
                     ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId);

            var result = new List<TaskXCompetencyDataModel>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new TaskXCompetencyDataModel();

                    dataItem.TaskXCompetencyId = (int)dbReader[TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId];
                    dataItem.TaskId = (int)dbReader[TaskXCompetencyDataModel.DataColumns.TaskId];
                    dataItem.CompetencyId = (int)dbReader[TaskXCompetencyDataModel.DataColumns.CompetencyId];
                    dataItem.Task = (string)dbReader[TaskXCompetencyDataModel.DataColumns.Task];
                    dataItem.Competency = (string)dbReader[TaskXCompetencyDataModel.DataColumns.Competency];

                    SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(TaskXCompetencyDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("TaskXCompetency.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(TaskXCompetencyDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("TaskXCompetency.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(TaskXCompetencyDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXCompetencyDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId);

            Framework.Components.DataAccess.DBDML.RunSQL("TaskXCompetency.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(TaskXCompetencyDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.TaskXCompetencySearch " +
               " " + BaseDataManager.SetCommonParametersForSearch(requestProfile.AuditId, requestProfile.ApplicationId, requestProfile.ApplicationModeId) +
               ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId) +
               ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.TaskId) +
               ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.CompetencyId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("TaskXCompetency.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(TaskXCompetencyDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TaskXCompetencyInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TaskXCompetencyUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId) +
                        ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.TaskId) +
                        ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.CompetencyId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(TaskXCompetencyDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskXCompetencySearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, TaskXCompetencyDataModel.DataColumns.TaskXCompetencyId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("TaskXCompetency.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

        #region Create By Task

        public static void CreateByTask(int taskId, int[] CompetencyIds, RequestProfile requestProfile)
        {
            foreach (int competencyId in CompetencyIds)
            {
                var sql = "EXEC TaskXCompetencyInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @TaskId					=   " + taskId +
                            ",      @CompetencyId				=   " + competencyId;

                Framework.Components.DataAccess.DBDML.RunSQL("TaskXCompetencyInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By Competency

        public static void CreateByCompetency(int competencyId, int[] TaskIds, RequestProfile requestProfile)
        {
            foreach (int taskId in TaskIds)
            {
                var sql = "EXEC TaskXCompetencyInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @TaskId					=   " + taskId +
                            ",      @CompetencyId				=   " + competencyId;
                Framework.Components.DataAccess.DBDML.RunSQL("TaskXCompetencyInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By Competency

        public static DataTable GetByCompetency(int competencyId, int auditId)
        {
            var sql = "EXEC TaskXCompetencySearch @CompetencyId     =" + competencyId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By Task

        public static DataTable GetByTask(int taskId, int auditId)
        {
            var sql = "EXEC TaskXCompetencySearch @TaskId       =" + taskId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete By Competency

        public static void DeleteByCompetency(int competencyId, int auditId)
        {
            var sql = "EXEC TaskXCompetencyDelete @CompetencyId       =" + competencyId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By Task

        public static void DeleteByTask(int taskId, int auditId)
        {
            var sql = "EXEC TaskXCompetencyDelete @TaskId		=" + taskId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
