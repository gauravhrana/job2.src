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
    public partial class ApplicationMonitoredEventProcessingStateDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

        static ApplicationMonitoredEventProcessingStateDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationMonitoredEventProcessingState");
        }

        #region GetList

        public static List<ApplicationMonitoredEventProcessingStateDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ApplicationMonitoredEventProcessingStateDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static ApplicationMonitoredEventProcessingStateDataModel GetDetails(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region GetEntitySearch

        public static List<ApplicationMonitoredEventProcessingStateDataModel> GetEntityDetails(ApplicationMonitoredEventProcessingStateDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ApplicationMonitoredEventProcessingStateSearch ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMonitoredEventProcessingStateId = dataQuery.ApplicationMonitoredEventProcessingStateId
                ,
                ReturnAuditInfo = returnAuditInfo
                ,
                ApplicationMode = requestProfile.ApplicationModeId
                ,
                ApplicationId = requestProfile.ApplicationId
                ,
                Code = dataQuery.Code
            };

            List<ApplicationMonitoredEventProcessingStateDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ApplicationMonitoredEventProcessingStateDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEventProcessingState.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEventProcessingState.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationMonitoredEventProcessingStateDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ApplicationMonitoredEventProcessingStateDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMonitoredEventProcessingStateId = dataQuery.ApplicationMonitoredEventProcessingStateId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ApplicationMonitoredEventProcessingStateDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId:
                    if (data.ApplicationMonitoredEventProcessingStateId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId, data.ApplicationMonitoredEventProcessingStateId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId);
                    }
                    break;

                case ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code:
                    if (!string.IsNullOrEmpty(data.Code))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code, data.Code.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code);

                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationMonitoredEventProcessingStateInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationMonitoredEventProcessingStateUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId) +
                        ", " + ToSQLParameter(data, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventProcessingStateSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
             ", " + ToSQLParameter(data, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId);

            var oDT = new DataAccess.DBDataTable("ApplicationMonitoredEventProcessingState.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventProcessingStateChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(ApplicationMonitoredEventProcessingStateDataModel data, RequestProfile requestProfile)
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
