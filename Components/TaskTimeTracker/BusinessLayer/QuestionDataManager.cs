using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class QuestionDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static QuestionDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("Question");
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            return GetList(requestProfile.AuditId, requestProfile.ApplicationId, DataStoreKey, "dbo.QuestionSearch");
        }

        #endregion GetList

        #region Search

        public static string ToSQLParameter(QuestionDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case QuestionDataModel.DataColumns.QuestionId:
                    if (data.QuestionId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, QuestionDataModel.DataColumns.QuestionId, data.QuestionId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuestionDataModel.DataColumns.QuestionId);

                    }
                    break;

                case QuestionDataModel.DataColumns.QuestionPhrase:
                    if (!string.IsNullOrEmpty(data.QuestionPhrase))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, QuestionDataModel.DataColumns.QuestionPhrase, data.QuestionPhrase);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuestionDataModel.DataColumns.QuestionPhrase);

                    }
                    break;

                case QuestionDataModel.DataColumns.QuestionCategoryId:
                    if (data.QuestionCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, QuestionDataModel.DataColumns.QuestionCategoryId, data.QuestionCategoryId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuestionDataModel.DataColumns.QuestionCategoryId);

                    }
                    break;

                case QuestionDataModel.DataColumns.SortOrder:
                    if (data.SortOrder != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, QuestionDataModel.DataColumns.SortOrder, data.SortOrder);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuestionDataModel.DataColumns.SortOrder);

                    }
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(QuestionDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion Search

        #region GetDetails

        public static DataTable GetDetails(QuestionDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static List<QuestionDataModel> GetEntityDetails(QuestionDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.QuestionSearch ";

            var parameters =
            new
            {
                    AuditId                     = requestProfile.AuditId
                ,   ApplicationId               = requestProfile.ApplicationId
                ,   ReturnAuditInfo             = returnAuditInfo
                ,   QuestionCategoryId          = dataQuery.QuestionCategoryId
                ,   QuestionPhrase              = dataQuery.QuestionPhrase
                ,   QuestionId                  = dataQuery.QuestionId

            };

            List<QuestionDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<QuestionDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<QuestionDataModel> GetEntityDetails(QuestionDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.QuestionSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //              ", " + ToSQLParameter(data, QuestionDataModel.DataColumns.QuestionId);

        //    var result = new List<QuestionDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new QuestionDataModel();

        //            dataItem.QuestionId		= (int?)dbReader[QuestionDataModel.DataColumns.QuestionId];
        //            dataItem.QuestionPhrase = dbReader[QuestionDataModel.DataColumns.QuestionPhrase].ToString();
        //            dataItem.Category		= dbReader[QuestionDataModel.DataColumns.Category].ToString();
        //            dataItem.QuestionCategoryId = (int?)dbReader[QuestionDataModel.DataColumns.QuestionCategoryId];
        //            dataItem.SortOrder		= (int?)dbReader[QuestionDataModel.DataColumns.SortOrder];

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion GetDetails

        #region Create

        public static void Create(QuestionDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");

            DBDML.RunSQL("Question.Insert", sql, DataStoreKey);
        }

        #endregion Create

        #region Update

        public static void Update(QuestionDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");

            DBDML.RunSQL("Question.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Delete

        public static void Delete(QuestionDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.QuestionDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                QuestionId = dataQuery.QuestionId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion Delete

        #region DoesExist

        public static DataTable DoesExist(QuestionDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new QuestionDataModel();
            doesExistRequest.QuestionId = data.QuestionId;
            doesExistRequest.QuestionPhrase = data.QuestionPhrase;

            return Search(doesExistRequest, requestProfile);
        }

        #endregion DoesExist

        #region Save

        private static string Save(QuestionDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.QuestionInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.QuestionUpdate  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, QuestionDataModel.DataColumns.QuestionId) +
                        ", " + ToSQLParameter(data, QuestionDataModel.DataColumns.QuestionPhrase) +
                        ", " + ToSQLParameter(data, QuestionDataModel.DataColumns.QuestionCategoryId) +
                        ", " + ToSQLParameter(data, QuestionDataModel.DataColumns.SortOrder);

            return sql;

        }

        #endregion Save

        #region GetChildren

        private static DataSet GetChildren(QuestionDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.QuestionChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, QuestionDataModel.DataColumns.QuestionId);

            var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(QuestionDataModel data, RequestProfile requestProfile)
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

        #region DeleteChildren

        static public DataSet DeleteChildren(QuestionDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.QuestionChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, QuestionDataModel.DataColumns.QuestionId);

            var oDT = new DBDataSet("Delete Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

    }
}
