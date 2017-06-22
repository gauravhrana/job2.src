using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace Framework.Components.DataAccess
{
    [DebuggerStepThrough]
    public class DBDataReader : IDisposable
    {
	    public SqlDataReader DBReader;
		private SqlCommand DBCommand;
		private SqlConnection Connection;

        public object ReturnValue;

        #region DBDataReader
		public DBDataReader(string process, string sql, string connectionKey)
        {
			var formattedSql = DBDataTable.FormatSql(sql);

            try
            {
				Log4Net.SqlStatementStack.Push(new Log4Net.SqlStatementStackMessage(formattedSql));
				Log4Net.LogDebug(formattedSql, connectionKey);

				Connection = StartUp.OpenConnection(connectionKey);

				DBCommand = new SqlCommand(formattedSql, Connection);
				DBCommand.CommandTimeout = StartUp.TimeOut;

                DBReader = DBCommand.ExecuteReader(CommandBehavior.Default);
            }
            catch (Exception ex)
            {
	            LogException(formattedSql, String.Empty, ex, process);
            }

			LogInfo(sql, "Reader");
        }
        #endregion DBDataReader

        #region DBDataReader
        public DBDataReader(string process, string sql, string connectionKey, Boolean singleRow)
        {
			var formattedSql = DBDataTable.FormatSql(sql);
			Log4Net.SqlStatementStack.Push(new Log4Net.SqlStatementStackMessage(formattedSql));
			Log4Net.LogDebug(formattedSql, connectionKey);

			Connection = StartUp.OpenConnection(connectionKey);

			DBCommand                = new SqlCommand(formattedSql, Connection);
            DBCommand.CommandTimeout = StartUp.TimeOut;
			ReturnValue              = string.Empty;

            try
            {
                DBReader = DBCommand.ExecuteReader(CommandBehavior.SingleRow);

                DBReader.Read();

	            for (var i = 0; i < DBReader.FieldCount; i++)
	            {
					ReturnValue += DBReader[i] + "|";
	            }

            }
            catch
            {
                try
                {
					if (Connection.State == ConnectionState.Closed) Connection.Open();
	                DBReader = DBCommand.ExecuteReader(CommandBehavior.SingleRow);
                }
                catch (Exception ex)
				{
					LogException(formattedSql, String.Empty, ex, process);
                }
            }
        }
        #endregion DBDataReader

        #region DBDataReader
        public DBDataReader(string process, string sql, string connectionKey,  Boolean singleRow, Boolean SingleValue)
        {
			var formattedSql = DBDataTable.FormatSql(sql);
			Log4Net.SqlStatementStack.Push(new Log4Net.SqlStatementStackMessage(formattedSql));
			Log4Net.LogDebug(formattedSql, connectionKey);

			Connection = StartUp.OpenConnection(connectionKey);

			DBCommand = new SqlCommand(formattedSql, Connection);
            DBCommand.CommandTimeout = StartUp.TimeOut;

            try
            {
	            ReturnValue = DBCommand.ExecuteScalar();
            }
            catch
            {
                try
                {
					if (Connection.State == ConnectionState.Closed) Connection.Open();
                    ReturnValue = DBCommand.ExecuteScalar();
                }
                catch (Exception ex)
				{
					LogException(sql, "Scalar", ex, process);
                }
            }

			LogInfo(sql, "Scalar");

        }
        #endregion DBDataReader

	    private static void LogInfo(string sql, string mode)
	    {
		    if (sql.StartsWith("exec AuditDetailInsert") || sql.StartsWith("exec AuditSummaryUpdate") || sql.StartsWith("exec AuditSummaryInsert")) return;

			MessageLog("DBDataReader.Constructor(): Ran " + mode + " Sql: " + sql + "\r\n");
	    }

	    public static void MessageLog(string message)
	    {
			File.AppendAllText(StartUp.LogFile, string.Format("<item>\n\t<timestamp>{0}</timestamp>\n\t<msg>{1}</msg>\n</item>\n", DateTime.Now.ToLongTimeString(), message));
	    }

	    private static void LogException(string sql, string mode, Exception ex, string process)
	    {
			var logEntry = "DBDataReader.Constructor(): Error running reader SQL\r\n" + sql + "\r\n " + ex.Message + "\r\n";

			if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
			{
				if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
					Audit.HandleError(process, logEntry);
				else
					MessageLog(logEntry + "\t" + DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\r\n");
			}

			throw (ex);
	    }

		/// <summary>
		///		Releases used resources.
		/// </summary>
		public virtual void Dispose()
		{
			//	keep it alive till the end of current method
			GC.KeepAlive(this);

			if (DBCommand != null)
				DBCommand.Dispose();

			if (DBReader != null)
				DBReader.Close();

			//	release resources. calling Dispose also calls Close().
			if (Connection != null)
				Connection.Dispose();

			//if (m_DataAdapter != null)
			//    m_DataAdapter.Dispose();
		}
    }
}
