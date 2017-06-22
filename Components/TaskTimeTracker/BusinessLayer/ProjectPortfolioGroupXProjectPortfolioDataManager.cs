using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ProjectPortfolioGroupXProjectPortfolioDataManager : BaseDataManager
    {
        private static readonly string DataStoreKey = "";

        static ProjectPortfolioGroupXProjectPortfolioDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ProjectPortfolioGroupXProjectPortfolio");
        }

        #region GetList

        public static List<ProjectPortfolioGroupXProjectPortfolioDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ProjectPortfolioGroupXProjectPortfolioDataModel.Empty, requestProfile);
        }

        #endregion GetList

        #region Search

        public static string ToSQLParameter(ProjectPortfolioGroupXProjectPortfolioDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupXProjectPortfolioId:
                    if (data.ProjectPortfolioGroupXProjectPortfolioId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupXProjectPortfolioId, data.ProjectPortfolioGroupXProjectPortfolioId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupXProjectPortfolioId);
                    }
                    break;

                case ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupId:
                    if (data.ProjectPortfolioGroupId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupId, data.ProjectPortfolioGroupId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupId);
                    }
                    break;

                case ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioId:
                    if (data.ProjectPortfolioId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioId, data.ProjectPortfolioId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioId);
                    }
                    break;

                case ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.Description:
                    if (!string.IsNullOrEmpty(data.Description))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectPortfolioDataModel.DataColumns.Description, data.Description);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectPortfolioDataModel.DataColumns.Description);
                    }
                    break;

                case ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.SortOrder:
                    if (data.SortOrder != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.SortOrder, data.SortOrder);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.SortOrder);
                    }
                    break;

                case ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolio:
                    if (!string.IsNullOrEmpty(data.ProjectPortfolio))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolio, data.ProjectPortfolio);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolio);
                    }
                    break;

                case ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroup:
                    if (!string.IsNullOrEmpty(data.ProjectPortfolioGroup))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroup, data.ProjectPortfolioGroup);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroup);
                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;
            }

            return returnValue;
        }

        public static DataTable Search(ProjectPortfolioGroupXProjectPortfolioDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        #endregion Search

        #region GetDetails

        public static ProjectPortfolioGroupXProjectPortfolioDataModel GetDetails(ProjectPortfolioGroupXProjectPortfolioDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        static public List<ProjectPortfolioGroupXProjectPortfolioDataModel> GetEntityDetails(ProjectPortfolioGroupXProjectPortfolioDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ProjectPortfolioGroupXProjectPortfolioSearch ";

            var parameters =
            new
            {
                    AuditId                                         = requestProfile.AuditId
                ,   ApplicationId                                   = requestProfile.ApplicationId
                ,   ApplicationMode                                 = requestProfile.ApplicationModeId
                ,   ProjectPortfolioGroupXProjectPortfolioId        = dataQuery.ProjectPortfolioGroupXProjectPortfolioId
                ,   ProjectPortfolioGroupId                         = dataQuery.ProjectPortfolioGroupId
                ,   ProjectPortfolioId                              = dataQuery.ProjectPortfolioId
                ,   CreatedDate                                     = dataQuery.CreatedDate
                ,   ModifiedDate                                    = dataQuery.ModifiedDate
                ,   CreatedByAuditId                                = dataQuery.CreatedByAuditId
                ,   ModifiedByAuditId                               = dataQuery.ModifiedByAuditId
            };

            List<ProjectPortfolioGroupXProjectPortfolioDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ProjectPortfolioGroupXProjectPortfolioDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion GetDetails

        #region CreateOrUpdate

        private static string CreateOrUpdate(ProjectPortfolioGroupXProjectPortfolioDataModel data, RequestProfile requestProfile, string sqlcmd)
        {
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.ProjectPortfolioGroupXProjectPortfolioInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ProjectPortfolioGroupXProjectPortfolioUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupXProjectPortfolioId) +
                        ", " + ToSQLParameter(data, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupId) +
                        ", " + ToSQLParameter(data, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioId) +
                         ", " + ToSQLParameter(data, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.Description) +
                         ", " + ToSQLParameter(data, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.SortOrder);
            return sql;
        }

        #endregion CreateOrUpdate

        #region Create

        public static int Create(ProjectPortfolioGroupXProjectPortfolioDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var projectPortfolioGroupXProjectPortfolioId = DBDML.RunScalarSQL("ProjectPortfolioGroupXProjectPortfolio.Insert", sql, DataStoreKey);
            return Convert.ToInt32(projectPortfolioGroupXProjectPortfolioId);
        }

        #endregion Create

        #region Update

        public static void Update(ProjectPortfolioGroupXProjectPortfolioDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");

            DBDML.RunSQL("ProjectPortfolioGroupXProjectPortfolio.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Delete

        public static void Delete(ProjectPortfolioGroupXProjectPortfolioDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProjectPortfolioGroupXProjectPortfolioDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupXProjectPortfolioId);

            DBDML.RunSQL("ProjectPortfolioGroupXProjectPortfolio.Delete", sql, DataStoreKey);
        }

        #endregion Delete

        #region DoesExist

        public static bool DoesExist(ProjectPortfolioGroupXProjectPortfolioDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new ProjectPortfolioGroupXProjectPortfolioDataModel();
            doesExistRequest.ProjectPortfolioGroupId = data.ProjectPortfolioGroupId;
            doesExistRequest.ProjectPortfolioId = data.ProjectPortfolioId;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
        }

        #endregion DoesExist

    }
}
