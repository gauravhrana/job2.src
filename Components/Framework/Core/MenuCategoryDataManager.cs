using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class MenuCategoryDataManager : StandardDataManager
	{

        #region DeleteChildren

        public static DataSet DeleteChildren(MenuCategoryDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MenuCategoryChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, MenuCategoryDataModel.DataColumns.MenuCategoryId);

            var oDt = new DBDataSet("MenuCategory.DeleteChildren", sql, DataStoreKey);

            return oDt.DBDataset;
        }

        #endregion

        public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
        {
            // get all records for old Application Id
            var sql = "EXEC dbo.MenuCategorySearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

            var oDT = new DBDataTable("MenuCategory.Search", sql, DataStoreKey);

            // formaulate a new request Profile which will have new Applicationid
            var newRequestProfile = new RequestProfile();
            newRequestProfile.ApplicationId = newApplicationId;
            newRequestProfile.AuditId = requestProfile.AuditId;

            foreach (DataRow dr in oDT.DBTable.Rows)
            {
                var data = new MenuCategoryDataModel();
                data.ApplicationId = newApplicationId;
                data.Name = dr[MenuCategoryDataModel.DataColumns.Name].ToString();

                // check for existing record in new Application Id
                if(!DoesExist(data, newRequestProfile))
                {
                    data.Description = dr[MenuCategoryDataModel.DataColumns.Description].ToString();
                    data.SortOrder = Convert.ToInt32(dr[MenuCategoryDataModel.DataColumns.SortOrder]);

                    //create in new application id
                    Create(data, newRequestProfile);

                }

            }
        }


    }
}
