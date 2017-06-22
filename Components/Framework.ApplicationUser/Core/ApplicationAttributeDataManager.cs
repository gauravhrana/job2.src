using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.ApplicationUser
{
    public partial class ApplicationAttributeDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ApplicationAttributeDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("Application");
        }

        #region GetList

        public static List<ApplicationAttributeDataModel> GetList(RequestProfile requestProfile)
        {
            var obj = new ApplicationAttributeDataModel();
            return GetEntityDetails(obj, requestProfile);
        }        

        #endregion

        #region GetDetails

        public static ApplicationAttributeDataModel GetDetails(ApplicationAttributeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region GetEntitySearch

        public static List<ApplicationAttributeDataModel> GetEntityDetails(ApplicationAttributeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ApplicationAttributeSearch ";

            var parameters =
            new
            {
                    AuditId = requestProfile.AuditId
                ,   ApplicationId = dataQuery.ApplicationId
                ,   ApplicationMode = requestProfile.ApplicationModeId
                ,   ReturnAuditInfo = returnAuditInfo
            };

            List<ApplicationAttributeDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ApplicationAttributeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }


            return result;
        }

        #endregion GetDetails

        #region Create

        public static void Create(ApplicationAttributeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("ApplicationAttribute.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationAttributeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationAttribute.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationAttributeDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ApplicationAttributeDelete ";

            var parameters =
            new
            {
                    AuditId = requestProfile.AuditId
                ,   ApplicationId = dataQuery.ApplicationId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ApplicationAttributeDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ApplicationAttributeDataModel.DataColumns.ApplicationId:
                    if (data.ApplicationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationAttributeDataModel.DataColumns.ApplicationId, data.ApplicationId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationAttributeDataModel.DataColumns.ApplicationId);
                    }
                    break;

                case ApplicationAttributeDataModel.DataColumns.RenderApplicationFilter:
                    if (data.RenderApplicationFilter != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationAttributeDataModel.DataColumns.RenderApplicationFilter, data.RenderApplicationFilter);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationAttributeDataModel.DataColumns.RenderApplicationFilter);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(ApplicationAttributeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(ApplicationAttributeDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationAttributeInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationAttributeUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ApplicationAttributeDataModel.DataColumns.ApplicationId) +
                        ", " + ToSQLParameter(data, ApplicationAttributeDataModel.DataColumns.RenderApplicationFilter);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ApplicationAttributeDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new ApplicationAttributeDataModel();
            doesExistRequest.ApplicationId = data.ApplicationId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }

        #endregion

    }

}
