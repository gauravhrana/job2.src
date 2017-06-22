using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TestCaseManagement;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TestCaseManagement.Components.DataAccess
{
    public partial class TestSuiteXTestCaseArchiveDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static TestSuiteXTestCaseArchiveDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TestSuiteXTestCaseArchive");
        }

        #region GetList

        public static List<TestSuiteXTestCaseArchiveDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TestSuiteXTestCaseArchiveDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static TestSuiteXTestCaseArchiveDataModel GetDetails(TestSuiteXTestCaseArchiveDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Create

        public static void Create(TestSuiteXTestCaseArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            DBDML.RunSQL("TestSuiteXTestCaseArchive.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(TestSuiteXTestCaseArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("TestSuiteXTestCaseArchive.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(TestSuiteXTestCaseArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TestSuiteXTestCaseArchiveDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId);

            DBDML.RunSQL("TestSuiteXTestCaseArchive.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(TestSuiteXTestCaseArchiveDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId:
                    if (data.TestSuiteXTestCaseArchiveId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId, data.TestSuiteXTestCaseArchiveId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseId:
                    if (data.TestSuiteXTestCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseId, data.TestSuiteXTestCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseId);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteId:
                    if (data.TestSuiteId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteId, data.TestSuiteId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteId);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseId:
                    if (data.TestCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseId, data.TestCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseId);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatusId:
                    if (data.TestCaseStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatusId, data.TestCaseStatusId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatusId);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriorityId:
                    if (data.TestCasePriorityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriorityId, data.TestCasePriorityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriorityId);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.RecordDate:
                    if (data.RecordDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.RecordDate, data.RecordDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.RecordDate);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.KnowledgeDate:
                    if (data.KnowledgeDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseArchiveDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.KnowledgeDate);
                    }
                    break;


                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuite:
                    if (!string.IsNullOrEmpty(data.TestSuite))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuite, data.TestSuite);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuite);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCase:
                    if (!string.IsNullOrEmpty(data.TestCase))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCase, data.TestCase);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCase);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatus:
                    if (!string.IsNullOrEmpty(data.TestCase))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatus, data.TestCaseStatus);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatus);
                    }
                    break;

                case TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriority:
                    if (!string.IsNullOrEmpty(data.TestCasePriority))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriority, data.TestCasePriority);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriority);
                    }
                    break;

            }


            return returnValue;
        }

        public static DataTable Search(TestSuiteXTestCaseArchiveDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.TestSuiteXTestCaseArchiveSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuite) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCase) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatus) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriority) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseId);

            var oDT = new DBDataTable("TestSuiteXTestCaseArchive.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchHistory(TestSuiteXTestCaseArchiveDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.TestSuiteXTestCaseArchiveSearchHistory " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                 ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuite) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCase) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatus) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriority) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseId);


            var oDT = new DBDataTable("TestSuiteXTestCaseArchive.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(TestSuiteXTestCaseArchiveDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TestSuiteXTestCaseArchiveInsert  ";
                    break;

                case "Update":
                    sql += "dbo.TestSuiteXTestCaseArchiveUpdate  ";
                    break;

                default:
                    break;

            }

            sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +

               ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId) +
               ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatusId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriorityId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.RecordDate) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.KnowledgeDate) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuite) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCase) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatus) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriority) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseId);


            return sql;
        }

        #endregion

        public static List<TestSuiteXTestCaseArchiveDataModel> GetEntityDetails(TestSuiteXTestCaseArchiveDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @" dbo.TestSuiteXTestCaseArchiveSearch ";

            var parameters =
            new
            {
                    AuditId                               = requestProfile.AuditId
                ,   ApplicationId                         = requestProfile.ApplicationId
                ,   ReturnAuditInfo                       = returnAuditInfo
                ,   TestSuiteXTestCaseId                  = dataQuery.TestSuiteXTestCaseId
                ,   TestSuite                             = dataQuery.TestSuite
                ,   TestCase                              = dataQuery.TestCase
                ,   TestCaseStatus                        = dataQuery.TestCaseStatus
                ,   TestCasePriority                      = dataQuery.TestCasePriority
            };

            List<TestSuiteXTestCaseArchiveDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<TestSuiteXTestCaseArchiveDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }


    }
}
