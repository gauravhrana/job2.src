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
	public partial class ThemeDataManager : StandardDataManager
	{
        public static List<ThemeDataModel> GetThemeList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ThemeDataModel.Empty, requestProfile, 0);
        }               

		public static DataTable ListOfParentThemeOnly(ThemeDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.ThemesListOfParentMenuOnly " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("Theme.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}
	}
}
