using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.LogAndTrace;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Reflection;

namespace Framework.Components.LogAndTrace
{
	public partial class UserLoginDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static UserLoginDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UserLogin");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UserLoginDataModel data, string dataColumnUserName)
		{
			var returnValue = "NULL";

			switch (dataColumnUserName)
			{

				case UserLoginDataModel.DataColumns.UserLoginId:
					if (data.UserLoginId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserLoginDataModel.DataColumns.UserLoginId, data.UserLoginId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.UserLoginId);

					}
					break;

				case UserLoginDataModel.DataColumns.UserName:
					if (!string.IsNullOrEmpty(data.UserName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginDataModel.DataColumns.UserName, data.UserName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.UserName);

					}
					break;

				case UserLoginDataModel.DataColumns.UserLoginStatus:
					if (!string.IsNullOrEmpty(data.UserLoginStatus))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginDataModel.DataColumns.UserLoginStatus, data.UserLoginStatus.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.UserLoginStatus);

					}
					break;

				case UserLoginDataModel.DataColumns.RecordDate:
					if (data.RecordDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserLoginDataModel.DataColumns.RecordDate, data.RecordDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.RecordDate);

					}
					break;

				case UserLoginDataModel.DataColumns.UserLoginStatusId:
					if (data.UserLoginStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserLoginDataModel.DataColumns.UserLoginStatusId, data.UserLoginStatusId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.UserLoginStatusId);

					}
					break;

				case UserLoginDataModel.DataColumns.FromSearchDate:
					if (data.FromSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginDataModel.DataColumns.FromSearchDate, data.FromSearchDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.FromSearchDate);

					}
					break;

				case UserLoginDataModel.DataColumns.ToSearchDate:
					if (data.ToSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginDataModel.DataColumns.ToSearchDate, data.ToSearchDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.ToSearchDate);

					}
					break;

				case UserLoginDataModel.DataColumns.RecordDate2:
					if (data.RecordDate2 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserLoginDataModel.DataColumns.RecordDate2, data.RecordDate2);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginDataModel.DataColumns.RecordDate2);

					}
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<UserLoginDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(UserLoginDataModel.Empty, requestProfile, 0);
		}		

		#endregion

		#region GetDetails

        public static UserLoginDataModel GetDetails(UserLoginDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<UserLoginDataModel> GetEntityDetails(UserLoginDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UserLoginSearch ";

			var parameters =
			new
			{
				    AuditId                 = requestProfile.AuditId
				,   ApplicationId           = requestProfile.ApplicationId
				,   UserLoginId             = dataQuery.UserLoginId
				,   UserLoginStatusId       = dataQuery.UserLoginStatusId
				,   UserName                = dataQuery.UserName
				,   RecordDate              = dataQuery.RecordDate
				,   RecordDate2             = dataQuery.RecordDate2
				,   ReturnAuditInfo         = returnAuditInfo
			};

			List<UserLoginDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UserLoginDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<UserLoginDataModel> GetEntityDetails(UserLoginDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.UserLoginSearch " +
		//		 " "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//		 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
		//		 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//		 ", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.UserLoginId) +
		//		 ", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.UserLoginStatusId) +
		//		 ", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.UserName) +
		//		 ", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.RecordDate) +
		//		 ", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.RecordDate2);

		//	var result = new List<UserLoginDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new UserLoginDataModel();

		//			dataItem.UserLoginId = (int)dbReader[UserLoginDataModel.DataColumns.UserLoginId];
		//			dataItem.UserLoginStatusId = (int)dbReader[UserLoginDataModel.DataColumns.UserLoginStatusId];
		//			dataItem.UserName = dbReader[UserLoginDataModel.DataColumns.UserName].ToString();
		//			dataItem.UserLoginStatus = dbReader[UserLoginDataModel.DataColumns.UserLoginStatus].ToString();
		//			dataItem.RecordDate = (int)dbReader[UserLoginDataModel.DataColumns.RecordDate];

		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion

		#region Create

		public static void Create(UserLoginDataModel data, RequestProfile requestProfile)
		{
			if (data.UserLoginStatusId == null && !string.IsNullOrEmpty(data.UserLoginStatus))
			{
				var dataStatus = new UserLoginStatusDataModel();
				dataStatus.Name = data.UserLoginStatus;

				var dt = UserLoginStatusDataManager.Search(dataStatus, requestProfile);

				if (dt.Rows.Count > 0)
				{
					data.UserLoginStatusId = Convert.ToInt32(dt.Rows[0][UserLoginDataModel.DataColumns.UserLoginStatusId]);
				}
			}

			var sql = Save(data, requestProfile, "Create");

			DBDML.RunSQL("UserLogin.Insert", sql, DataStoreKey);

			Framework.Components.LogAndTrace.UserLoginMongoDbDataManager.Create(data, requestProfile);
		}

		#endregion

		#region Update

		public static void Update(UserLoginDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("UserLogin.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(UserLoginDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.UserLoginDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				UserLoginId = data.UserLoginId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(UserLoginDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Report

		public static DataTable LoginReport(string username, decimal startdate, decimal enddate, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.UserLoginReport " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @UserName = '" + username + "'";

			var oDT = new DBDataTable("UserLogin.Report", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(UserLoginDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UserLoginInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UserLoginUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.UserLoginId) +
						", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.UserName) +
						", " + ToSQLParameter(data, UserLoginDataModel.DataColumns.UserLoginStatusId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(UserLoginDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new UserLoginDataModel();
			doesExistRequest.UserName = data.UserName;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region Report

		public static DataTable LoginDetails(decimal strFromDate, decimal strToDate)
		{
			// formulate SQL
			var sql = "EXEC dbo.LoginSearch " +
				"  @FromDate = '" + strFromDate + "'" +
				", @ToDate = '" + strToDate + "'";

			var oDT = new DBDataTable("UserLogin.Report", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

        public static void BackupUserLogin(DateTime recordDate)
        {
            var sourceTable = "UserLogin";
            var SQLKey = ".SQL.MoveUserLoginByDate.sql";

            // Get SQL Template and replace parameters
            var assembly = Assembly.GetExecutingAssembly();
            var scriptTemplate = GetResourceText(assembly, SQLKey);

            var replacementFieldSet = new Dictionary<string, string>();
            var replacementOtherSet = new Dictionary<string, string>();

            var backupTableName = string.Empty;
            backupTableName = sourceTable + "_bkp_" + recordDate.ToString("yyyyMMdd");

            replacementOtherSet.Add("@RecordDate@", recordDate.ToString());
            replacementOtherSet.Add("@BackupTableName@", backupTableName);

            var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

            DBDML.RunSQL("UserLogin.Backup", sql, DataStoreKey);
        }

	}
}
