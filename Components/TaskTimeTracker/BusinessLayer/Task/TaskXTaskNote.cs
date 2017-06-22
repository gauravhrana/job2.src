using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
    public partial class TaskXTaskNote : BaseDataManager
    {
        static readonly string DataStoreKey = "";
       
        static TaskXTaskNote()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskXTaskNote");
        }

        #region Create By Task

        public static void CreateByTask(int TaskId, int[] TaskNoteIds, RequestProfile requestProfile)
        {
            foreach (int TaskNoteId in TaskNoteIds)
            {
                var sql = "EXEC TaskXTaskNoteInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @TaskId=" + TaskId +
                            ",      @TaskNoteId=" + TaskNoteId;

                DBDML.RunSQL("TaskXTaskNoteInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By TaskNote

        public static void CreateByTaskNote(int TaskNoteId, int[] TaskIds, RequestProfile requestProfile)
        {
            foreach (int TaskId in TaskIds)
            {
                var sql = "EXEC TaskXTaskNoteInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @TaskId=" + TaskId +
                            ",      @TaskNoteId=" + TaskNoteId;
                DBDML.RunSQL("TaskXTaskNoteInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By TaskNote

        public static DataTable GetByTaskNote(int TaskNoteId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskXTaskNoteSearch @TaskNoteId=" + TaskNoteId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By Task

        public static DataTable GetByTask(int TaskId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskXTaskNoteSearch @TaskId=" + TaskId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create By Task

        public static void Create(int TaskId, int[] tasknoteIds, RequestProfile requestProfile)
        {
            foreach (int tasknoteId in tasknoteIds)
            {
                var sql = "EXEC TaskXTaskNoteInsert " +
                          "@TaskId=" + TaskId + ", " +
                          "@TaskNoteId=" + tasknoteId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;

                DBDML.RunSQL("TaskNote_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Delete By TaskNote

        public static void DeleteByTaskNote(int TaskNoteId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskXTaskNoteDelete @TaskNoteId=" + TaskNoteId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By Task

        public static void DeleteByTask(int TaskId, RequestProfile requestProfile)
        {
            var sql = "EXEC TaskXTaskNoteDelete @TaskId=" + TaskId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
