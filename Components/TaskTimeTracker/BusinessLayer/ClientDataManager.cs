using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Dynamic;
using System.Collections;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ClientDataManager : StandardDataManager
    {

        #region DeleteChildren

        public static DataSet DeleteChildren(ClientDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ClientChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, ClientDataModel.DataColumns.ClientId);

            var oDt = new DBDataSet("Client.DeleteChildren", sql, DataStoreKey);

            return oDt.DBDataset;
        }

        public static IList GetEntityDetails(ClientDataModel searchItem, object p)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
