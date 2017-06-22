using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.EventMonitoring;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.EventMonitoring
{
    public partial class NotificationPublisherXEventTypeDataManager : BaseDataManager
    {
        private static readonly string DataStoreKey = "";

        static NotificationPublisherXEventTypeDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationPublisherXEventType");
        }

        public static string ToSQLParameter(NotificationPublisherXEventTypeDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId:
                    if (data.NotificationPublisherXEventTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId, data.NotificationPublisherXEventTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId);
                    }
                    break;

                case NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherId:
                    if (data.NotificationPublisherId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherId, data.NotificationPublisherId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherId);
                    }
                    break;

                case NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId:
                    if (data.NotificationEventTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId, data.NotificationEventTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId);
                    }
                    break;

                case NotificationPublisherXEventTypeDataModel.DataColumns.CreatedTimeId:
                    if (data.CreatedTimeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationPublisherXEventTypeDataModel.DataColumns.CreatedTimeId, data.CreatedTimeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherXEventTypeDataModel.DataColumns.CreatedTimeId);
                    }
                    break;

                case NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisher:
                    if (!string.IsNullOrEmpty(data.NotificationPublisher))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisher, data.NotificationPublisher);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisher);
                    }
                    break;

                case NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventType:
                    if (!string.IsNullOrEmpty(data.NotificationEventType))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventType, data.NotificationEventType);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventType);
                    }
                    break;

            }

            return returnValue;
        }

        #region Create By NotificationEventType

        public static void CreateByNotificationEventType(int notificationEventTypeId, int[] NotificationPublisherIds, RequestProfile requestProfile)
        {
            foreach (int NotificationPublisherId in NotificationPublisherIds)
            {
                var sql = "EXEC NotificationPublisherXEventTypeInsert " +
                          "@NotificationEventTypeId=" + notificationEventTypeId + ", " +
                          "@NotificationPublisherId=" + NotificationPublisherId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;

                Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisherXEventTypeInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By NotificationPublisher

        public static void CreateByNotificationPublisher(int NotificationPublisherId, int[] notificationEventTypeIds, RequestProfile requestProfile)
        {
            foreach (int notificationEventTypeId in notificationEventTypeIds)
            {
                var sql = "EXEC NotificationPublisherXEventTypeInsert " +
                          "@NotificationEventTypeId=" + notificationEventTypeId + ", " +
                          "@NotificationPublisherId=" + NotificationPublisherId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;
                Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisherXEventTypeInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By NotificationEventType

        public static DataTable GetByNotificationEventType(int notificationEventTypeId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationPublisherXEventTypeSearch @NotificationEventTypeId     =" + notificationEventTypeId + ", " +
                          "@AuditId=" + requestProfile.AuditId + ", " +
                         "@ApplicationId=" + requestProfile.ApplicationId;
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By NotificationPublisher

        public static DataTable GetByNotificationPublisher(int NotificationPublisherId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationPublisherXEventTypeSearch @NotificationPublisherId =" + NotificationPublisherId + ", " +
                          "@AuditId=" + requestProfile.AuditId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId;
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static NotificationPublisherXEventTypeDataModel GetDetails(NotificationPublisherXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityList(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region GetEntitySearch

        public static List<NotificationPublisherXEventTypeDataModel> GetEntityList(NotificationPublisherXEventTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.NotificationPublisherXEventTypeSearch ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationId = requestProfile.ApplicationId
                ,
                NotificationPublisherXEventTypeId = dataQuery.NotificationPublisherXEventTypeId
                ,
                ReturnAuditInfo = returnAuditInfo
                ,
                ApplicationMode = requestProfile.ApplicationModeId
                ,
                NotificationPublisherId = dataQuery.NotificationPublisherId
                ,
                NotificationEventTypeId = dataQuery.NotificationEventTypeId
                ,
                CreatedDateId = dataQuery.CreatedDateId
                ,
                CreatedTimeId = dataQuery.CreatedTimeId
            };

            List<NotificationPublisherXEventTypeDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<NotificationPublisherXEventTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }


            return result;
        }

        #endregion

        #region Create

        public static void Create(NotificationPublisherXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisherXEventType.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(NotificationPublisherXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisherXEventType.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(NotificationPublisherXEventTypeDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.NotificationPublisherXEventTypeDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                NotificationPublisherXEventTypeId = dataQuery.NotificationPublisherXEventTypeId
                ,
                NotificationPublisherId = dataQuery.NotificationPublisherId
                ,
                NotificatiionEventTypeId = dataQuery.NotificationEventTypeId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(NotificationPublisherXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityList(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(NotificationPublisherXEventTypeDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.NotificationPublisherXEventTypeInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.NotificationPublisherXEventTypeUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId) +
                ", " + ToSQLParameter(data, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherId) +
                ", " + ToSQLParameter(data, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId);

            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(NotificationPublisherXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.NotificationPublisherXEventTypeSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId);


            var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationPublisherXEventType.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

        #region Delete By NotificationPublisher

        public static void DeleteByNotificationPublisher(int NotificationPublisherId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationPublisherXEventTypeDelete @NotificationPublisherId       =" + NotificationPublisherId + ", " +
                 ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) + ", " +
                          "@AuditId=" + requestProfile.AuditId;
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By NotificationEventType

        public static void DeleteByNotificationEventType(int notificationEventTypeId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationPublisherXEventTypeDelete @NotificationEventTypeId  =" + notificationEventTypeId + ", " +
                ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) + ", " +
                          "@AuditId								=" + requestProfile.AuditId;
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion
    }
}
