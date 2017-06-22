using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{
	public partial class UserLoginHistoryDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static UserLoginHistoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UserLoginHistory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UserLoginHistoryDataModel data, string dataColumnUserName)
		{
			var returnValue = "NULL";

			switch (dataColumnUserName)
			{

				case UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId:
					if (data.UserLoginHistoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId, data.UserLoginHistoryId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId);

					}
					break;

				case UserLoginHistoryDataModel.DataColumns.UserName:
					if (!string.IsNullOrEmpty(data.UserName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginHistoryDataModel.DataColumns.UserName, data.UserName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.UserName);

					}
					break;

				case UserLoginHistoryDataModel.DataColumns.URL:
					if (!string.IsNullOrEmpty(data.URL))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginHistoryDataModel.DataColumns.URL, data.URL.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.URL);

					}
					break;

				case UserLoginHistoryDataModel.DataColumns.DateVisited:
					if (data.DateVisited != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginHistoryDataModel.DataColumns.DateVisited, data.DateVisited);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.DateVisited);

					}
					break;

				case UserLoginHistoryDataModel.DataColumns.ServerName:
					if (!string.IsNullOrEmpty(data.ServerName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginHistoryDataModel.DataColumns.ServerName, data.ServerName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.ServerName);

					}
					break;

				case UserLoginHistoryDataModel.DataColumns.UserId:
					if (data.UserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserLoginHistoryDataModel.DataColumns.UserId, data.UserId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.UserId);

					}
					break;

				case UserLoginHistoryDataModel.DataColumns.FromSearchDate:
					if (data.FromSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginHistoryDataModel.DataColumns.FromSearchDate, data.FromSearchDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.FromSearchDate);

					}
					break;

				case UserLoginHistoryDataModel.DataColumns.ToSearchDate:
					if (data.ToSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserLoginHistoryDataModel.DataColumns.ToSearchDate, data.ToSearchDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginHistoryDataModel.DataColumns.ToSearchDate);

					}
					break;


			}
			return returnValue;
		}

		#endregion

		#region GetDetails

        public static UserLoginHistoryDataModel GetDetails(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<UserLoginHistoryDataModel> GetEntityDetails(UserLoginHistoryDataModel dataQuery, RequestProfile requestProfile, int applicationModeId = 0)
		{
			const string sql = @"dbo.UserLoginHistorySearch ";

			var parameters =
			new
			{
				    AuditId                 = requestProfile.AuditId
				,   UserLoginHistoryId      = dataQuery.UserLoginHistoryId
				,   ReturnAuditInfo         = ReturnAuditInfoOnDetails
			};

			List<UserLoginHistoryDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<UserLoginHistoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("UserLoginHistory.Insert", sql, DataStoreKey);

			UserLoginHistoryMongoDbDataManager.Create(data, requestProfile);
		}

		#endregion

		#region Update

		public static void Update(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("UserLoginHistory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.UserLoginHistoryDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				UserLoginHistoryId = data.UserLoginHistoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Search

		public static List<UserLoginHistoryDataModel> GetEntityDetails(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UserLoginHistorySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserName) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.FromSearchDate) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.ToSearchDate);

			var result = new List<UserLoginHistoryDataModel>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new UserLoginHistoryDataModel();

					dataItem.UserLoginHistoryId = (int)dbReader[UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId];
					dataItem.UserId = (int)dbReader[UserLoginHistoryDataModel.DataColumns.UserId];
					dataItem.UserName = dbReader[UserLoginHistoryDataModel.DataColumns.UserName].ToString();
					dataItem.ServerName = dbReader[UserLoginHistoryDataModel.DataColumns.ServerName].ToString();
					dataItem.URL = dbReader[UserLoginHistoryDataModel.DataColumns.URL].ToString();
					dataItem.DateVisited = (DateTime)dbReader[UserLoginHistoryDataModel.DataColumns.DateVisited];

					//SetBaseInfo(dataItem, dbReader);

					result.Add(dataItem);
				}
			}

			return result;
		}

		public static DataTable Search(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.UserLoginHistorySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserName) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.FromSearchDate) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.ToSearchDate);

			var oDT = new DBDataTable("UserLoginHistory.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(UserLoginHistoryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UserLoginHistoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UserLoginHistoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.URL) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.ServerName) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserName) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserId) +
				", @Date = '" + DateTime.Now + "'";

			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UserLoginHistorySearch " +
			" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
			", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId);

			var oDT = new DBDataTable("UserLoginHistory.DoesExist", sql, DataStoreKey);
			return oDT.DBTable.Rows.Count > 0;
		}

		#endregion

		#region Report

		public static DataTable LoginDetails(decimal strFromDate, decimal strToDate)
		{
			// formulate SQL
			var sql = "EXEC dbo.LoginSearch " +
				"  @FromDate = '" + strFromDate + "'" +
				", @ToDate = '" + strToDate + "'";

			var oDT = new DBDataTable("UserLoginHistory.Report", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetLastUrls

		public static List<UserLoginHistoryDataModel> GetLastUrls(UserLoginHistoryDataModel data, int numOfRecords, RequestProfile requestProfile)
		{
			var SQLKey = ".SQL.GetLastUrls.sql";

			// Get SQL Template and replace parameters
			var assembly = Assembly.GetExecutingAssembly();
			var scriptTemplate = GetResourceText(assembly, SQLKey);

			var replacementFieldSet = new Dictionary<string, string>();
			var replacementOtherSet = new Dictionary<string, string>();

			replacementOtherSet.Add("@numOfRecords@", numOfRecords.ToString(CultureInfo.InvariantCulture));
			replacementOtherSet.Add("@UserId@", data.UserId.ToString());

			var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

			// get data 
			var result = new List<UserLoginHistoryDataModel>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new UserLoginHistoryDataModel();

					dataItem.UserLoginHistoryId = (int)dbReader[UserLoginHistoryDataModel.DataColumns.UserLoginHistoryId];
					dataItem.UserId = (int)dbReader[UserLoginHistoryDataModel.DataColumns.UserId];
					dataItem.UserName = dbReader[UserLoginHistoryDataModel.DataColumns.UserName].ToString();
					dataItem.ServerName = dbReader[UserLoginHistoryDataModel.DataColumns.ServerName].ToString();
					dataItem.URL = dbReader[UserLoginHistoryDataModel.DataColumns.URL].ToString();
					dataItem.DateVisited = (DateTime)dbReader[UserLoginHistoryDataModel.DataColumns.DateVisited];

					result.Add(dataItem);
				}
			}

			result.Reverse();

			return result;
		}

		#endregion

        public static void BackupUserLoginHistory(DateTime recordDate)
        {
            var sourceTable = "UserLoginHistory";
            var SQLKey = ".SQL.MoveUserLoginHistoryByDate.sql";

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

            DBDML.RunSQL("UserLoginHistory.Backup", sql, DataStoreKey);
        }

	}
}
