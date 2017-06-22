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
    public partial class ApplicationMonitoredEventSourceDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

        static ApplicationMonitoredEventSourceDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationMonitoredEventSource");
        }

        #region GetList

        public static List<ApplicationMonitoredEventSourceDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ApplicationMonitoredEventSourceDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static ApplicationMonitoredEventSourceDataModel GetDetails(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region GetEntitySearch

        public static List<ApplicationMonitoredEventSourceDataModel> GetEntityDetails(ApplicationMonitoredEventSourceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ApplicationMonitoredEventSourceSearch ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMonitoredEventSourceId = dataQuery.ApplicationMonitoredEventSourceId
                ,
                ReturnAuditInfo = returnAuditInfo
                ,
                ApplicationMode = requestProfile.ApplicationModeId
                ,
                ApplicationId = dataQuery.ApplicationId
                ,
                Code = dataQuery.Code
            };

            List<ApplicationMonitoredEventSourceDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ApplicationMonitoredEventSourceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }


            return result;
        }

        #endregion

        #region Create

        public static void Create(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEventSource.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEventSource.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationMonitoredEventSourceDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ApplicationMonitoredEventSourceDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMonitoredEventSourceId = dataQuery.ApplicationMonitoredEventSourceId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ApplicationMonitoredEventSourceDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId:
                    if (data.ApplicationMonitoredEventSourceId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId, data.ApplicationMonitoredEventSourceId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId);
                    }
                    break;

                case ApplicationMonitoredEventSourceDataModel.DataColumns.Code:
                    if (!string.IsNullOrEmpty(data.Code))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventSourceDataModel.DataColumns.Code, data.Code.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventSourceDataModel.DataColumns.Code);

                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationMonitoredEventSourceInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationMonitoredEventSourceUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId) +
                        ", " + ToSQLParameter(data, ApplicationMonitoredEventSourceDataModel.DataColumns.Code) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventSourceSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
             ", " + ToSQLParameter(data, ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId);

            var oDT = new DataAccess.DBDataTable("ApplicationMonitoredEventSource.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventSourceChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(ApplicationMonitoredEventSourceDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;
            var ds = GetChildren(data, requestProfile);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        isDeletable = false;
                        break;
                    }
                }
            }
            return isDeletable;
        }

        #endregion

    }
}
