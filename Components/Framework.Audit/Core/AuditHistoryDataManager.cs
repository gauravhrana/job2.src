using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace Framework.Components.Audit
{
    public partial class AuditHistoryDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

        static AuditHistoryDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("AuditHistory");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(DataModel.Framework.Audit.AuditHistory data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case DataModel.Framework.Audit.AuditHistory.DataColumns.AuditHistoryId:
                    if (data.AuditHistoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditHistoryId, data.AuditHistoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditHistoryId);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId:
                    if (data.SystemEntityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId, data.SystemEntityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.TraceId:
                    if (data.TraceId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Audit.AuditHistory.DataColumns.TraceId, data.TraceId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.TraceId);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey:
                    if (data.EntityKey != null)
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey, data.EntityKey);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId:
                    if (data.AuditActionId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId, data.AuditActionId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedDate:
                    if (data.CreatedDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedDate, data.CreatedDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedDate);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedByPersonId:
                    if (data.CreatedByPersonId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedByPersonId, data.CreatedByPersonId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedByPersonId);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId:
                    if (data.PersonId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId, data.PersonId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.TimeInterval:
                    if (data.TimeInterval != null)
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.TimeInterval, data.TimeInterval);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.TimeInterval);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntity:
                    if (!string.IsNullOrEmpty(data.SystemEntity))
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntity, data.SystemEntity);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntity);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.AuditAction:
                    if (!string.IsNullOrEmpty(data.AuditAction))
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditAction, data.AuditAction);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditAction);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.Person:
                    if (!string.IsNullOrEmpty(data.Person))
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.Person, data.Person);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.Person);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.DataViewMode:
                    if (!string.IsNullOrEmpty(data.DataViewMode))
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.DataViewMode, data.DataViewMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.DataViewMode);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.ToSearchDate:
                    if (data.TimeInterval != null)
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.ToSearchDate, data.ToSearchDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.ToSearchDate);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.FromSearchDate:
                    if (data.FromSearchDate != null)
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.FromSearchDate, data.FromSearchDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.FromSearchDate);
                    }
                    break;

                case DataModel.Framework.Audit.AuditHistory.DataColumns.TypeOfIssue:
                    if (!string.IsNullOrEmpty(data.TypeOfIssue))
                    {
                        returnValue = string.Format(BaseDataManager.SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Audit.AuditHistory.DataColumns.TypeOfIssue, data.TypeOfIssue);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Audit.AuditHistory.DataColumns.TypeOfIssue);
                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }
            return returnValue;
        }

        #endregion

        #region GetTopNList

        public static DataTable GetTopNList(int count)
        {
            var sql = "Select TOP " + count + "* FROM AuditHistory";

            var oDT = new DataAccess.DBDataTable("AuditHistory.TopNList", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataModel.Framework.Audit.AuditHistory GetDetails(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        static public List<DataModel.Framework.Audit.AuditHistory> GetEntityDetails(DataModel.Framework.Audit.AuditHistory dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.AuditHistorySearch ";

            var parameters =
            new
            {
                    AuditId           = requestProfile.AuditId
                ,   AuditActionId     = dataQuery.AuditActionId
                ,   EntityKey         = dataQuery.EntityKey
                ,   CreatedByPersonId = dataQuery.CreatedByPersonId
                ,   TraceId           = dataQuery.TraceId
                ,   SystementityId    = dataQuery.SystemEntityId
            };

            List<DataModel.Framework.Audit.AuditHistory> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<DataModel.Framework.Audit.AuditHistory>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("AuditHistory.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("AuditHistory.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {

            const string sql = @"dbo.AuditHistoryDelete ";

            var parameters = new
            {
                AuditHistoryId = data.AuditHistoryId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region DeleteByEntity

        public static void DeleteByEntity(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.AuditHistoryDeleteByEntity " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey);

            DataAccess.DBDML.RunSQL("AuditHistory.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static DataTable SearchForActivityStream(DataModel.Framework.Audit.AuditHistory data, int pageSize, int pageIndex, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.AuditHistorySearchForActivityStream " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns.PageSize, pageSize) +
                ", " + ToSQLParameter(DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns.PageIndex, pageIndex) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.FromSearchDate) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.ToSearchDate) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.DataViewMode);

            var oDT = new DataAccess.DBDataTable("AuditHistory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchByAuditAction(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.AuditHistoryFindByAuditAction " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                //", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId);

            var oDT = new DataAccess.DBDataTable("AuditHistory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchByActionByAndAuditAction(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.FindByActionByAuditAction " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                //", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId);

            var oDT = new DataAccess.DBDataTable("AuditHistory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchRecordsWithMissingHistory(int systemEntityTypeId, RequestProfile requestProfile)
        {

            var SQL = string.Format("EXEC dbo.FindRecordsWithNoAuditHistory @SystemEntityTypeId = {0}, @auditId = {1}", systemEntityTypeId, requestProfile.AuditId);

            var oDT = new DataAccess.DBDataTable("AuditHistory.RecordsWithNoHistory", SQL, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchByTimeInterval(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.AuditHistoryFind " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                //", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.TimeInterval);

            var oDT = new DataAccess.DBDataTable("AuditHistory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchBrokenHistoryRecords(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FindBrokenHistoryRecords " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.TypeOfIssue) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId);

            var oDT = new DataAccess.DBDataTable("AuditHistory.FindBrokenHistoryRecords", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Find

        public static DataTable Find(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {

            // formulate SQL
            var sql = "EXEC dbo.AuditHistorySearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                //", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.PersonId) +
                ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId);// +
            //", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.FromSearchDate) +
            //", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.ToSearchDate);

            var oDT = new DataAccess.DBDataTable("AuditHistory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.AuditHistoryInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.AuditHistoryUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditHistoryId) +
                        ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId) +
                        ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey) +
                        ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId) +
                        ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedDate) +
                        ", " + ToSQLParameter(data, DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedByPersonId);
            return sql;
        }

        #endregion

        #region Get Entity wise Test DataModel.AuditHistory

        public static DataTable GetEntityTestData(DataModel.Framework.Audit.AuditHistory data, RequestProfile requestProfile)
        {

            var SQL = string.Format("EXEC dbo.GetTestDataCount");

            var oDT = new DataAccess.DBDataTable("EXEC GetTestDataCount", SQL, "TaskTimeTracker");

            return oDT.DBTable;
        }

        #endregion

        #region Get Entity wise Test DataModel.AuditHistory

        public static DataTable GetTestAndAuditDataIds(string Entity, string typeofdata)
        {

            var SQL = string.Format("EXEC dbo.GetTestAndAuditDataIds @SystemEntityType={0}, @TypeOfData={1}", Entity, typeofdata);

            var oDT = new DataAccess.DBDataTable("EXEC GetTestAndAuditDataIds", SQL, "TaskTimeTracker");

            return oDT.DBTable;
        }

        #endregion

        #region Get Entity Test Detail DataModel.AuditHistory

        public static DataTable GetEntityTestDetailData(int systemEntityTypeId, RequestProfile requestProfile)
        {
            var systemEntityId = Convert.ToString(systemEntityTypeId);

            var SQL = string.Format("EXEC AuditTestDataDetails @SystemEntityTypeId = {0}, @AuditId = {1}", systemEntityId, requestProfile.AuditId);

            var oDT = new DataAccess.DBDataTable("EXEC GetEntityTestDetailData", SQL, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Delete Audit History Records by Entit Key and System Entity

        public static void DeleteDataBySystemEntity(string entityKey, int systemEntityTypeId, RequestProfile requestProfile)
        {
            //var SQL = "EXEC dbo.AuditHistoryDeleteDataBySystemEntity " +
            //                " " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
            //                ", @EntityKey                = '" + entityKey + "' " +
            //                ", @SystemEntityTypeId		= " + systemEntityTypeId.ToString();

            //DataAccess.DBDML.RunSQL("AuditHistory.Delete", SQL, DataStoreKey);
        }

        #endregion Delete

    }
}


