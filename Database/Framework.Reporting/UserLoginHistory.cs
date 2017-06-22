using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;

namespace Framework.Components.LogAndTrace
{
	public partial class UserLoginHistory : DataAccess.BaseClass
	{

		static readonly string DataStoreKey = "";
		static readonly int ApplicationId;

		static UserLoginHistory()
		{
			ApplicationId = SetupConfiguration.ApplicationId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UserLoginHistory");
		}

		#region GetList

		static public DataTable GetList(int auditId)
		{
			var sql = "EXEC dbo.UserLoginHistoryList" +
				" " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
				", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId);

			var oDT = new DataAccess.DBDataTable("UserLoginHistory.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		static public DataTable GetDetails(Data data, int auditId)
		{
			var sql = "EXEC dbo.UserLoginHistoryDetails " +
				" " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
				", " + data.ToSQLParameter(DataColumns.UserLoginHistoryId);

			var oDT = new DataAccess.DBDataTable("UserLoginHistory.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Create

		static public void Create(Data data, int auditId)
		{
			var sql = Save(data, auditId, "Create");
			DataAccess.DBDML.RunSQL("UserLoginHistory.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		static public void Update(Data data, int auditId)
		{
			var sql = Save(data, auditId, "Update");
			DataAccess.DBDML.RunSQL("UserLoginHistory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		static public void Delete(Data data, int auditId)
		{
			var sql = "EXEC dbo.UserLoginHistoryDelete " +
				" " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
				", " + data.ToSQLParameter(DataColumns.UserLoginHistoryId);

			DataAccess.DBDML.RunSQL("UserLoginHistory.Delete", sql, DataStoreKey);
		}

		#endregion

		#region Search

		static public DataTable Search(Data data, int auditId)
		{
			// formulate SQL
			var sql = "EXEC dbo.UserLoginHistorySearch " +
				" " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
				", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId) +
				", " + data.ToSQLParameter(DataColumns.UserLoginHistoryId) +
				", " + data.ToSQLParameter(DataColumns.UserName) +
				", " + data.ToSQLParameter(DataColumns.FromSearchDate) +
				", " + data.ToSQLParameter(DataColumns.ToSearchDate);

			var oDT = new DataAccess.DBDataTable("UserLoginHistory.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion		
		

		#region Save

		static private string Save(Data data, int auditId, string sqlcmd)
		{
			var sql = "EXEC ";

			switch (sqlcmd)
			{
				case "Create":
					sql += "dbo.UserLoginHistoryInsert  " +
						" " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
						", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId);
					break;

				case "Update":
					sql += "dbo.UserLoginHistoryUpdate  " +
						" " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + data.ToSQLParameter(DataColumns.UserLoginHistoryId) +						
				", " + data.ToSQLParameter(DataColumns.URL) +
				", " + data.ToSQLParameter(DataColumns.UserName) +
				", " + data.ToSQLParameter(DataColumns.UserId)+
				", @Date = '" + DateTime.Now + "'";

			return sql;
		}

		#endregion

		#region DoesExist

		static public DataTable DoesExist(Data data, int auditId)
		{
			var sql = "EXEC dbo.UserLoginHistoryDoesExist " +
			" " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
			", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId) +
				", " + data.ToSQLParameter(DataColumns.UserLoginHistoryId);

			var oDT = new DataAccess.DBDataTable("UserLoginHistory.DoesExist", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Report

		static public DataTable LoginDetails(decimal strFromDate, decimal strToDate)
		{
			// formulate SQL
			var sql = "EXEC dbo.LoginDetails " +
				"  @FromDate = '" + strFromDate + "'" +
				", @ToDate = '" + strToDate + "'";

			var oDT = new DataAccess.DBDataTable("UserLoginHistory.Report", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion



	}
}
