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

    }
}
