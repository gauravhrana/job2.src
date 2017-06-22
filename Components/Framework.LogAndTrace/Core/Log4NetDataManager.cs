using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Reflection;

namespace Framework.Components.LogAndTrace
{
	public partial class Log4NetDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static Log4NetDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Log4Net");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(Log4NetDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{

				case Log4NetDataModel.DataColumns.Id:
					if (data.Id != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, Log4NetDataModel.DataColumns.Id, data.Id);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Id);
					}
					break;

				case Log4NetDataModel.DataColumns.ExcludeApplicationId:
					if (data.ExcludeApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, Log4NetDataModel.DataColumns.ExcludeApplicationId, data.ExcludeApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.ExcludeApplicationId);
					}
					break;

				case Log4NetDataModel.DataColumns.NoOfRecords:
					if (data.NoOfRecords != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, Log4NetDataModel.DataColumns.NoOfRecords, data.NoOfRecords);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.NoOfRecords);
					}
					break;

				case Log4NetDataModel.DataColumns.LogUser:
					if (!string.IsNullOrEmpty(data.LogUser))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.LogUser, data.LogUser.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.LogUser);
					}
					break;

				case Log4NetDataModel.DataColumns.ConnectionKey:
					if (!string.IsNullOrEmpty(data.ConnectionKey))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.ConnectionKey, data.ConnectionKey.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.ConnectionKey);
					}
					break;

				case Log4NetDataModel.DataColumns.Computer:
					if (!string.IsNullOrEmpty(data.Computer))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.Computer, data.Computer.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Computer);
					}
					break;

				case Log4NetDataModel.DataColumns.Date:
					if (data.Date != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.Date, data.Date.ToString());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Date);
					}
					break;

				case Log4NetDataModel.DataColumns.CleanupDate:
					if (data.CleanupDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.CleanupDate, data.CleanupDate.ToString());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.CleanupDate);
					}
					break;

				case Log4NetDataModel.DataColumns.Level:
					if (!string.IsNullOrEmpty(data.Level))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.Level, data.Level.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Level);
					}
					break;

				case Log4NetDataModel.DataColumns.Thread:
					if (!string.IsNullOrEmpty(data.Thread))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.Thread, data.Thread.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Thread);
					}
					break;

				case Log4NetDataModel.DataColumns.Logger:
					if (!string.IsNullOrEmpty(data.Logger))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.Logger, data.Logger.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Logger);
					}
					break;

				case Log4NetDataModel.DataColumns.Message:
					if (!string.IsNullOrEmpty(data.Message))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.Message, data.Message.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Message);
					}
					break;

				case Log4NetDataModel.DataColumns.Exception:
					if (!string.IsNullOrEmpty(data.Exception))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, Log4NetDataModel.DataColumns.Exception, data.Exception.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, Log4NetDataModel.DataColumns.Exception);
					}
					break;
			}

			return returnValue;
		}

		#endregion

		#region GetList

        public static List<Log4NetDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(Log4NetDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Get Sequential

		public static DataTable GetSequential(Log4NetDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.Log4NetSequentialSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.LogUser) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.NoOfRecords) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.ConnectionKey) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.ExcludeApplicationId);

			var oDT = new DBDataTable("Log4Net.Sequential", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get Elapsed Time Records

		public static DataTable GetElapsedTimeRecords(Log4NetDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.Log4NetElapsedTimeSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.ConnectionKey) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.LogUser) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Computer);
				
			var oDT = new DBDataTable("Log4Net.ElapsedTime", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetListLast

		public static DataTable GetListLast(Log4NetDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.Log4NetListLast" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.LogUser) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.NoOfRecords) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.ConnectionKey) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.ExcludeApplicationId);

			var oDT = new DBDataTable("Log4Net.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

        public static Log4NetDataModel GetDetails(Log4NetDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<Log4NetDataModel> GetEntityDetails(Log4NetDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.Log4NetSearch ";

			var parameters =
			new
			{
				    AuditId         = requestProfile.AuditId
				,   ApplicationId   = requestProfile.ApplicationId
				,   Id              = dataQuery.Id
				,   Logger          = dataQuery.Logger
				,   ConnectionKey   = dataQuery.ConnectionKey
				,	Computer		= dataQuery.Computer
				,   LogUser         = dataQuery.LogUser
				,	Exception		= dataQuery.Exception
				,	Level			= dataQuery.Level
				,	Date			= dataQuery.Date
				,	Message			= dataQuery.Message
				,   ReturnAuditInfo = returnAuditInfo
			};

			List<Log4NetDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<Log4NetDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		public static List<string> GetComputerDetails(RequestProfile requestProfile)
		{
            List<string> result = new List<string>();

            result.Add("IVR-DEV-12");
            result.Add("IVR-DEV-09");
            result.Add("IVR-DEV-19");
            result.Add("IVR-DEV-41");
            result.Add("IVR-DEV-05");
            result.Add("IVR-DEV-02");
            result.Add("IVR-DEV-17");

            return result;

            //getting timeout error on executing the above SQL query.
            //const string sql = @"SELECT DISTINCT TOP 10  a.Computer FROM Log4Net a WHERE a.Computer <> '' AND a.Message  LIKE 'Elapsed Milliseconds%' ";

            //using (var dataAccess = new DataAccessBase(DataStoreKey))
            //{
            //    var result = dataAccess.Connection.Query<string>(sql, null, commandType: CommandType.Text).ToList();
            //}
		}

		public static List<string> GetConnectionKeyList(RequestProfile requestProfile)
		{
            List<string> result = new List<string>();

            result.Add("AuthenticationAndAuthorization");
            result.Add("Configuration");
            result.Add("TimeEntry");
            result.Add("ApplicationDevelopment");
            result.Add("CommonServices");
            result.Add("TestCaseManagement");
            result.Add("ProjectPlanning");
            result.Add("TaskTimeTracker");
            result.Add("LoggingAndTrace");
            result.Add("TaskAndWorkflow");
			result.Add("CapitalMarkets");
			result.Add("Legal");
			result.Add("DayCare"); 

            return result;

            //getting timeout error on executing the above SQL query.
            //const string sql = @"SELECT DISTINCT TOP 10  a.ConnectionKey FROM Log4Net a WHERE a.ConnectionKey <> '' AND a.Message  LIKE 'Elapsed Milliseconds%' ";

            //using (var dataAccess = new DataAccessBase(DataStoreKey))
            //{
            //    var result = dataAccess.Connection.Query<string>(sql, null, commandType: CommandType.Text).ToList();

            //    return result;
            //}
		}

		#region Create

		public static int Create(Log4NetDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			var Log4NetId = DBDML.RunScalarSQL("Log4Net.Insert", sql, DataStoreKey);
			return Convert.ToInt32(Log4NetId);
		}

		#endregion

		#region Update

		public static void Update(Log4NetDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("Log4Net.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(Log4NetDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.Log4NetDelete ";

			var parameters = new
			{
					AuditId = requestProfile.AuditId
				,	Id = data.Id
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		public static void Cleanup(int cleanupDays, RequestProfile requestProfile)
		{
			var data = new Log4NetDataModel();
			data.CleanupDate = DateTime.Now.AddDays(cleanupDays * -1);
			var sql = "EXEC dbo.Log4NetCleanup " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.CleanupDate);

			DBDML.RunSQL("Log4Net.Delete", sql, DataStoreKey);
		}

		#endregion

		#region Search

		public static DataTable Search(Log4NetDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		public static DataTable SearchWithPaging(Log4NetDataModel data, int pageIndex, int pageSize, string orderBy, string orderByDirection, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.Log4NetSearch1 " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.PageIndex, pageIndex) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.PageSize, pageSize) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.OrderBy, orderBy) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.OrderByDirection, orderByDirection) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Id) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Logger) +
				", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.LogUser);

			var oDT = new DBDataTable("Log4Net.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(Log4NetDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.Log4NetInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.Log4NetUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Id) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.LogUser) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Date) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Level) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Thread) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Logger) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Message) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.ConnectionKey) +
						", " + ToSQLParameter(data, Log4NetDataModel.DataColumns.Exception);
			return sql;
		}

		#endregion

        public static void BackupLog4Net(DateTime recordDate)
        {
            var sourceTable = "Log4Net";
            var SQLKey = ".SQL.MoveLog4NetByDate.sql";

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

            DBDML.RunSQL("Log4Net.Backup", sql, DataStoreKey);
        }
        
	}
}
