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
    public partial class TestSuiteXTestCaseDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static TestSuiteXTestCaseDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TestSuiteXTestCase");
        }

        public static string ToSQLParameter(TestSuiteXTestCaseDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId:
                    if (data.TestSuiteXTestCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId, data.TestSuiteXTestCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId:
                    if (data.TestSuiteId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId, data.TestSuiteId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestCaseId:
                    if (data.TestCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseDataModel.DataColumns.TestCaseId, data.TestCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestCaseId);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId:
                    if (data.TestCaseStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId, data.TestCaseStatusId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId:
                    if (data.TestCasePriorityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId, data.TestCasePriorityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestSuite:
                    if (!string.IsNullOrEmpty(data.TestSuite))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseDataModel.DataColumns.TestSuite, data.TestSuite);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestSuite);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestCase:
                    if (!string.IsNullOrEmpty(data.TestCase))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseDataModel.DataColumns.TestCase, data.TestCase);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestCase);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatus:
                    if (!string.IsNullOrEmpty(data.TestCase))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatus, data.TestCaseStatus);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatus);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestCasePriority:
                    if (!string.IsNullOrEmpty(data.TestCasePriority))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriority, data.TestCasePriority);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriority);
                    }
                    break;

                case TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusList:
                    if (!string.IsNullOrEmpty(data.TestCaseStatusList))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusList, data.TestCaseStatusList);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusList);
                    }
                    break;
            }


            return returnValue;
        }

        #region GetList

        public static List<TestSuiteXTestCaseDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityList(TestSuiteXTestCaseDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static TestSuiteXTestCaseDataModel GetDetails(TestSuiteXTestCaseDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityList(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<TestSuiteXTestCaseDataModel> GetEntityList(TestSuiteXTestCaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @" dbo.TestSuiteXTestCaseSearch ";

            var parameters =
            new
            {
                    AuditId                               = requestProfile.AuditId
                ,   ApplicationId                         = requestProfile.ApplicationId
                ,   ReturnAuditInfo                       = returnAuditInfo
                ,   TestSuiteXTestCaseId                  = dataQuery.TestSuiteXTestCaseId
                ,   TestSuiteId                           = dataQuery.TestSuiteId
                ,   TestCaseId                            = dataQuery.TestCaseId
                ,   TestCaseStatusId                      = dataQuery.TestCaseStatusId
                ,   TestCasePriorityId                    = dataQuery.TestCasePriorityId
            };

            List<TestSuiteXTestCaseDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<TestSuiteXTestCaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(TestSuiteXTestCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            DBDML.RunSQL("TestSuiteXTestCase.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(TestSuiteXTestCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("TestSuiteXTestCase.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(TestSuiteXTestCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TestSuiteXTestCaseDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId);

            DBDML.RunSQL("TestSuiteXTestCase.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(TestSuiteXTestCaseDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.TestSuiteXTestCaseSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCaseId) +
                //", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusList) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId);

            var oDT = new DBDataTable("TestSuiteXTestCase.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(TestSuiteXTestCaseDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TestSuiteXTestCaseInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TestSuiteXTestCaseUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                         ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCaseId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(TestSuiteXTestCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TestSuiteXTestCaseSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCaseId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId) +
                ", " + ToSQLParameter(data, TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId);

            var oDT = new DBDataTable("TestSuiteXTestCase.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion


        #region Create By TestSuite

        public static void CreateByTestSuite(int testSuiteId, int[] testCaseIds, RequestProfile requestProfile)
        {
            foreach (int testCaseId in testCaseIds)
            {
                var sql = "EXEC TestSuiteXTestCaseInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @TestSuiteId					=   " + testSuiteId +
                            ",      @TestCaseId				=   " + testCaseId;

                DBDML.RunSQL("TestSuiteXTestCaseInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By TestCase

        public static void CreateByTestCase(int testCaseId, int[] testSuiteIds, RequestProfile requestProfile)
        {
            foreach (int testSuiteId in testSuiteIds)
            {
                var sql = "EXEC TestSuiteXTestCaseInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @TestSuiteId					=   " + testSuiteId +
                            ",      @TestCaseId				=   " + testCaseId;
                DBDML.RunSQL("TestSuiteXTestCaseInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By TestCase

        public static DataTable GetByTestCase(int testCaseId, RequestProfile requestProfile)
        {
            var sql = "EXEC TestSuiteXTestCaseSearch @TestCaseId     =" + testCaseId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By TestSuite

        public static DataTable GetByTestSuite(int testSuiteId, RequestProfile requestProfile)
        {
            var sql = "EXEC TestSuiteXTestCaseSearch @TestSuiteId       =" + testSuiteId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create By TestSuite

        public static void Create(int testSuiteId, int[] testCaseIds, RequestProfile requestProfile)
        {
            foreach (int testCaseId in testCaseIds)
            {
                var sql = "EXEC TestSuiteXTestCaseInsert " +
                          "@TestSuiteId=" + testSuiteId + ", " +
                          "@TestCaseId=" + testCaseId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;

                DBDML.RunSQL("TestSuiteXTestCase_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Delete By TestCase

        public static void DeleteByTestCase(int testCaseId, RequestProfile requestProfile)
        {
            var sql = "EXEC TestSuiteXTestCaseDelete @TestCaseId       =" + testCaseId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By TestSuite

        public static void DeleteByTestSuite(int testSuiteId, RequestProfile requestProfile)
        {
            var sql = "EXEC TestSuiteXTestCaseDelete @TestSuiteId		=" + testSuiteId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
