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
    public partial class UseCaseActorXUseCaseDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static UseCaseActorXUseCaseDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UseCaseActorXUseCase");
        }

        #region GetList

        public static List<UseCaseActorXUseCaseDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(UseCaseActorXUseCaseDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static UseCaseActorXUseCaseDataModel GetDetails(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.UseCaseActorXUseCaseInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UseCaseActorXUseCaseUpdate  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId);


            return sql;

        }
        #endregion CreateOrUpdate

        #region Create

        public static void Create(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("UseCaseActorXUseCase.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update
        public static void Update(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");

            Framework.Components.DataAccess.DBDML.RunSQL("UseCaseActorXUseCase.Update", sql, DataStoreKey);
        }
        #endregion Update

        #region Delete

        public static void Delete(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.UseCaseActorXUseCaseDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId);

            Framework.Components.DataAccess.DBDML.RunSQL("UseCaseActorXUseCase.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static List<UseCaseActorXUseCaseDataModel> GetEntityDetails(UseCaseActorXUseCaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.UseCaseActorXUseCaseSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   UseCaseActorXUseCaseId             = dataQuery.UseCaseActorXUseCaseId
                ,   UseCaseId                    = dataQuery.UseCaseId
                ,   UseCaseRelationshipId                    = dataQuery.UseCaseRelationshipId
                ,   UseCaseActorId               = dataQuery.UseCaseActorId
            };

            List<UseCaseActorXUseCaseDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<UseCaseActorXUseCaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }


        public static string ToSQLParameter(UseCaseActorXUseCaseDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId:
                    if (data.UseCaseActorXUseCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId, data.UseCaseActorXUseCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId);
                    }
                    break;

                case UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId:
                    if (data.UseCaseActorId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId, data.UseCaseActorId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId);
                    }
                    break;

                case UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId:
                    if (data.UseCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId, data.UseCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId);
                    }
                    break;

                case UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId:
                    if (data.UseCaseRelationshipId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId, data.UseCaseRelationshipId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId);
                    }
                    break;

                case UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActor:
                    if (data.UseCaseActor != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActor, data.UseCaseActor);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActor);
                    }
                    break;

                case UseCaseActorXUseCaseDataModel.DataColumns.UseCase:
                    if (data.UseCase != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCaseActorXUseCaseDataModel.DataColumns.UseCase, data.UseCase);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseActorXUseCaseDataModel.DataColumns.UseCase);
                    }
                    break;

                case UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationship:
                    if (data.UseCaseRelationship != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationship, data.UseCaseRelationship);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationship);
                    }
                    break;
                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.UseCaseActorXUseCaseSearch " +
                " " + BaseDataManager.SetCommonParametersForSearch(requestProfile.AuditId, requestProfile.ApplicationId, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("UseCaseActorXUseCase.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.UseCaseActorXUseCaseInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UseCaseActorXUseCaseUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId) +
                        ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId) +
                        ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId) +
                        ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(UseCaseActorXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.UseCaseActorXUseCaseSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("UseCaseActorXUseCase.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

        #region Create By UseCase

        public static void CreateByUseCase(int useCaseId, int[] useCaseActorIds, RequestProfile requestProfile)
        {
            foreach (int useCaseActorId in useCaseActorIds)
            {
                var sql = "EXEC UseCaseActorXUseCaseInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @UseCaseId					=   " + useCaseId +
                            ",      @UseCaseActorId				=   " + useCaseActorId;

                Framework.Components.DataAccess.DBDML.RunSQL("UseCaseActorXUseCaseInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By UseCaseActor

        public static void CreateByUseCaseActor(int useCaseActorId, int[] useCaseIds, RequestProfile requestProfile)
        {
            foreach (int useCaseId in useCaseIds)
            {
                var sql = "EXEC UseCaseActorXUseCaseInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @UseCaseId					=   " + useCaseId +
                            ",      @UseCaseActorId				=   " + useCaseActorId;
                Framework.Components.DataAccess.DBDML.RunSQL("UseCaseActorXUseCaseInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By UseCaseActor

        public static DataTable GetByUseCaseActor(int useCaseActorId, int auditId)
        {
            var sql = "EXEC UseCaseActorXUseCaseSearch @UseCaseActorId     =" + useCaseActorId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By UseCase

        public static DataTable GetByUseCase(int useCaseId, int auditId)
        {
            var sql = "EXEC UseCaseActorXUseCaseSearch @UseCaseId       =" + useCaseId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create By UseCase

        public static void Create(int useCaseId, int[] useCaseActorIds, RequestProfile requestProfile)
        {
            foreach (int useCaseActorId in useCaseActorIds)
            {
                var sql = "EXEC UseCaseActorXUseCaseInsert " +
                          "@UseCaseId=" + useCaseId + ", " +
                          "@UseCaseActorId=" + useCaseActorId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;

                Framework.Components.DataAccess.DBDML.RunSQL("UseCaseActorXUseCase_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Delete By UseCaseActor

        public static void DeleteByUseCaseActor(int useCaseActorId, int auditId)
        {
            var sql = "EXEC UseCaseActorXUseCaseDelete @UseCaseActorId       =" + useCaseActorId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By UseCase

        public static void DeleteByUseCase(int useCaseId, int auditId)
        {
            var sql = "EXEC UseCaseActorXUseCaseDelete @UseCaseId		=" + useCaseId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
