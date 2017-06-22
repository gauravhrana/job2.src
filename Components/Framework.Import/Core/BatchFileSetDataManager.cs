using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Import
{
    public partial class BatchFileSetDataManager : StandardDataManager
    {

        static readonly string DataStoreKey = "";

        static BatchFileSetDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("BatchFileSet");
        }

        #region ToSQLParameter



        public static string ToSQLParameter(BatchFileSetDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case BatchFileSetDataModel.DataColumns.BatchFileSetId:
                    if (data.BatchFileSetId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileSetDataModel.DataColumns.BatchFileSetId, data.BatchFileSetId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileSetDataModel.DataColumns.BatchFileSetId);
                    }
                    break;

                case BatchFileSetDataModel.DataColumns.CreatedByPersonId:
                    if (data.CreatedByPersonId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileSetDataModel.DataColumns.CreatedByPersonId, data.CreatedByPersonId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileSetDataModel.DataColumns.CreatedByPersonId);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        #endregion

        #region GetList

        public static List<BatchFileSetDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(BatchFileSetDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static BatchFileSetDataModel GetDetails(BatchFileSetDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        #endregion

        #region GetEntitySearch

        public static List<BatchFileSetDataModel> GetEntityDetails(BatchFileSetDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.BatchFileSetSearch ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationMode = requestProfile.ApplicationModeId
                ,
                ApplicationId = dataQuery.ApplicationId
                ,
                BatchFileSetId = dataQuery.BatchFileSetId
                ,
                Name = dataQuery.Name
                ,
                CreatedByPersonId = dataQuery.CreatedByPersonId

            };

            List<BatchFileSetDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<BatchFileSetDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }



            return result;
        }

        #endregion

        #region Create

        public static void Create(BatchFileSetDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("BatchFileSet.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(BatchFileSetDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("BatchFileSet.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(BatchFileSetDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.BatchFileSetDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                BatchFileSetId = dataQuery.BatchFileSetId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(BatchFileSetDataModel data, RequestProfile requestProfile, int applicationMode = 0)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(BatchFileSetDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.BatchFileSetInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.BatchFileSetUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, BatchFileSetDataModel.DataColumns.BatchFileSetId) +
                         ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                       ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);
            //", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.CreatedDate) +
            //", " + ToSQLParameter(data, BatchFileSetDataModel.DataColumns.CreatedByPersonId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(BatchFileSetDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new BatchFileSetDataModel();
            doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(BatchFileSetDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.BatchFileSetChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                           ", " + ToSQLParameter(data, BatchFileSetDataModel.DataColumns.BatchFileSetId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(BatchFileSetDataModel data, RequestProfile requestProfile)
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
