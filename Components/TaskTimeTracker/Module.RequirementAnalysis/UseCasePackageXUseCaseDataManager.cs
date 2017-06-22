using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis
{
    public partial class UseCasePackageXUseCaseDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static UseCasePackageXUseCaseDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UseCasePackageXUseCase");
        }

        public static string ToSQLParameter(UseCasePackageXUseCaseDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId:
                    if (data.UseCasePackageXUseCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId, data.UseCasePackageXUseCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId);
                    }
                    break;

                case UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId:
                    if (data.UseCasePackageId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId, data.UseCasePackageId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId);
                    }
                    break;

                case UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId:
                    if (data.UseCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId, data.UseCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId);
                    }
                    break;

                case UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackage:
                    if (!string.IsNullOrEmpty(data.UseCasePackage))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackage, data.UseCasePackage);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackage);
                    }
                    break;

                case UseCasePackageXUseCaseDataModel.DataColumns.UseCase:
                    if (!string.IsNullOrEmpty(data.UseCase))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCasePackageXUseCaseDataModel.DataColumns.UseCase, data.UseCase);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCasePackageXUseCaseDataModel.DataColumns.UseCase);
                    }
                    break;
            }

            return returnValue;
        }

        #region Create By UseCasePackage

        public static void CreateByUseCasePackage(int useCasePackageId, int[] useCaseIds, RequestProfile requestProfile)
        {
            foreach (int useCaseId in useCaseIds)
            {
                var sql = "EXEC UseCasePackageXUseCaseInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @UseCasePackageId					=   " + useCasePackageId +
                            ",      @UseCaseId				=   " + useCaseId;

                Framework.Components.DataAccess.DBDML.RunSQL("UseCasePackageXUseCaseInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By UseCase

        public static void CreateByUseCase(int useCaseId, int[] useCasePackageIds, RequestProfile requestProfile)
        {
            foreach (int useCasePackageId in useCasePackageIds)
            {
                var sql = "EXEC UseCasePackageXUseCaseInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @UseCasePackageId					=   " + useCasePackageId +
                            ",      @UseCaseId				=   " + useCaseId;
                Framework.Components.DataAccess.DBDML.RunSQL("UseCasePackageXUseCaseInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By UseCase

        public static DataTable GetByUseCase(int useCaseId, int auditId)
        {
            var sql = "EXEC UseCasePackageXUseCaseSearch @UseCaseId     =" + useCaseId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Get By UseCasePackage

        public static DataTable GetByUseCasePackage(int useCasePackageId, int auditId)
        {
            var sql = "EXEC UseCasePackageXUseCaseSearch @UseCasePackageId       =" + useCasePackageId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Delete

        public static void Delete(UseCasePackageXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.UseCasePackageXUseCaseDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId);

            Framework.Components.DataAccess.DBDML.RunSQL("UseCasePackageXUseCase.Delete", sql, DataStoreKey);
        }

        #endregion

        #region GetDetails

        public static UseCasePackageXUseCaseDataModel GetDetails(UseCasePackageXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion


        #region Search

        public static DataTable Search(UseCasePackageXUseCaseDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.UseCasePackageXUseCaseSearch " +
                  " " + BaseDataManager.SetCommonParametersForSearch(requestProfile.AuditId, requestProfile.ApplicationId, requestProfile.ApplicationModeId) +
                  ", " + ToSQLParameter(data, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId) +
                  ", " + ToSQLParameter(data, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId) +
                  ", " + ToSQLParameter(data, UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("UseCasePackageXUseCase.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion



        public static List<UseCasePackageXUseCaseDataModel> GetEntityDetails(UseCasePackageXUseCaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.UseCasePackageXUseCaseSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   UseCasePackageId = dataQuery.UseCasePackageId
                ,   UseCasePackageXUseCaseId = dataQuery.UseCasePackageXUseCaseId
                ,   UseCaseId               = dataQuery.UseCaseId
            };

            List<UseCasePackageXUseCaseDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<UseCasePackageXUseCaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #region Create

        public static void Create(UseCasePackageXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("UseCasePackageXUseCase.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(UseCasePackageXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("UseCasePackageXUseCase.Update", sql, DataStoreKey);
        }

        #endregion

        #region Save

        private static string Save(UseCasePackageXUseCaseDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.UseCasePackageXUseCaseInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UseCasePackageXUseCaseUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId) +
                        ", " + ToSQLParameter(data, UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId) +
                        ", " + ToSQLParameter(data, UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId);
            return sql;
        }

        #endregion

        #region Delete By UseCase

        public static void DeleteByUseCase(int useCaseId, int auditId)
        {
            var sql = "EXEC UseCasePackageXUseCaseDelete @useCaseId       =" + useCaseId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By UseCasePackage

        public static void DeleteByUseCasePackage(int UseCasePackageId, int auditId)
        {
            var sql = "EXEC UseCasePackageXUseCaseDelete @UseCasePackageId		=" + UseCasePackageId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
