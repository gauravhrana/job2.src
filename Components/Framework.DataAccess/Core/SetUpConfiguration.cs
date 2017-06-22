using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace Framework.Components.DataAccess
{
	public class SetupConfiguration 
	{
		private static string DataStoreKey;
		internal static int ApplicationId = -99999;

		private static Dictionary<string, string> ConnectionKeyDictionary { get; set; }

		// local list of dynamically loaded connection strings related to the applicationId
		public static Dictionary<string, ConnectionStringSettings> ConnectionStrings;

		public static string UserMachineName = String.Empty;

		static SetupConfiguration()
		{
			ApplicationId = int.Parse(ConfigurationManager.AppSettings["StartupApplicationId"]);
		}

		private static ConnectionStringSettings BuildConnectionString(DataRow dr)
		{
			var name = (string)dr["Name"];

			var providerName = (string)dr["ProviderName"];

			var connectionString = string.Format(
				"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connect Timeout=0"
				, dr["DataSource"]
				, dr["InitialCatalog"]
				, dr["UserName"]
				, dr["Password"]
			);

			return new ConnectionStringSettings(name, connectionString, providerName);
		}

		private static void LoadConnectionStrings(int auditId)
		{
			ConnectionStrings = new Dictionary<string, ConnectionStringSettings>();

			// get application related connection string ids from ConnectionString X Application
			var sql = "EXEC dbo.ConnectionStringXApplicationSearch " +
					"	@AuditId		= " + auditId +
					",	@ApplicationId  = " + ApplicationId;

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			sql = "EXEC ConnectionStringSearch " +
					"	@AuditId				= " + auditId;

			// Get All Connection Strings
			var oConnectionList = new DBDataTable("Get List", sql, DataStoreKey);

			// Get related Connection String for each Connection String Id Record
			foreach (DataRow dr in oDT.DBTable.Rows)
			{
				var rows = oConnectionList.DBTable.Select("ConnectionStringId = " + dr["ConnectionStringId"]);

				if (rows.Length > 0)
				{
					// build connection string
					var connectionStringSetting = BuildConnectionString(rows[0]);
					ConnectionStrings.Add(connectionStringSetting.Name, connectionStringSetting);

					//System.Configuration.ConfigurationManager.ConnectionStrings.Add(connectionStringSetting);
				}
			}
		}

		public static void SetConnectionList(int auditId)
		{
			if (string.IsNullOrEmpty(DataStoreKey))
			{
				DataStoreKey = GetConnectionString("Configuration");
			}

			var sql = "EXEC dbo.SetUpConfigurationSearch " +
						" @ApplicationId = " + ApplicationId +
						", @AuditId = 5";

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			ConnectionKeyDictionary = new Dictionary<string, string>();

			for (var i = 0; i < oDT.DBTable.Rows.Count; i++)
			{
				var row = oDT.DBTable.Rows[i];
				var key = row["EntityName"].ToString();
				var value = row["ConnectionKeyName"].ToString();

				if (!ConnectionKeyDictionary.ContainsKey(row.ItemArray[0].ToString()))
				{
					ConnectionKeyDictionary.Add(key, value);
				}
			}

			LoadConnectionStrings(auditId);
		}

		#region GetList

		public static DataTable GetList()
		{
			if (string.IsNullOrEmpty(DataStoreKey))
			{
				DataStoreKey = GetConnectionString("Configuration");
			}

			var sql = "EXEC dbo.SetUpConfigurationSearch " +
						" @ApplicationId = " + ApplicationId;

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			ConnectionKeyDictionary = new Dictionary<string, string>();

			for (var i = 0; i < oDT.DBTable.Rows.Count; i++)
			{
				var key = oDT.DBTable.Rows[i].ItemArray[0].ToString();
				var value = oDT.DBTable.Rows[i].ItemArray[1].ToString();

				if (!ConnectionKeyDictionary.ContainsKey(key))
				{
					ConnectionKeyDictionary.Add(value, value);
				}
			}

			return oDT.DBTable;
		}

		public static DataTable GetList(int applicationId, string connectionKeyName = "")
		{
			var sql=string.Empty;

			if (string.IsNullOrEmpty(DataStoreKey))
			{
				DataStoreKey = GetConnectionString("Configuration");
			}

			sql = "EXEC dbo.SetUpConfigurationSearch ";

			if (!string.IsNullOrEmpty(connectionKeyName))
			{
				sql += " @ConnectionKeyName='" + connectionKeyName + "'";
			}
			else
			{
				sql += " @ConnectionKeyName=NULL";
			}

			if (applicationId != -1)
			{
				sql += ", @ApplicationId = " + applicationId;
			}
			else
			{
				sql += ", @ApplicationId=NULL";
			}
			var oDT = new DBDataTable("Get List", sql, DataStoreKey);			

			return oDT.DBTable;
		}

		public static DataTable GetListConnectionKeyName(int auditId)
		{
			if (string.IsNullOrEmpty(DataStoreKey))
			{
				DataStoreKey = GetConnectionString("Configuration");
			}

			var sql = "EXEC dbo.SetUpConfigurationListConnectionKeyName @AuditId = " + auditId;
			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static DataTable Search(string connectionKeyName, int auditId)
		{
			if (string.IsNullOrEmpty(DataStoreKey))
			{
				DataStoreKey = GetConnectionString("Configuration");
			}

			var sql = "EXEC dbo.SetUpConfigurationSearch @ConnectionKeyName ='" + connectionKeyName + "', @AuditId = " + auditId;

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}
		#endregion

		#region Search

		public static DataTable Search(string entityName, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.SetUpConfigurationSearch " +
				"@AuditId="+requestProfile.AuditId +					
					",@EntityName='"+entityName+"'";
					
			var oDT = new DBDataTable("SetUpConfiguration.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion


		public static string GetDataStoreKey(string entityName)
		{
			var connectionKey = String.Empty;

			ConnectionKeyDictionary.TryGetValue(entityName, out connectionKey);

			// should cause errors as key was not found ...
			return connectionKey;
		}

		private static string GetConnectionString(string connectionStringName)
		{
			// check in web.config Connection String collection
			var connString = ConfigurationManager.ConnectionStrings;

			if (connString.Count > 0)
			{
				if (connString[connectionStringName] != null)
				{
					return connString[connectionStringName].ConnectionString;
				}
			}

			// check in locally loaded Connection String collection
			if (ConnectionStrings != null && ConnectionStrings.Count > 0)
			{
				if (ConnectionStrings[connectionStringName] != null)
				{
					return ConnectionStrings[connectionStringName].ConnectionString;
				}
			}

			return string.Empty;
		}

		#region Create

		public static void Create(int applicationId, string entityName, string connectionKey, RequestProfile requestProfile)
		{
			var sql = "EXEC SetupConfigurationInsert " +
					"	@AuditId		= " + requestProfile.AuditId +
					",	@ApplicationId  = " + applicationId +
					",	@EntityName  = '" + entityName +
					"',	@ConnectionKeyName  = '" + connectionKey +"'";

			DBDML.RunSQL("SetupConfiguration_Insert", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(int setupConfigurationId, RequestProfile requestProfile)
		{
			var sql = "EXEC SetupConfigurationDelete " +
					"	@AuditId		= " + requestProfile.AuditId +
					",	@SetupConfigurationId  = " + setupConfigurationId;					

			DBDML.RunSQL("SetupConfiguration_Delete", sql, DataStoreKey);
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(string entityName, RequestProfile requestProfile)
		{
			return Search(entityName, requestProfile);
		}
		#endregion

	}
}
