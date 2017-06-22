using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using Dapper;

namespace Framework.Components.Import
{
    public partial class BatchFileHistoryDataManager : StandardDataManager
    {

        static readonly string DataStoreKey = "";

        static BatchFileHistoryDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("BatchFileHistory");
        }

        public static string ToSQLParameter(BatchFileHistoryDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case BatchFileHistoryDataModel.DataColumns.BatchFileHistoryId:
                    if (data.BatchFileHistoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileHistoryDataModel.DataColumns.BatchFileId, data.BatchFileHistoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileHistoryDataModel.DataColumns.BatchFileHistoryId);
                    }
                    break;


                case BatchFileHistoryDataModel.DataColumns.BatchFileSetId:
                    if (data.BatchFileSetId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileHistoryDataModel.DataColumns.BatchFileSetId, data.BatchFileSetId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileHistoryDataModel.DataColumns.BatchFileSetId);
                    }
                    break;

                case BatchFileHistoryDataModel.DataColumns.BatchFileId:
                    if (data.BatchFileId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileHistoryDataModel.DataColumns.BatchFileId, data.BatchFileId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileHistoryDataModel.DataColumns.BatchFileId);
                    }
                    break;

                case BatchFileHistoryDataModel.DataColumns.UpdatedDate:
                    if (data.UpdatedDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileHistoryDataModel.DataColumns.UpdatedDate, data.UpdatedDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileHistoryDataModel.DataColumns.UpdatedDate);
                    }
                    break;

                case BatchFileHistoryDataModel.DataColumns.UpdatedByPersonId:
                    if (data.UpdatedByPersonId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BatchFileHistoryDataModel.DataColumns.UpdatedByPersonId, data.UpdatedByPersonId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileHistoryDataModel.DataColumns.UpdatedByPersonId);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        #region GetDetails

        public static BatchFileHistoryDataModel GetDetails(BatchFileHistoryDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Create

        public static void Create(BatchFileHistoryDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("BatchFileHistory.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(BatchFileHistoryDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("BatchFileHistory.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(BatchFileHistoryDataModel data, RequestProfile requestProfile)
        {
            const string sql = @"dbo.BatchFileHistoryDelete ";

            var parameters = new
            {
                AuditId = requestProfile.AuditId
                ,
                BatchFileHistoryId = data.BatchFileHistoryId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(BatchFileHistoryDataModel data, RequestProfile requestProfile, int applicationMode = 0)
        {
            // formulate SQL
            var sql = "EXEC dbo.BatchFileHistorySearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationMode) +
                ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.BatchFileHistoryId) +
                //", " + ToSQLParameter(BaseDataModel.BaseDataColumns.UpdatedDate, data.UpdatedDate) +
                ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.UpdatedByPersonId) +
                ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.BatchFileId) +
                ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.BatchFileSetId);


            var oDT = new DataAccess.DBDataTable("BatchFileHistory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<BatchFileHistoryDataModel> GetEntityDetails(BatchFileHistoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.BatchFileSearch ";

            var parameters =
            new
            {
                    AuditId                              = requestProfile.AuditId
                ,   ApplicationId                        = requestProfile.ApplicationId
                ,   ReturnAuditInfo                      = returnAuditInfo
                ,   BatchFileHistoryId                   = dataQuery.BatchFileHistoryId
                ,   UpdatedByPersonId                    = dataQuery.UpdatedByPersonId
                ,   BatchFileId                          = dataQuery.BatchFileId
                ,   BatchFileSetId                       = dataQuery.BatchFileSetId
            };

            List<BatchFileHistoryDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<BatchFileHistoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Save

        private static string Save(BatchFileHistoryDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.BatchFileHistoryInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.BatchFileHistoryUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.BatchFileHistoryId) +
                        ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.BatchFileId) +
                        ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.BatchFileSetId) +
                        ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.BatchFileStatusId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.UpdatedDate, data.UpdatedDate) +
                        ", " + ToSQLParameter(data, BatchFileHistoryDataModel.DataColumns.UpdatedByPersonId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(BatchFileHistoryDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new BatchFileHistoryDataModel();
            doesExistRequest.Name = data.Name;

            var list = Search(doesExistRequest, requestProfile);
            return list.Rows.Count > 0;
        }

        #endregion

    }
}
