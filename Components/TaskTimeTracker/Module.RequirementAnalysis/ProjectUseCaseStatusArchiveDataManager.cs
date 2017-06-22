using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using System.Globalization;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer.DataModel.RequirementAnalysis;

namespace TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis
{
    public partial class ProjectUseCaseStatusArchiveDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = ""; 

        static ProjectUseCaseStatusArchiveDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ProjectUseCaseStatusArchive");
        }

        #region GetList

        public static List<ProjectUseCaseStatusArchiveDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ProjectUseCaseStatusArchiveDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static ProjectUseCaseStatusArchiveDataModel GetDetails(ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<ProjectUseCaseStatusArchiveDataModel> GetEntityDetails(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProjectUseCaseStatusArchiveSearch " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                       ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
                      ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId);

            var result = new List<Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel();

                    dataItem.ProjectUseCaseStatusArchiveId   = Convert.ToInt32(dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId]);
                    dataItem.UseCase                         = Convert.ToString(dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase]);
                    dataItem.Project                         = Convert.ToString(dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project]);
                    dataItem.ProjectUseCaseStatus            = Convert.ToString(dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus]);
                    dataItem.Memo                            = Convert.ToString(dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Memo]);
                    //dataItem.KnowledgeDate                 = Convert.ToDateTime(dbReader[DataColumns.KnowledgeDate]);					
                    dataItem.AcknowledgedBy                  = Convert.ToString(dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedBy]);
                    dataItem.AcknowledgedById                = (int?)dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedById];
                    dataItem.ProjectUseCaseStatusId          = (int?)dbReader[Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId];
                    /*dataItem.UpdatedDate                   = Convert.ToDateTime(dbReader[BaseColumns.UpdatedDate]);
                    dataItem.UpdatedBy                       = Convert.ToString(dbReader[BaseColumns.UpdatedBy]);
                    dataItem.LastAction                      = Convert.ToString(dbReader[BaseColumns.LastAction]);
                    */
                    result.Add(dataItem);
                }
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("ProjectUseCaseStatusArchive.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("ProjectUseCaseStatusArchive.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProjectUseCaseStatusArchiveDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId);

            Framework.Components.DataAccess.DBDML.RunSQL("ProjectUseCaseStatusArchive.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId:
                    if (data.ProjectUseCaseStatusArchiveId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId, data.ProjectUseCaseStatusArchiveId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.RecordDate:
                    if (data.RecordDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.RecordDate, data.RecordDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.RecordDate);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase:
                    if (!string.IsNullOrEmpty(data.UseCase))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase, data.UseCase);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project:
                    if (!string.IsNullOrEmpty(data.Project))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project, data.Project);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus:
                    if (!string.IsNullOrEmpty(data.ProjectUseCaseStatus))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus, data.ProjectUseCaseStatus);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId:
                    if (data.ProjectUseCaseStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId, data.ProjectUseCaseStatusId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Memo:
                    if (!string.IsNullOrEmpty(data.Memo))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Memo, data.Memo);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Memo);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.KnowledgeDate:
                    if (data.KnowledgeDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.KnowledgeDate);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedById:
                    if (data.AcknowledgedById != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedById, data.AcknowledgedById);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedById);

                    }
                    break;

                case Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedBy:
                    if (!string.IsNullOrEmpty(data.AcknowledgedBy))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedBy);

                    }
                    break;

            }
            return returnValue;
        }

        public static DataTable Search(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ProjectUseCaseStatusArchiveSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ProjectUseCaseStatusArchive.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchHistory(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ProjectUseCaseStatusArchiveSearchHistory " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ProjectUseCaseStatusArchive.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ProjectUseCaseStatusArchiveInsert  ";
                    break;

                case "Update":
                    sql += "dbo.ProjectUseCaseStatusArchiveUpdate  ";
                    break;

                default:
                    break;

            }

            sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.RecordDate) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Memo) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.KnowledgeDate) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedById) +
                ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.AcknowledgedBy);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProjectUseCaseStatusArchiveSearch " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                        ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusArchiveId) +
                        ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase) +
                        ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.Project) +
                        ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus) +
                        ", " + ToSQLParameter(data, Components.BusinessLayer.DataModel.RequirementAnalysis.ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatusId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ProjectUseCaseStatusArchive.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion
    }

}
