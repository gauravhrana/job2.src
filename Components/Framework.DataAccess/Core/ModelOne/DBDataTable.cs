using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace Framework.Components.DataAccess
{
    //[System.Diagnostics.DebuggerStepThrough]
    public class DBDataTable
    {
        //public SqlDataAdapter DBAdapter;
        public DataTable DBTable;
		//private SqlCommand DBCommand;
        //private SqlCommandBuilder DBCommandBuilder;
        //private int TimeOut = StartUp.TimeOut;

		#region FormatSQL

        public static string FormatSql(string sql)
        {
            sql = "\r\n" + sql.Replace("@", Environment.NewLine + "@") + "\r\n";
            return sql;
        }

		public static string FormatSQL(string sql, string connectionKey)
		{
			var database = string.Empty;
			System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
			if (connectionKey.Contains("Initial Catalog"))
			{
				builder.ConnectionString = connectionKey;
				database = builder.InitialCatalog;
			}
			else
				database = connectionKey;
			if(sql .StartsWith("EXEC dbo."))
				sql = sql.Replace("EXEC ", database + ".");
			else
				sql = sql.Replace("EXEC ", database + ".dbo.");
			return sql;
		}


        #endregion FormatSQL

        public DBDataTable(string process, string sql, string connectionKey)
        {
            try
            {
				 GetData(sql, connectionKey);
            }
            catch (Exception ex)
            {
				var fromattedSQL= FormatSQL(sql, connectionKey);

		        ex.Data.Add("SQL", fromattedSQL);

				var logEntry = "SqlDBDataTable.Constructor(): Error fetching data\r\n" + fromattedSQL + "\r\n" + ex.Message + "\r\n";

				Audit.HandleError(process, logEntry);

                Log4Net.LogError(sql, ex, connectionKey);

                throw new Exception("SQL Error in " + fromattedSQL, ex);
            }
        }

        public void GetData(string sql, string connectionKey)
        {
			var formattedSQL = FormatSQL(sql, connectionKey);

			Log4Net.SqlStatementStack.Push(new Log4Net.SqlStatementStackMessage(formattedSQL));
			Log4Net.LogDebug(formattedSQL, connectionKey);

			var connectionString = StartUp.GetConnectionString(connectionKey);

			DBTable = new DataTable();

		    using (var connection = new SqlConnection(connectionString))
		    {
			    //var conn = new ProfiledDbConnection(connection, MiniProfiler.Current)

				var dbCommand = new SqlCommand(sql, connection);
				dbCommand.CommandTimeout = StartUp.TimeOut;
				var dbAdapter = new SqlDataAdapter(dbCommand);

				//var prdataAdapter = new ProfiledDbDataAdapter(DBAdapter);
				DBTable.BeginLoadData();
				dbAdapter.Fill(DBTable);
				DBTable.EndLoadData();

				DBTable.CaseSensitive = false;

				connection.Close();
		    }

			DBDataSet.LogResultCount(DBTable);

			#region log exception
			if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
			{
				DBDataReader.MessageLog("SQL: " + formattedSQL);
			}
			#endregion log exception
        }

        //#region SaveToDatabase

        //public void SaveToDatabase()
        //{
        //    DBCommandBuilder = new SqlCommandBuilder(DBAdapter);

        //    try
        //    {
        //        DBAdapter.Update(DBTable);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Audit.HandleError("Sql Error", "SqlDBDataTable.SaveToDatabase(): " + ex.Message);
        //        return;
        //    }

        //    try
        //    {
        //        System.IO.File.AppendAllText(StartUp.LogFile, "SqlDBDataTable.SaveToDatabase(): Saved Adapter" + "\r\n");
        //    }
        //    catch
        //    {
        //    }
        //}

        //#endregion SaveToDatabase

    }
}