using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ProductivityAreaXProductivityAreaFeatureDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";
       
		static ProductivityAreaXProductivityAreaFeatureDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ProductivityAreaXProductivityAreaFeature");
        }

        #region Create By ProductivityArea

        public static void Create(int productivityAreaId, int[] productivityAreaFeatureIds, RequestProfile requestProfile)
        {
            foreach (int productivityAreaFeatureId in productivityAreaFeatureIds)
            {
               var sql =  "EXEC ProductivityAreaXProductivityAreaFeatureInsert " +
                          "@ProductivityAreaId                               = " + productivityAreaId + ", " +
                          "@ProductivityAreaFeatureId                        = " + productivityAreaFeatureId + ", " +
                          "@ApplicationId                                    = " + requestProfile.ApplicationId + ", " +
                          "@AuditId                                          = " + requestProfile.AuditId;

                DBDML.RunSQL("ProductivityAreaXProductivityAreaFeature_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By ProductivityAreaFeatures

        public static void CreateByProductivityAreaFeature(int productivityAreaFeatureId, int[] productivityAreaIds, RequestProfile requestProfile)
        {
            foreach (int productivityAreaId in productivityAreaIds)
            {
                var sql = "EXEC ProductivityAreaXProductivityAreaFeatureInsert " +
                          "@ProductivityAreaId						=" + productivityAreaId + ", " +
                          "@ProductivityAreaFeatureId				=" + productivityAreaFeatureId + ", " +
                          "@ApplicationId			                =" + requestProfile.ApplicationId + ", " +
                          "@AuditId						            =" + requestProfile.AuditId;
                DBDML.RunSQL("ProductivityAreaXProductivityAreaFeature_Insert", sql, DataStoreKey);
            }
        }
        #endregion

        #region Get By ProductivityAreaFeature

        public static DataTable GetByProductivityAreaFeature(int productivityAreaFeatureId, int auditId)
        {
            var sql = "EXEC ProductivityAreaXProductivityAreaFeatureSearch @ProductivityAreaFeatureId			=" + productivityAreaFeatureId + ", " +
                          "@AuditId									=" + auditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By ProductivityArea

        public static DataTable GetByProductivityArea(int productivityAreaId, int auditId)
        {
            var sql = "EXEC ProductivityAreaXProductivityAreaFeatureSearch @ProductivityAreaId			=" + productivityAreaId + ", " +
                          "@AuditId									=" + auditId;
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete By ProductivityAreaFeature

        public static void DeleteByProductivityAreaFeature(int productivityAreaFeatureId, int auditId)
        {
            var sql = "EXEC ProductivityAreaXProductivityAreaFeatureDelete @ProductivityAreaId			=" + productivityAreaFeatureId + ", " +
                          "@AuditId									=" + auditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By ProductivityArea

        public static void DeleteByProductivityArea(int productivityAreaId, int auditId)
        {
            var sql = "EXEC ProductivityAreaXProductivityAreaFeatureDelete @ProductivityAreaId				=" + productivityAreaId + ", " +
                          "@AuditId										=" + auditId;
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(Data data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProductivityAreaXProductivityAreaFeatureSearch " +
                            "  @" + DataColumns.ProductivityAreaXProductivityAreaFeatureId + "	=  " + data.ProductivityAreaXProductivityAreaFeatureId.ToString() +
                            "  @" + DataColumns.ProductivityAreaId                         + "	=  " + data.ProductivityAreaId.ToString() +
                            ", @" + DataColumns.ProductivityAreaFeatureId                  + "	=  " + data.ProductivityAreaFeatureId.ToString() +
                            ", @" + BaseDataModel.BaseDataColumns.AuditId + "					=  " + requestProfile.AuditId.ToString();

            var oDT = new DBDataTable("ProductivityAreaXProductivityAreaFeature.DoesExist", sql, DataStoreKey);

            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion DoesExist

    }
}
