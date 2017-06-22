using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class NeedXFeatureDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static NeedXFeatureDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("NeedXFeature");
        }

        public static string ToSQLParameter(NeedXFeatureDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case NeedXFeatureDataModel.DataColumns.NeedXFeatureId:
                    if (data.NeedXFeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NeedXFeatureDataModel.DataColumns.NeedXFeatureId, data.NeedXFeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedXFeatureDataModel.DataColumns.NeedXFeatureId);
                    }
                    break;

                case NeedXFeatureDataModel.DataColumns.NeedId:
                    if (data.NeedId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NeedXFeatureDataModel.DataColumns.NeedId, data.NeedId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedXFeatureDataModel.DataColumns.NeedId);
                    }
                    break;

                case NeedXFeatureDataModel.DataColumns.FeatureId:
                    if (data.FeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NeedXFeatureDataModel.DataColumns.FeatureId, data.FeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedXFeatureDataModel.DataColumns.FeatureId);
                    }
                    break;

                case NeedXFeatureDataModel.DataColumns.Need:
                    if (!string.IsNullOrEmpty(data.Need))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NeedXFeatureDataModel.DataColumns.Need, data.Need);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedXFeatureDataModel.DataColumns.Need);
                    }
                    break;

                case NeedXFeatureDataModel.DataColumns.Feature:
                    if (!string.IsNullOrEmpty(data.Feature))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NeedXFeatureDataModel.DataColumns.Feature, data.Feature);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedXFeatureDataModel.DataColumns.Feature);
                    }
                    break;
            }

            return returnValue;
        }

        #region Create By Need

        public static void Create(int needId, int[] featureIds, RequestProfile requestProfile)
        {
            foreach (int featureId in featureIds)
            {
                var sql = "EXEC NeedXFeatureInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @NeedId					=   " + needId +
                            ",      @FeatureId				=   " + featureId;

                DBDML.RunSQL("NeedXFeatureInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By Feature

        public static void CreateByFeature(int featureId, int[] needIds, RequestProfile requestProfile)
        {
            foreach (int needId in needIds)
            {
                var sql = "EXEC NeedXFeatureInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @NeedId					=   " + needId +
                            ",      @FeatureId				=   " + featureId;
                DBDML.RunSQL("NeedXFeatureInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By Feature

        public static DataTable GetByFeature(int featureId, int auditId)
        {
            var sql = "EXEC NeedXFeatureSearch @FeatureId     =" + featureId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Get By Need

        public static DataTable GetByNeed(int needId, int auditId)
        {
            var sql = "EXEC NeedXFeatureSearch @NeedId       =" + needId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Delete

        public static void Delete(NeedXFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.NeedXFeatureDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, NeedXFeatureDataModel.DataColumns.NeedXFeatureId);

            DBDML.RunSQL("NeedXFeature.Delete", sql, DataStoreKey);
        }

        #endregion

        #region GetDetails

        public static NeedXFeatureDataModel GetDetails(NeedXFeatureDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        #endregion

        #region Search

        public static DataTable Search(NeedXFeatureDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.NeedXFeatureSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, NeedXFeatureDataModel.DataColumns.NeedXFeatureId) +
                ", " + ToSQLParameter(data, NeedXFeatureDataModel.DataColumns.NeedId) +
                ", " + ToSQLParameter(data, NeedXFeatureDataModel.DataColumns.FeatureId);

            var oDT = new DBDataTable("NeedXFeature.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        static public List<NeedXFeatureDataModel> GetEntityDetails(NeedXFeatureDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.NeedXFeatureSearch ";

            var parameters =
            new
            {
                    AuditId                                         = requestProfile.AuditId
                ,   ApplicationId                                   = requestProfile.ApplicationId
                ,   ApplicationMode                                 = requestProfile.ApplicationModeId
                ,   NeedXFeatureId                                  = dataQuery.NeedXFeatureId
                ,   NeedId                                          = dataQuery.NeedId
                ,   FeatureId                                       = dataQuery.FeatureId
            };

            List<NeedXFeatureDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<NeedXFeatureDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Delete By Feature

        public static void DeleteByFeature(int featureId, int auditId)
        {
            var sql = "EXEC NeedXFeatureDelete @FeatureId       =" + featureId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By Need

        public static void DeleteByNeed(int needId, int auditId)
        {
            var sql = "EXEC NeedXFeatureDelete @NeedId		=" + needId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
