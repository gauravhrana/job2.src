using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace Framework.Components.DataAccess
{
    [DebuggerStepThrough]
    public class DBDataSet
    {
		public static bool LogResultIsOkay = true;

        private SqlDataAdapter DBAdapter;
        public DataSet DBDataset = new DataSet();
		private SqlCommand DBCommand;

        #region DBDataSet

        public DBDataSet(string process, string sql, string connectionKey)
        {
            try
            {
                GetData(sql, connectionKey);

				//if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate"))
				//{
				//	StartUp.FeedsAudit.AuditDetailInsert(process, "OK", "Retrieved data: " + sql);
				//}

            }
            catch (Exception ex)
			{
				var logEntry = "SqlDBDataTable.Constructor(): Error fetching data\r\n" + sql + "\r\n" + ex.Message + "\r\n";

                Audit.HandleError(process, logEntry);

				Log4Net.LogError(sql, ex, connectionKey);

				var exp = new Exception(ex.Message + "\n" + sql, ex);

				throw exp;
            }
        }
		#endregion DBDataSet

		public static void LogResultCount(DataTable dt)
		{
			if (!LogResultIsOkay) return;

			var resultCount = "Result Count: " + Environment.NewLine;

			resultCount += dt.TableName + " => " + dt.Rows.Count;

			Log4Net.LogDebug(resultCount);
		}

		public static void LogResultCount(DataSet ds)
		{
			if (!LogResultIsOkay) return;

			var resultCount = "Result Count: " + Environment.NewLine;

			foreach (DataTable table in ds.Tables)
		    {
			    resultCount += table.TableName + " => " + table.Rows.Count + Environment.NewLine;
		    }

			Log4Net.LogDebug(resultCount);
	    }

		public void GetData(string sql, string connectionKey)
        {
			var formattedSql = DBDataTable.FormatSql(sql);

			Log4Net.SqlStatementStack.Push(new Log4Net.SqlStatementStackMessage(formattedSql));
			Log4Net.LogDebug(formattedSql, connectionKey);

			var connectionString = StartUp.GetConnectionString(connectionKey);

			using (var connection = new SqlConnection(connectionString))
			{

				var dataAdapter = new SqlDataAdapter(formattedSql, connection);

				dataAdapter.Fill(DBDataset);
				DBDataset.CaseSensitive = false;
			}

			LogResultCount(DBDataset);

            if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
            {
				DBDataReader.MessageLog("SQL: " + sql);
            }
        }
    }
}

//public sealed class RichErrorDbConnection : ProfiledDbConnection
//{
//	#if DEBUG
//	DbConnection connection;
//	MiniProfiler profiler;
//	#endif

//	public RichErrorDbConnection(DbConnection connection, MiniProfiler profiler) : base(connection, profiler)
//	{
//		#if DEBUG
//		this.connection = connection;
//		this.profiler = profiler;
//		#endif
//	}

//	#if DEBUG
//	protected override DbCommand CreateDbCommand()
//	{
//		return new RichErrorCommand(connection.CreateCommand(), connection, profiler);
//	}
//	#endif
//}

//public class RichErrorCommand : ProfiledDbCommand
//{
//	public RichErrorCommand(DbCommand cmd, DbConnection conn, IDbProfiler profiler) : base(cmd, conn, profiler)
//	{
//	}

//	void LogCommandAsError(Exception e, SqlExecuteType type)
//	{
//		var formatter = new SqlServerFormatter();
//		var timing = new SqlTimingParameter(this, type, null);

//		e.Data["SQL"] = formatter.FormatSql(timing);
//	}

//	public override int ExecuteNonQuery()
//	{
//		try
//		{
//			return base.ExecuteNonQuery();
//		}
//		catch (DbException e)
//		{
//			LogCommandAsError(e, SqlExecuteType.NonQuery);
//			throw;
//		}
//	}

//	protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
//	{
//		try
//		{
//			return base.ExecuteDbDataReader(behavior);
//		}
//		catch (DbException e)
//		{
//			LogCommandAsError(e, SqlExecuteType.Reader);
//			throw;
//		}
//	}

//	public override object ExecuteScalar()
//	{
//		try
//		{
//			return base.ExecuteScalar();
//		}
//		catch (DbException e)
//		{
//			LogCommandAsError(e, SqlExecuteType.Scalar);
//			throw;
//		}
//	}
//}