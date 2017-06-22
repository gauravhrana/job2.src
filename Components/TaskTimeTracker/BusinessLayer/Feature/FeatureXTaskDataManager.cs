using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
    public partial class FeatureXTaskDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static FeatureXTaskDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FeatureXTask");
        }

        #region Create By Task

        public static void Create(int TaskId, int[] featureIds, RequestProfile requestProfile)
        {
            foreach (int featureId in featureIds)
            {
                var sql = "EXEC FeatureXTaskInsert " +
                          "@TaskId = " + TaskId + ", " +
                          "@FeatureId =" + featureId + ", " +
                          "@ApplicationId =" + requestProfile.ApplicationId + ", " +
                          "@AuditId	= " + requestProfile.AuditId;

                DBDML.RunSQL("FeatureXTask_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(FeatureXTaskDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case FeatureXTaskDataModel.DataColumns.FeatureXTaskId:
                    if (data.FeatureXTaskId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureXTaskDataModel.DataColumns.FeatureXTaskId, data.FeatureXTaskId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXTaskDataModel.DataColumns.FeatureXTaskId);
                    }
                    break;

                case FeatureXTaskDataModel.DataColumns.TaskId:
                    if (data.TaskId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureXTaskDataModel.DataColumns.TaskId, data.TaskId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXTaskDataModel.DataColumns.TaskId);
                    }
                    break;

                case FeatureXTaskDataModel.DataColumns.FeatureId:
                    if (data.FeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureXTaskDataModel.DataColumns.FeatureId, data.FeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXTaskDataModel.DataColumns.FeatureId);
                    }
                    break;

                case FeatureXTaskDataModel.DataColumns.Task:
                    if (!string.IsNullOrEmpty(data.Task))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FeatureXTaskDataModel.DataColumns.Task, data.Task);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXTaskDataModel.DataColumns.Task);
                    }
                    break;

                case FeatureXTaskDataModel.DataColumns.Feature:
                    if (!string.IsNullOrEmpty(data.Feature))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FeatureXTaskDataModel.DataColumns.Feature, data.Feature);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXTaskDataModel.DataColumns.Feature);
                    }
                    break;
            }
            return returnValue;
        }

        public static DataTable Search(FeatureXTaskDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FeatureXTaskSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, FeatureXTaskDataModel.DataColumns.FeatureId) +
                ", " + ToSQLParameter(data, FeatureXTaskDataModel.DataColumns.TaskId);


            var oDT = new DBDataTable("FeatureXTask.Search", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion Search

        #region Create By Features

        public static void CreateByFeature(int featureId, int[] TaskIds, RequestProfile requestProfile)
        {
            foreach (int TaskId in TaskIds)
            {
                var sql = "EXEC FeatureXTaskInsert " +
                          "@TaskId						=" + TaskId + ", " +
                          "@FeatureId					=" + featureId + ", " +
                          "@ApplicationId			    =" + requestProfile.ApplicationId + ", " +
                          "@AuditId						=" + requestProfile.AuditId;
                DBDML.RunSQL("FeatureXTask_Insert", sql, DataStoreKey);
            }
        }
        #endregion

        #region Get By Feature

        public static DataTable GetByFeature(int featureId, int auditId)
        {
            FeatureXTaskDataModel data = new FeatureXTaskDataModel();
            data.FeatureId = featureId;
            //var sql = "EXEC FeatureXTaskSearch @FeatureId			=" + featureId + ", " +
            //			  "@AuditId									=" + auditId;
            var sql = "EXEC dbo.FeatureXTaskSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, FeatureXTaskDataModel.DataColumns.FeatureId);
            var oDT = new DBDataTable("FeatureXTask.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By Task

        public static DataTable GetByTask(int TaskId, int auditId)
        {
            var sql = "EXEC FeatureXTaskSearch @TaskId			=" + TaskId + ", " +
                          "@AuditId								=" + auditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete By Feature

        public static void DeleteByFeature(int featureId, int auditId)
        {
            var sql = "EXEC FeatureXTaskDelete @FeatureId			=" + featureId + ", " +
                          "@AuditId									=" + auditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By Task

        public static void DeleteByTask(int TaskId, int auditId)
        {
            var sql = "EXEC FeatureXTaskDelete @TaskId				=" + TaskId + ", " +
                          "@AuditId									=" + auditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region DoesExist

        //public static DataTable DoesExist(FeatureXTask.Data data, int auditId)
        //{
        //    var sql = "EXEC dbo.FeatureXTaskSearch " +
        //                    "  @" + Columns.FeatureXTaskId + "				=  " + data.FeatureXTaskId.ToString() +
        //                    "  @" + Columns.TaskId + "						=  " + data.TaskId.ToString() +
        //                    ", @" + Columns.FeatureId + "					=  " + data.FeatureId.ToString() +
        //                    ", @" + BaseColumns.AuditId + "					=  " + auditId.ToString();

        //    var oDT = new Framework.Components.DataAccess.DBDataTable("FeatureXTask.DoesExist", sql, DataStoreKey);

        //    return oDT.DBTable;
        //}


        #endregion DoesExist

    }
}
