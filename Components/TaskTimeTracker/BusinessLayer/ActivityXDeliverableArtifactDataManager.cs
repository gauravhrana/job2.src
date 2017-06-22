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
    public partial class ActivityXDeliverableArtifactDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ActivityXDeliverableArtifactDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ActivityXDeliverableArtifact");
        }

        #region GetList

        public static List<ActivityXDeliverableArtifactDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityList(ActivityXDeliverableArtifactDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static ActivityXDeliverableArtifactDataModel GetDetails(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityList(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<ActivityXDeliverableArtifactDataModel> GetEntityList(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var oDT = GetDetails(data, requestProfile);

            var dataList = new List<ActivityXDeliverableArtifactDataModel>();

            if (oDT != null)
            {
                var oData = new ActivityXDeliverableArtifactDataModel();

                oData.ActivityXDeliverableArtifactId = oDT.ActivityXDeliverableArtifactId;
                oData.Activity                       = oDT.Activity;
                oData.DeliverableArtifact            = oDT.DeliverableArtifact;
                oData.DeliverableArtifactStatus      = oDT.DeliverableArtifactStatus;

                //SetBaseInfo(oData, oDT);

                dataList.Add(oData);
            }

            return dataList;
        }

        #endregion

        #region Create

        public static void Create(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DBDML.RunSQL("ActivityXDeliverableArtifact.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("ActivityXDeliverableArtifact.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ActivityXDeliverableArtifactDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

            DBDML.RunSQL("ActivityXDeliverableArtifact.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ActivityXDeliverableArtifactDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId:
                    if (data.ActivityXDeliverableArtifactId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId, data.ActivityXDeliverableArtifactId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId);
                    }
                    break;

                case ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId:
                    if (data.DeliverableArtifactId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId, data.DeliverableArtifactId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);
                    }
                    break;

                case ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId:
                    if (data.ActivityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId, data.ActivityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId);
                    }
                    break;

                case ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId:
                    if (data.DeliverableArtifactStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId, data.DeliverableArtifactStatusId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId);

                    }
                    break;

                case ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact:
                    if (!string.IsNullOrEmpty(data.DeliverableArtifact))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact, data.DeliverableArtifact);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifact);
                    }
                    break;

                case ActivityXDeliverableArtifactDataModel.DataColumns.Activity:
                    if (!string.IsNullOrEmpty(data.Activity))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ActivityXDeliverableArtifactDataModel.DataColumns.Activity, data.Activity);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityXDeliverableArtifactDataModel.DataColumns.Activity);
                    }
                    break;

                case ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatus:
                    if (!string.IsNullOrEmpty(data.DeliverableArtifactStatus))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatus, data.DeliverableArtifactStatus);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatus);

                    }
                    break;
            }
            return returnValue;
        }

        public static DataTable Search(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ActivityXDeliverableArtifactSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId) +
                ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

            var oDT = new DBDataTable("ActivityXDeliverableArtifact.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ActivityXDeliverableArtifactInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ActivityXDeliverableArtifactUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId) +
                        ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId) +
                        ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId) +
                        ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ActivityXDeliverableArtifactDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ActivityXDeliverableArtifactSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
            ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId) +
            ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId) +
            ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId) +
            ", " + ToSQLParameter(data, ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId);

            var oDT = new DBDataTable("ActivityXDeliverableArtifact.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion
    }
}
