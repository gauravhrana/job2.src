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
    public partial class ApplicationMonitoredEventEmailDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

        static ApplicationMonitoredEventEmailDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationMonitoredEventEmail");
        }

        #region GetList

        public static List<ApplicationMonitoredEventEmailDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ApplicationMonitoredEventEmailDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static ApplicationMonitoredEventEmailDataModel GetDetails(ApplicationMonitoredEventEmailDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region GetEntitySearch

        public static List<ApplicationMonitoredEventEmailDataModel> GetEntityDetails(ApplicationMonitoredEventEmailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ApplicationMonitoredEventEmailSearch ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMode = requestProfile.ApplicationModeId
                ,
                ApplicationId = requestProfile.ApplicationId
                ,
                ApplicationMonitoredEventEmailId = dataQuery.ApplicationMonitoredEventEmailId
                ,
                ApplicationMonitoredEventSourceId = dataQuery.ApplicationMonitoredEventSourceId
                ,
                UserId = dataQuery.UserId
                ,
                ReturnAuditInfo = returnAuditInfo
            };

            List<ApplicationMonitoredEventEmailDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ApplicationMonitoredEventEmailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(ApplicationMonitoredEventEmailDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEventEmail.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationMonitoredEventEmailDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEventEmail.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationMonitoredEventEmailDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ApplicationMonitoredEventEmailDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMonitoredEventEmailId = dataQuery.ApplicationMonitoredEventEmailId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ApplicationMonitoredEventEmailDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventEmailId:
                    if (data.ApplicationMonitoredEventEmailId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventEmailId, data.ApplicationMonitoredEventEmailId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventEmailId);
                    }
                    break;


                case ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSourceId:
                    if (data.ApplicationMonitoredEventSourceId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSourceId, data.ApplicationMonitoredEventSourceId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSourceId);

                    }
                    break;

                case ApplicationMonitoredEventEmailDataModel.DataColumns.UserId:
                    if (data.UserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventEmailDataModel.DataColumns.UserId, data.UserId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventEmailDataModel.DataColumns.UserId);

                    }
                    break;

                case ApplicationMonitoredEventEmailDataModel.DataColumns.Active:
                    if (data.Active != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventEmailDataModel.DataColumns.Active, data.Active);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventEmailDataModel.DataColumns.Active);

                    }
                    break;

                case ApplicationMonitoredEventEmailDataModel.DataColumns.CorrespondenceLevel:
                    if (!string.IsNullOrEmpty(data.CorrespondenceLevel))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventEmailDataModel.DataColumns.CorrespondenceLevel, data.CorrespondenceLevel.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventEmailDataModel.DataColumns.CorrespondenceLevel);

                    }
                    break;

                case ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSource:
                    if (!string.IsNullOrEmpty(data.ApplicationMonitoredEventSource))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSource, data.ApplicationMonitoredEventSource.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSource);

                    }
                    break;

                case ApplicationMonitoredEventEmailDataModel.DataColumns.User:
                    if (!string.IsNullOrEmpty(data.User))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventEmailDataModel.DataColumns.User, data.User.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventEmailDataModel.DataColumns.User);

                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(ApplicationMonitoredEventEmailDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(ApplicationMonitoredEventEmailDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationMonitoredEventEmailInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationMonitoredEventEmailUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventEmailId) +
                        ", " + ToSQLParameter(data, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSourceId) +
                        ", " + ToSQLParameter(data, ApplicationMonitoredEventEmailDataModel.DataColumns.UserId) +
                        ", " + ToSQLParameter(data, ApplicationMonitoredEventEmailDataModel.DataColumns.CorrespondenceLevel) +
                        ", " + ToSQLParameter(data, ApplicationMonitoredEventEmailDataModel.DataColumns.Active);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ApplicationMonitoredEventEmailDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventEmailSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
            ", " + ToSQLParameter(data, ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventEmailId);

            var oDT = new DataAccess.DBDataTable("ApplicationMonitoredEventEmail.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

    }
}
