using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.Audit
{
    public partial class TraceDataManager : StandardDataManager
    {
        public static List<DataModel.Framework.Audit.TraceDataModel> GetEntityList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TraceSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            //", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var result = new List<DataModel.Framework.Audit.TraceDataModel>();

            using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new DataModel.Framework.Audit.TraceDataModel();

                    dataItem.TraceId = (int)dbReader[DataModel.Framework.Audit.TraceDataModel.DataColumns.TraceId];

                    SetStandardInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }

            }

            return result;
        }

        #region GetNextTraceId

        public static int GetNextTraceId(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.GetNextTraceId " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var traceId = Framework.Components.DataAccess.DBDML.RunScalarSQL("Trace.Insert", sql, DataStoreKey);
            return Convert.ToInt32(traceId);
        }

        #endregion


    }
}
