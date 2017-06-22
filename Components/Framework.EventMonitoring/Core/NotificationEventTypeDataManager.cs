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
    public partial class NotificationEventTypeDataManager : StandardDataManager
    {
        
        #region GetList

        
        public static List<NotificationEventTypeDataModel> GetNotificationEventTypeList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.NotificationEventTypeSearch" +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var result = new List<NotificationEventTypeDataModel>();

            using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new NotificationEventTypeDataModel();

                    dataItem.NotificationEventTypeId = (int)dbReader[NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId];
                    dataItem.ApplicationId = (int)dbReader[BaseDataModel.BaseDataColumns.ApplicationId];

                    SetStandardInfo(dataItem, dbReader);

                    SetStandardInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }

            }

            return result;
        }

        #endregion GetList

    }
}
