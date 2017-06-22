using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
	public class Admin :  BaseDataManager
	{
        static readonly string DataStoreKey = "";

        public Admin()
        {
        }

		static Admin()
		{
			DataStoreKey = "TaskTimeTracker";
		}

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
        	var sql = "EXEC dbo.GetTestAndAuditData";
				//+
				//" " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
               
            var oDT = new DBDataTable("Get List", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion GetList
	}
}
