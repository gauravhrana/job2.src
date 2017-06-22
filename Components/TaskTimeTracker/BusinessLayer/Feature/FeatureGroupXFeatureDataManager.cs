using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
    public partial class FeatureGroupXFeatureDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static FeatureGroupXFeatureDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FeatureGroupXFeature");
        }

        #region Create By Feature

        public static void CreateByFeature(int FeatureId, int[] FeatureGroupIds, RequestProfile requestProfile)
        {
            foreach (int FeatureGroupId in FeatureGroupIds)
            {
                var sql = "EXEC FeatureGroupXFeatureInsert " +
                          "@FeatureId						=" + FeatureId + ", " +
                          "@FeatureGroupId					=" + FeatureGroupId + ", " +
                          "@ApplicationId				=" + requestProfile.ApplicationId + ", " +
                          "@AuditId						=" + requestProfile.AuditId;

                DBDML.RunSQL("FeatureGroupXFeatureInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By FeatureGroup

        public static void CreateByFeatureGroup(int FeatureGroupId, int[] FeatureIds, RequestProfile requestProfile)
        {
            foreach (int FeatureId in FeatureIds)
            {
                var sql = "EXEC FeatureGroupXFeatureInsert " +
                          "@FeatureId		=" + FeatureId + ", " +
                          "@FeatureGroupId	=" + FeatureGroupId + ", " +
                          "@ApplicationId	    =" + requestProfile.ApplicationId + ", " +
                          "@AuditId			=" + requestProfile.AuditId;
                DBDML.RunSQL("FeatureGroupXFeatureInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By FeatureGroup

        public static DataTable GetByFeatureGroup(int FeatureGroupId, RequestProfile requestProfile)
        {
            var sql = "EXEC FeatureGroupXFeatureSearch @FeatureGroupId     =" + FeatureGroupId + ", " +
                          "@AuditId=" + requestProfile.AuditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By Feature

        public static DataTable GetByFeature(int FeatureId, RequestProfile requestProfile)
        {
            var sql = "EXEC FeatureGroupXFeatureSearch @FeatureId       =" + FeatureId + ", " +
                          "@AuditId=" + requestProfile.AuditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete By FeatureGroup

        public static void DeleteByFeatureGroup(int FeatureGroupId, RequestProfile requestProfile)
        {
            var sql = "EXEC FeatureGroupXFeatureDelete @FeatureGroupId       =" + FeatureGroupId + ", " +
                          "@AuditId=" + requestProfile.AuditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By Feature

        public static void DeleteByFeature(int FeatureId, RequestProfile requestProfile)
        {
            var sql = "EXEC FeatureGroupXFeatureDelete @FeatureId  =" + FeatureId + ", " +
                          "@AuditId								=" + requestProfile.AuditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
