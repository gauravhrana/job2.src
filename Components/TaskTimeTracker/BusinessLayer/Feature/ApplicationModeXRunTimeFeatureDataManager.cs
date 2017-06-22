using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
    public partial class ApplicationModeXRunTimeFeatureDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ApplicationModeXRunTimeFeatureDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXRunTimeFeature");
        }

        public static string ToSQLParameter(ApplicationModeXRunTimeFeatureDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeXRunTimeFeatureId:
                    if (data.ApplicationModeXRunTimeFeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeXRunTimeFeatureId, data.ApplicationModeXRunTimeFeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeXRunTimeFeatureId);
                    }
                    break;

                case ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeId:
                    if (data.ApplicationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeId, data.ApplicationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeId);
                    }
                    break;

                case ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeatureId:
                    if (data.RunTimeFeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeatureId, data.RunTimeFeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeatureId);
                    }
                    break;

                case ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationMode:
                    if (!string.IsNullOrEmpty(data.ApplicationMode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationMode, data.ApplicationMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationMode);
                    }
                    break;

                case ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeature:
                    if (!string.IsNullOrEmpty(data.RunTimeFeature))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeature, data.RunTimeFeature);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeature);
                    }
                    break;
            }

            return returnValue;
        }

        #region Create By ApplicationMode

        public static void CreateByApplicationMode(int applicationModeId, int[] runTimeFeatureIds, RequestProfile requestProfile)
        {
            foreach (int runTimeFeatureId in runTimeFeatureIds)
            {
                var sql = "EXEC ApplicationModeXRunTimeFeatureInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @ApplicationModeId					=   " + applicationModeId +
                            ",      @RunTimeFeatureId				=   " + runTimeFeatureId;

                DBDML.RunSQL("ApplicationModeXRunTimeFeatureInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By RunTimeFeature

        public static void CreateByRunTimeFeature(int runTimeFeatureId, int[] applicationModeIds, RequestProfile requestProfile)
        {
            foreach (int applicationModeId in applicationModeIds)
            {
                var sql = "EXEC ApplicationModeXRunTimeFeatureInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @ApplicationModeId					=   " + applicationModeId +
                            ",      @RunTimeFeatureId				=   " + runTimeFeatureId;
                DBDML.RunSQL("ApplicationModeXRunTimeFeatureInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By RunTimeFeature

        public static DataTable GetByRunTimeFeature(int runTimeFeatureId, RequestProfile requestProfile)
        {
            var sql = "EXEC ApplicationModeXRunTimeFeatureSearch @RunTimeFeatureId     =" + runTimeFeatureId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                         ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Get By ApplicationMode

        public static DataTable GetByApplicationMode(int applicationModeId, RequestProfile requestProfile)
        {
            var sql = "EXEC ApplicationModeXRunTimeFeatureSearch @ApplicationModeId       =" + applicationModeId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                         ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationModeXRunTimeFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationModeXRunTimeFeatureDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeXRunTimeFeatureId);

            DBDML.RunSQL("ApplicationModeXRunTimeFeature.Delete", sql, DataStoreKey);
        }

        #endregion

        #region GetDetails

        public static ApplicationModeXRunTimeFeatureDataModel GetDetails(ApplicationModeXRunTimeFeatureDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Search

        public static DataTable Search(ApplicationModeXRunTimeFeatureDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ApplicationModeXRunTimeFeatureSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeXRunTimeFeatureId) +
                ", " + ToSQLParameter(data, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeId) +
                ", " + ToSQLParameter(data, ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeatureId);

            var oDT = new DBDataTable("ApplicationModeXRunTimeFeature.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<ApplicationModeXRunTimeFeatureDataModel> GetEntityDetails(ApplicationModeXRunTimeFeatureDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ApplicationModeXRunTimeFeatureSearch ";

            var parameters =
            new
            {
                    AuditId                                      = requestProfile.AuditId
                ,   ApplicationId                                = requestProfile.ApplicationId
                ,   ReturnAuditInfo                              = returnAuditInfo
                ,   ApplicationModeXRunTimeFeatureId             = dataQuery.ApplicationModeXRunTimeFeatureId
                ,   ApplicationModeId                            = dataQuery.ApplicationModeId
                ,   RunTimeFeatureId                             = dataQuery.RunTimeFeatureId
            };

            List<ApplicationModeXRunTimeFeatureDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ApplicationModeXRunTimeFeatureDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }


        #endregion

        #region Create

        public static void Create(ApplicationModeXRunTimeFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DBDML.RunSQL("ApplicationModeXRunTimeFeature.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationModeXRunTimeFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("ApplicationModeXRunTimeFeature.Update", sql, DataStoreKey);
        }

        #endregion

        #region Save

        private static string Save(ApplicationModeXRunTimeFeatureDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationModeXRunTimeFeatureInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationModeXRunTimeFeatureUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeXRunTimeFeatureId) +
                        ", " + ToSQLParameter(data, ApplicationModeXRunTimeFeatureDataModel.DataColumns.RunTimeFeatureId) +
                        ", " + ToSQLParameter(data, ApplicationModeXRunTimeFeatureDataModel.DataColumns.ApplicationModeId);
            return sql;
        }

        #endregion

        #region Delete By RunTimeFeature

        public static void DeleteByRunTimeFeature(int runTimeFeatureId, int auditId)
        {
            var sql = "EXEC ApplicationModeXRunTimeFeatureDelete @runTimeFeatureId       =" + runTimeFeatureId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By ApplicationMode

        public static void DeleteByApplicationMode(int ApplicationModeId, int auditId)
        {
            var sql = "EXEC ApplicationModeXRunTimeFeatureDelete @ApplicationModeId		=" + ApplicationModeId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
