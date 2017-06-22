using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Import;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Import
{
    public partial class FileTypeDataManager : StandardDataManager
    {
		#region DeleteChildren

		public static DataSet DeleteChildren(FileTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FileTypeChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FileTypeDataModel.DataColumns.FileTypeId);

			var oDT = new DBDataSet("FileType.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region Search

		public static DataTable Search(FileTypeDataModel data, RequestProfile requestProfile, int applicationMode = 0)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search
    }
}
