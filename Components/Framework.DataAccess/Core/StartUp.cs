using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Framework.Components.DataAccess
{
    public class StartUp
    {
        //public static System.Data.SqlClient.SqlConnection SqlConn = null;
        public static Audit FeedsAudit = null;
        public static string LogFile = null;

        public static int ApplicationId = 1;
        //public static string ConnectionString = @"Data Source=IVR-SQL-01\SQL01;Initial Catalog=TaskTimeTracker;Persist Security Info=True;User ID=706;Password=Welcome1;Connect Timeout=0";

        public static string SourceComputer = Environment.MachineName;

        public static int TimeOut = 2400;

        static StartUp()
        {
            var path = @"C:\Temp\";
            var timeout = 2400;
			//string connectionString = GetConnectionString("Default");

            EntryPoint(path, timeout, "Default");
        }

        public static string GetConnectionString(string key)
        {
            var connStrings = ConfigurationManager.ConnectionStrings;

			//SetupConfiguration.SetConnectionList(5);

			var connStrings2 = SetupConfiguration.ConnectionStrings;

			//var connStrings = ConfigurationManager.ConnectionStrings;

            if (string.IsNullOrEmpty(key))
            {
                //throw new Exception("Entity Entry missing in SetupConfiguration");
            }
			else if (connStrings[key] != null)
			{
				return connStrings[key].ConnectionString;
			}
			else if (key.Contains("Initial Catalog"))
			{
				return key;
			}

			if (SetupConfiguration.ConnectionStrings != null && SetupConfiguration.ConnectionStrings.ContainsKey(key))
			{
				return SetupConfiguration.ConnectionStrings[key].ConnectionString;
			}
			else
			{
				return connStrings["Default"].ConnectionString;
			}
        }

		public static SqlConnection OpenConnection(string connectionKey)
		{
			var connectionString = GetConnectionString(connectionKey);
			var connection = new SqlConnection(connectionString);

			if (connection.State == ConnectionState.Open)
			    connection.Close();

			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}

			return connection;
		}

		//public static void Create(string sql)
		//{
		//	//var data = new Common.Components.DataAccess.DataLog.Data();

		//	//data.ApplicationId = StartUp.ApplicationId;
		//	//data.ConnectionString = StartUp.ConnectionString;
		//	//data.SourceComputer = StartUp.SourceComputer;
		//	//data.DataMessage = sql;

		//	//Common.Components.DataAccess.DataLog.Create(data);
		//}

        /// <summary>
        /// Establish Connection to database and set file setting references
        /// </summary>
        /// <param name="path"></param>
        /// <param name="timeout"></param>
        /// <param name="connectionString"></param>
		public static void EntryPoint(string path, int timeout, string connectionKey)
        {
            var LogPath = path + "Log";
			LogFile = LogPath + "\\" + connectionKey + DateTime.Now.ToString("MMddyyhhmmss") + ".txt";

            // create Log dirctory 
			if (!Directory.Exists(LogPath))
			{
				//try
				//{
					Directory.CreateDirectory(LogPath);
				//}
				//catch (Exception ex)
				//{
				//	try
				//	{
				//		File.AppendAllText(@"c:\temp\Error.txt", "EntryPoint(): Error creating directory " + ex.Message);
				//	}
				//	catch
				//	{
				//	}

				//	return;
				//}
			}

			TimeOut = timeout;
			FeedsAudit = new Audit();

            // open connection
			//try
			//{
			//	using (var connection = new SqlConnection(GetConnectionString(connectionKey)))
			//	{
			//		connection.Open();
			//		// Execute 
			//		connection.Close();
			//	}
			//}
			//catch (Exception ex)
			//{
			//	File.AppendAllText(LogFile, "EntryPoint(): Error opening connection " + ex.Message); return;
			//}
        }

    }
}