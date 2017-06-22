using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ProductivityAreaFeatureXApplicationUserDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";
      
        static ProductivityAreaFeatureXApplicationUserDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ProductivityAreaFeatureXApplicationUser");
        }

        #region Create By ProductivityArea

        public static void Create(int applicationUserId, int[] productivityAreaFeatureIds, RequestProfile requestProfile)
        {
            foreach (int productivityAreaFeatureId in productivityAreaFeatureIds)
            {
                var sql = "EXEC ProductivityAreaFeatureXApplicationUserInsert " +
                           "@ApplicationUserId                               = " + applicationUserId + ", " +
                           "@ProductivityAreaFeatureId                        = " + productivityAreaFeatureId + ", " +
                           "@ApplicationId                                    = " + requestProfile.ApplicationId + ", " +
                           "@AuditId                                          = " + requestProfile.AuditId;

                DBDML.RunSQL("ProductivityAreaFeatureXApplicationUser_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By ProductivityAreaFeatures

        public static void CreateByProductivityAreaFeature(int productivityAreaFeatureId, int[] applicationUserIds, RequestProfile requestProfile)
        {
            foreach (int applicationUserId in applicationUserIds)
            {
                var sql = "EXEC ProductivityAreaFeatureXApplicationUserInsert " +
                          "@ApplicationUserId						=" + applicationUserId + ", " +
                          "@ProductivityAreaFeatureId				=" + productivityAreaFeatureId + ", " +
                          "@ApplicationId			                =" + requestProfile.ApplicationId + ", " +
                          "@AuditId						            =" + requestProfile.AuditId;
                DBDML.RunSQL("ProductivityAreaFeatureXApplicationUser_Insert", sql, DataStoreKey);
            }
        }
        #endregion

        #region Get By ProductivityAreaFeature

        public static DataTable GetByProductivityAreaFeature(int productivityAreaFeatureId, int auditId)
        {
            var sql = "EXEC ProductivityAreaFeatureXApplicationUserSearch @ProductivityAreaFeatureId	=" + productivityAreaFeatureId + ", " +
                          "@AuditId									=" + auditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By ApplicationUser

        public static DataTable GetByApplicationUser(int applicationUserId, int auditId)
        {
            var sql = "EXEC ProductivityAreaFeatureXApplicationUserSearch @ApplicationUserId			=" + applicationUserId + ", " +
                          "@AuditId									=" + auditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete By ProductivityAreaFeature

        public static void DeleteByProductivityAreaFeature(int productivityAreaFeatureId, int auditId)
        {
            var sql = "EXEC ProductivityAreaFeatureXApplicationUserDelete @ApplicationUserId			=" + productivityAreaFeatureId + ", " +
                          "@AuditId									=" + auditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By ApplicationUser

        public static void DeleteByApplicationUser(int applicationUserId, int auditId)
        {
            var sql = "EXEC ProductivityAreaFeatureXApplicationUserDelete @ApplicationUserId		=" + applicationUserId + ", " +
                          "@AuditId										=" + auditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(Data data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProductivityAreaFeatureXApplicationUserSearch " +
                            "  @" + DataColumns.ProductivityAreaFeatureXApplicationUserId							+ "	=  " + data.ProductivityAreaFeatureXApplicationUserId.ToString() +
                            "  @" + DataColumns.ApplicationUserId													+ "	=  " + data.ApplicationUserId.ToString() +
                            ", @" + DataColumns.ProductivityAreaFeatureId											+ "	=  " + data.ProductivityAreaFeatureId.ToString() +
                            ", @" + BaseDataModel.BaseDataColumns.AuditId + "	=  " + requestProfile.AuditId.ToString();

            var oDT = new DBDataTable("ProductivityAreaFeatureXApplicationUser.DoesExist", sql, DataStoreKey);

            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion DoesExist

    }
}
