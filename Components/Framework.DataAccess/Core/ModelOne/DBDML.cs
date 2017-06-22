using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Framework.Components.DataAccess
{
    [DebuggerStepThrough]
    public class DBDML
	{
		#region Transaction
		public static void Transaction(ref string[] sql, string connectionKey)
        {
            var connection = StartUp.OpenConnection(connectionKey);

            SqlTransaction Trans;
            var DBCommand = new SqlCommand();
            DBCommand.Connection = connection;

            Trans = connection.BeginTransaction();
            DBCommand.CommandTimeout = StartUp.TimeOut;
            DBCommand.Transaction = Trans; int i = 0;

            try
            {
                for (i = 0; i < sql.Length; i++)
                {
                    if (sql[i] != null)
                    {
						DBDataReader.MessageLog("SQL: " + sql[i]);
                        DBCommand.CommandText = sql[i];
                        DBCommand.ExecuteNonQuery();
                        Log4Net.LogInfo(sql[i], connectionKey);
                    }
                }

                Trans.Commit();
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                Audit.HandleError("Sql Error", "SqlDML.Transaction(): Error running sql\r\n" + sql[i] + "\r\n" + ex.Message);
                Log4Net.LogError(sql[i], ex, connectionKey);
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion Transaction

        #region RunSQL

        public static void RunSQL(string process, string sql, string connectionKey)
        {
            sql = "\r\n" + sql.Replace("@", "\r\n @") + "\r\n";

            var connection = StartUp.OpenConnection(connectionKey);

            var dbCommand = new SqlCommand(sql);
            dbCommand.CommandTimeout = 24000;
            dbCommand.Connection = connection;

            try
            {
                Debug.WriteLine(dbCommand.CommandText);

                dbCommand.ExecuteNonQuery();

                // sucessful log to file
                if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
                {
                    StartUp.FeedsAudit.AuditDetailInsert(process, "OK", "Ran statement: " + sql);
                }

				Log4Net.LogDebug(sql, connectionKey);
            }
            catch (Exception ex)
            {
                #region log exception to file
                var logEntry = "DML.RunSQL(): Error running SQL\r\n" + sql + "\r\n " + ex.Message + "\r\n";

                if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
                {
                    Audit.HandleError(process, logEntry);
                }
                else
                {
                    try
                    {
						DBDataReader.MessageLog(logEntry + "\t" + DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString());
                    }
                    catch { }
                }
                #endregion log excption to file

				Log4Net.LogError(sql, ex, connectionKey);

                var exp = new Exception(ex.Message + "\n" + sql, ex);

                throw exp;
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
			}

            // sucessful log to file
            if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
				DBDataReader.MessageLog("SQL: " + sql);

            // var x = DateTime.Now;
            // var y = "Ran sql: " + sql + "\r\n";
            //StartUp.Create(sql);
        }

        public static void RunSQLWithParameters(string process, string sql, SqlParameter[] parameters, string connectionKey)
        {           
            var connection = StartUp.OpenConnection(connectionKey);

            var dbCommand = new SqlCommand(sql);
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandTimeout = 24000;
            dbCommand.Connection = connection;
            dbCommand.Parameters.AddRange(parameters);

            try
            {
                Debug.WriteLine(dbCommand.CommandText);

                dbCommand.ExecuteNonQuery();

                // sucessful log to file
                if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
                {
                    StartUp.FeedsAudit.AuditDetailInsert(process, "OK", "Ran statement: " + sql);
                }

				Log4Net.LogDebug(sql, connectionKey);
            }
            catch (Exception ex)
            {
                #region log exception to file
                var logEntry = "DML.RunSQL(): Error running SQL\r\n" + sql + "\r\n " + ex.Message + "\r\n";

                if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
                {
                    Audit.HandleError(process, logEntry);
                }
                else
                {
                    try
                    {
						DBDataReader.MessageLog(logEntry + "\t" + DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString());
                    }
                    catch { }
                }
                #endregion log excption to file

				Log4Net.LogError(sql, ex, connectionKey);

                var exp = new Exception(ex.Message + "\n" + sql, ex);

                throw exp;
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
			}

            // sucessful log to file
            if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
				DBDataReader.MessageLog("SQL: " + sql);

            // var x = DateTime.Now;
            // var y = "Ran sql: " + sql + "\r\n";
            //StartUp.Create(sql);
        }

        #endregion RunSQL

        #region RunScalarSQL

        public static object RunScalarSQL(string process, string sql, string connectionKey)
        {
            object result;
            sql = "\r\n" + sql.Replace("@", "\r\n @") + "\r\n";

            var connection = StartUp.OpenConnection(connectionKey);

            var dbCommand = new SqlCommand(sql);
            dbCommand.CommandTimeout = 24000;
            dbCommand.Connection = connection;
			//dbCommand.Parameters.Add("@NextValue", SqlDbType.Int).Direction = ParameterDirection.Output;

			try
			{


				result = dbCommand.ExecuteScalar();

				// sucessful log to file
				if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
				{
					StartUp.FeedsAudit.AuditDetailInsert(process, "OK", "Ran statement: " + sql);
				}

				Log4Net.LogDebug(sql, connectionKey);
			}
			catch (Exception ex)
			{
				#region log exception to file
				var logEntry = "DML.RunSQL(): Error running SQL\r\n" + sql + "\r\n " + ex.Message + "\r\n";

				if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
				{
					Audit.HandleError(process, logEntry);
				}
				else
				{
					try
					{
						DBDataReader.MessageLog(logEntry + "\t" + DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString());
					}
					catch { }
				}
				#endregion log excption to file

				Log4Net.LogError(sql, ex, connectionKey);

				var exp = new Exception(ex.Message + "\n" + sql, ex);

				throw exp;
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
			}

            // sucessful log to file
            if (!sql.StartsWith("exec AuditDetailInsert") && !sql.StartsWith("exec AuditSummaryUpdate") && !sql.StartsWith("exec AuditSummaryInsert"))
				DBDataReader.MessageLog("SQL: " + sql);

            // var x = DateTime.Now;
            // var y = "Ran sql: " + sql + "\r\n";
            //StartUp.Create(sql);

            return result;
        }

        #endregion RunSQL
    }
}