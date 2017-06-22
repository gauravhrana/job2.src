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
    public partial class NotificationSubscriberXEventTypeDataManager : BaseDataManager
    {
        private static readonly string DataStoreKey = "";

        static NotificationSubscriberXEventTypeDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationSubscriberXEventType");
        }

        public static string ToSQLParameter(NotificationSubscriberXEventTypeDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberXEventTypeId:
                    if (data.NotificationSubscriberXEventTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberXEventTypeId, data.NotificationSubscriberXEventTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberXEventTypeId);
                    }
                    break;

                case NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberId:
                    if (data.NotificationSubscriberId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberId, data.NotificationSubscriberId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberId);
                    }
                    break;

                case NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationEventTypeId:
                    if (data.NotificationEventTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationEventTypeId, data.NotificationEventTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationEventTypeId);
                    }
                    break;

                case NotificationSubscriberXEventTypeDataModel.DataColumns.CreatedTimeId:
                    if (data.CreatedTimeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationSubscriberXEventTypeDataModel.DataColumns.CreatedTimeId, data.CreatedTimeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberXEventTypeDataModel.DataColumns.CreatedTimeId);
                    }
                    break;

                case NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriber:
                    if (!string.IsNullOrEmpty(data.NotificationSubscriber))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriber, data.NotificationSubscriber);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriber);
                    }
                    break;

                case NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationEventType:
                    if (!string.IsNullOrEmpty(data.NotificationEventType))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationEventType, data.NotificationEventType);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationEventType);
                    }
                    break;

            }

            return returnValue;
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.NotificationSubscriberXEventTypeSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationSubscriberXEventType.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create By NotificationEventType

        public static void CreateByNotificationEventType(int notificationEventTypeId, int[] NotificationSubscriberIds, RequestProfile requestProfile)
        {
            foreach (int NotificationSubscriberId in NotificationSubscriberIds)
            {
                var sql = "EXEC NotificationSubscriberXEventTypeInsert " +
                          "@NotificationEventTypeId=" + notificationEventTypeId + ", " +
                          "@NotificationSubscriberId=" + NotificationSubscriberId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;

                Framework.Components.DataAccess.DBDML.RunSQL("NotificationSubscriberXEventTypeInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By NotificationSubscriber

        public static void CreateByNotificationSubscriber(int NotificationSubscriberId, int[] notificationEventTypeIds, RequestProfile requestProfile)
        {
            foreach (int notificationEventTypeId in notificationEventTypeIds)
            {
                var sql = "EXEC NotificationSubscriberXEventTypeInsert " +
                          "@NotificationEventTypeId=" + notificationEventTypeId + ", " +
                          "@NotificationSubscriberId=" + NotificationSubscriberId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;
                Framework.Components.DataAccess.DBDML.RunSQL("NotificationSubscriberXEventTypeInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By NotificationEventType

        public static DataTable GetByNotificationEventType(int notificationEventTypeId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationSubscriberXEventTypeSearch @NotificationEventTypeId     =" + notificationEventTypeId + ", " +
                          "@AuditId=" + requestProfile.AuditId + ", " +
                         "@ApplicationId=" + requestProfile.ApplicationId;
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By NotificationSubscriber

        public static DataTable GetByNotificationSubscriber(int NotificationSubscriberId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationSubscriberXEventTypeSearch @NotificationSubscriberId =" + NotificationSubscriberId + ", " +
                          "@AuditId=" + requestProfile.AuditId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId;
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static NotificationSubscriberXEventTypeDataModel GetDetails(NotificationSubscriberXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityList(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region GetEntitySearch


        public static List<NotificationSubscriberXEventTypeDataModel> GetEntityList(NotificationSubscriberXEventTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.NotificationSubscriberXEventTypeSearch ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                NotificationSubscriberXEventTypeId = dataQuery.NotificationSubscriberXEventTypeId
                ,
                ReturnAuditInfo = returnAuditInfo
                ,
                ApplicationId = dataQuery.ApplicationId
                ,
                NotificationSubscriberId = dataQuery.NotificationSubscriberId
                ,
                NotificationEventTypeId = dataQuery.NotificationEventTypeId
                ,
                CreatedDateId = dataQuery.CreatedDateId
                ,
                CreatedTimeId = dataQuery.CreatedTimeId
                ,
                ApplicationMode = requestProfile.ApplicationModeId
            };

            List<NotificationSubscriberXEventTypeDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<NotificationSubscriberXEventTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(NotificationSubscriberXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("NotificationSubscriberXEventType.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(NotificationSubscriberXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("NotificationSubscriberXEventType.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(NotificationSubscriberXEventTypeDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.NotificationSubscriberXEventTypeDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                NotificationSubscriberXEventTypeId = dataQuery.NotificationSubscriberXEventTypeId
                ,
                NotificationSubscriberId = dataQuery.NotificationSubscriberId
                ,
                NotificationEventTypeID = dataQuery.NotificationEventTypeId

            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(NotificationSubscriberXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityList(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(NotificationSubscriberXEventTypeDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.NotificationSubscriberXEventTypeInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.NotificationSubscriberXEventTypeUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberXEventTypeId) +
                ", " + ToSQLParameter(data, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberId) +
                ", " + ToSQLParameter(data, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationEventTypeId);

            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(NotificationSubscriberXEventTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.NotificationSubscriberXEventTypeSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, NotificationSubscriberXEventTypeDataModel.DataColumns.NotificationSubscriberXEventTypeId);


            var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationSubscriberXEventType.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

        #region Delete By NotificationSubscriber

        public static void DeleteByNotificationSubscriber(int NotificationSubscriberId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationSubscriberXEventTypeDelete @NotificationSubscriberId       =" + NotificationSubscriberId + ", " +
                 ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) + ", " +
                          "@AuditId=" + requestProfile.AuditId;
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By NotificationEventType

        public static void DeleteByNotificationEventType(int notificationEventTypeId, RequestProfile requestProfile)
        {
            var sql = "EXEC NotificationSubscriberXEventTypeDelete @NotificationEventTypeId  =" + notificationEventTypeId + ", " +
                ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) + ", " +
                          "@AuditId								=" + requestProfile.AuditId;
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion
    }
}
