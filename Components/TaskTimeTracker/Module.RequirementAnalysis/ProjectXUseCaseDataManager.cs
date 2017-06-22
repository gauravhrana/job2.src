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
    public partial class ProjectXUseCaseDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ProjectXUseCaseDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ProjectXUseCase");
        }

        #region GetList

        public static List<ProjectXUseCaseDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ProjectXUseCaseDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static ProjectXUseCaseDataModel GetDetails(ProjectXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Get By Project Use Case Status

        public static DataTable GetByProjectUseCaseStatus(int projectUseCaseStatusId, int auditId)
        {
            var sql = "EXEC ProjectXUseCaseSearch @ProjectUseCaseStatusId       =" + projectUseCaseStatusId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }


        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(ProjectXUseCaseDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ProjectXUseCaseInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ProjectXUseCaseUpdate  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;

            }
            sql = sql + ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId);

            return sql;

        }

        #endregion CreateOrUpdate

        #region Create

        public static void Create(ProjectXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("ProjectXUseCase.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update
        public static void Update(ProjectXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");

            Framework.Components.DataAccess.DBDML.RunSQL("ProjectXUseCase.Update", sql, DataStoreKey);
        }
        #endregion Update

        #region Delete

        public static void Delete(ProjectXUseCaseDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProjectXUseCaseDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId);

            Framework.Components.DataAccess.DBDML.RunSQL("ProjectXUseCase.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ProjectXUseCaseDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId:
                    if (data.ProjectXUseCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId, data.ProjectXUseCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId);
                    }
                    break;

                case ProjectXUseCaseDataModel.DataColumns.ProjectId:
                    if (data.ProjectId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectXUseCaseDataModel.DataColumns.ProjectId, data.ProjectId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXUseCaseDataModel.DataColumns.ProjectId);
                    }
                    break;

                case ProjectXUseCaseDataModel.DataColumns.UseCaseId:
                    if (data.UseCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectXUseCaseDataModel.DataColumns.UseCaseId, data.UseCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXUseCaseDataModel.DataColumns.UseCaseId);
                    }
                    break;

                case ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId:
                    if (data.ProjectUseCaseStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId, data.ProjectUseCaseStatusId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId);
                    }
                    break;

                case ProjectXUseCaseDataModel.DataColumns.Project:
                    if (data.Project != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectXUseCaseDataModel.DataColumns.Project, data.Project);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXUseCaseDataModel.DataColumns.Project);
                    }
                    break;

                case ProjectXUseCaseDataModel.DataColumns.UseCase:
                    if (data.UseCase != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectXUseCaseDataModel.DataColumns.UseCase, data.UseCase);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXUseCaseDataModel.DataColumns.UseCase);
                    }
                    break;

                case ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatus:
                    if (data.ProjectUseCaseStatus != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatus, data.ProjectUseCaseStatus);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatus);
                    }
                    break;
                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static List<ProjectXUseCaseDataModel> GetEntityDetails(ProjectXUseCaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.MilestoneSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   ProjectXUseCaseId             = dataQuery.ProjectXUseCaseId
                ,   ProjectId                    = dataQuery.ProjectId
                ,   UseCaseId               = dataQuery.UseCaseId
            };

            List<ProjectXUseCaseDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ProjectXUseCaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }


        public static DataTable Search(ProjectXUseCaseDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ProjectXUseCaseSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectId) +
                ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.UseCaseId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ProjectXUseCase.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(ProjectXUseCaseDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ProjectXUseCaseInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                    ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ProjectXUseCaseUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId) +
                        ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectId) +
                        ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.UseCaseId) +
                        ", " + ToSQLParameter(data, ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId);
            return sql;
        }

        #endregion

    }
}
