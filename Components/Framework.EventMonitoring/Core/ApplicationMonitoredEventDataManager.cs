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
    public partial class ApplicationMonitoredEventDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

        static ApplicationMonitoredEventDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationMonitoredEvent");
        }

        #region GetList

        public static List<ApplicationMonitoredEventDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ApplicationMonitoredEventDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetEntitySearch

        public static List<ApplicationMonitoredEventDataModel> GetEntityDetails(ApplicationMonitoredEventDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ApplicationMonitoredEventSearch ";

            var parameters =
            new
            {
                    AuditId                                     = requestProfile.AuditId
                ,   ApplicationMode                             = requestProfile.ApplicationModeId
                ,   ApplicationId                               = requestProfile.ApplicationId
                ,   ApplicationMonitoredEventId                 = dataQuery.ApplicationMonitoredEventId
                ,   ReturnAuditInfo                             = returnAuditInfo
                ,   ApplicationMonitoriedEventProcessingStateId = dataQuery.ApplicationMonitoredEventProcessingStateId
                ,   ApplicationMonitoredEventSourceId           = dataQuery.ApplicationMonitoredEventSourceId
                ,   LastModifiedBy                              = dataQuery.LastModifiedBy
            };

            List<ApplicationMonitoredEventDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ApplicationMonitoredEventDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }


            return result;
        }

        #endregion

        #region GetDetails

        public static ApplicationMonitoredEventDataModel GetDetails(ApplicationMonitoredEventDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Create

        public static void Create(ApplicationMonitoredEventDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEvent.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationMonitoredEventDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEvent.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationMonitoredEventDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ApplicationMonitoredEventDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMonitoredEventId = dataQuery.ApplicationMonitoredEventId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ApplicationMonitoredEventDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventId:
                    if (data.ApplicationMonitoredEventId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventId, data.ApplicationMonitoredEventId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventId);
                    }
                    break;


                case ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventSourceId:
                    if (data.ApplicationMonitoredEventSourceId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventSourceId, data.ApplicationMonitoredEventSourceId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventSourceId);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId:
                    if (data.ApplicationMonitoredEventProcessingStateId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId, data.ApplicationMonitoredEventProcessingStateId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.ReferenceId:
                    if (data.ReferenceId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventDataModel.DataColumns.ReferenceId, data.ReferenceId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.ReferenceId);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.ReferenceCode:
                    if (!string.IsNullOrEmpty(data.ReferenceCode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventDataModel.DataColumns.ReferenceCode, data.ReferenceCode.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.ReferenceCode);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.Category:
                    if (!string.IsNullOrEmpty(data.Category))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventDataModel.DataColumns.Category, data.Category.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.Category);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.Message:
                    if (!string.IsNullOrEmpty(data.Message))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventDataModel.DataColumns.Message, data.Message.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.Message);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.IsDuplicate:
                    if (data.IsDuplicate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEventDataModel.DataColumns.IsDuplicate, data.IsDuplicate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.IsDuplicate);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.LastModifiedBy:
                    if (!string.IsNullOrEmpty(data.LastModifiedBy))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventDataModel.DataColumns.LastModifiedBy, data.LastModifiedBy.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.LastModifiedBy);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.LastModifiedOn:
                    if (data.LastModifiedOn != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventDataModel.DataColumns.LastModifiedOn, data.LastModifiedOn);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.LastModifiedOn);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventSource:
                    if (!string.IsNullOrEmpty(data.ApplicationMonitoredEventSource))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventSource, data.ApplicationMonitoredEventSource.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventSource);

                    }
                    break;

                case ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventProcessingState:
                    if (!string.IsNullOrEmpty(data.ApplicationMonitoredEventProcessingState))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventProcessingState, data.ApplicationMonitoredEventProcessingState.Trim());

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventProcessingState);

                    }
                    break;


                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(ApplicationMonitoredEventDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(ApplicationMonitoredEventDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationMonitoredEventInsert  " +
                       " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationMonitoredEventUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventId) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventSourceId) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.ReferenceId) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.ReferenceCode) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.Category) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.Message) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.IsDuplicate) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.LastModifiedOn) +
                    ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.LastModifiedBy);


            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ApplicationMonitoredEventDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
            ", " + ToSQLParameter(data, ApplicationMonitoredEventDataModel.DataColumns.ApplicationMonitoredEventId);

            var oDT = new DataAccess.DBDataTable("ApplicationMonitoredEvent.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

    }
}
