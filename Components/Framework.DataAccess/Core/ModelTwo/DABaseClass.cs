using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Framework.CommonServices.BusinessDomain.Utils;
using Framework.PM.Common.BusinessDomain;
using log4net;


namespace Framework.PM.Common.DataAccess
{

	/// <summary>
	/// Summary description for DASQLBase.
	/// </summary>
	public class DASQLBase : DataObject, IDisposable
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public const string DEFAULT_VALUES = "default";
		protected SqlConnection m_Connection;
		protected SqlCommand m_Command;
		protected SqlDataAdapter m_DataAdapter;

		protected SqlConnection m_parentConnection;
		protected SqlTransaction m_parentTransaction;
		protected SqlDataReader m_Reader;

		protected string m_ConnectionString;
		protected static readonly string sm_ConnectionString;
		protected static readonly string sm_ApplicationCoreConnectionString;
		protected static readonly string sm_EzeConnectionString;
		protected static readonly string sm_ApplicationResearchConnectionString;
		protected static readonly string sm_SalesLogixConnectionString;
		protected static readonly string sm_ApplicationPMLocalConnectionString;
		protected static readonly string sm_WhatsUpConnectionString;
		protected static readonly string sm_MarketDataConnectionString;
		protected static readonly string sm_DTCCDataConnectionString;

		protected static readonly string DEADLOCK_TEXT = "deadlock victim";
		protected static readonly string TIMEOUT_TEXT = "timeout period";

		protected static readonly int sqlRetryMaxAttempts = DataHelper.GetIntAppSettings("DeadLockRetryMaxAttempts", 3);
		protected static readonly int sqlRetryTimeout = DataHelper.GetIntAppSettings("DeadLockRetryTimeout", 500);

		private bool m_InBatch = false;
		private StringBuilder m_BatchText = null;
		private static readonly string sm_BeginBatch = "DECLARE @rc INT  SELECT @rc = 1 ";

		public enum TargetDB
		{
			Application        ,
			ApplicationCore    ,
			ApplicationResearch,
			SalesLogix         ,
			ApplicationPMLocal ,
			WhatsUp            ,
			MarketData         ,
			DTCC               ,
			Eze
		}

		/// <summary>
		/// Cache configurations
		/// </summary>
		static DASQLBase()
		{
			sm_ConnectionString                    = ConfigurationManager.AppSettings["ConnectionString"];
			sm_EzeConnectionString                 = ConfigurationManager.AppSettings["EzeConnectionString"];
			sm_ApplicationCoreConnectionString     = ConfigurationManager.AppSettings["ApplicationConnectionString"];
			sm_ApplicationResearchConnectionString = ConfigurationManager.AppSettings["ResearchConnectionString"];
			sm_SalesLogixConnectionString          = ConfigurationManager.AppSettings["SalesLogixConnectionString"];
			sm_ApplicationPMLocalConnectionString  = ConfigurationManager.AppSettings["ApplicationPMLocalConnectionString"];
			sm_WhatsUpConnectionString             = ConfigurationManager.AppSettings["WhatsUpConnectionString"];
			sm_MarketDataConnectionString          = ConfigurationManager.AppSettings["MarketDataConnectionString"];
			sm_DTCCDataConnectionString            = ConfigurationManager.AppSettings["DtccDataConnectionString"];
		}

		/// <summary>
		/// All connections default to the application database
		/// </summary>
		public DASQLBase() : this(TargetDB.Application)
		{
		}

		/// <summary>
		/// A helper constructor that specifies what our target DB is
		/// </summary>
		/// <param name="target"></param>
		public DASQLBase(TargetDB target) : this(ConnectionStringFromTargetDB(target))
		{
		}


		/// <summary>
		/// All purpose constructor in which the user explicitly specifies the connection string
		/// </summary>
		/// <param name="connectionString"></param>
		public DASQLBase(string connectionString)
		{
			m_Command = new SqlCommand();
			m_Command.CommandTimeout = 180; // 3 minutes is a tolerable timeout
			m_DataAdapter = new SqlDataAdapter();
			m_ConnectionString = connectionString;
		}

		public SqlConnection Connection
		{
			get
			{
				if (m_parentConnection != null)
					return m_parentConnection;
				else
				{
					OpenConnection();
					return m_Connection;
				}
			}

			set { m_Connection = value; }

		}

		public SqlCommand Command
		{
			get { return m_Command; }
		}

		/// <summary>
		///		Reuses a connection of a parent transaction. The caller is responsible for a resource cleanup.
		/// </summary>
		/// <param name="transaction">A transaction to use.</param>
		public void UseTransaction(SqlTransaction transaction)
		{
			if (transaction != null)
			{
				//	check if command already has a transaction associated with it...
				if (m_Command.Transaction != null)
					throw new ApplicationException("A command already has a transaction associated with it.");

				m_parentConnection = transaction.Connection;
				m_parentTransaction = transaction;
				m_Command.Transaction = m_parentTransaction;
			}
			else
			{
				m_parentTransaction = null;
				m_parentConnection = null;
			}
		}

		public SqlTransaction Transaction
		{
			get { return m_Command.Transaction; }
		}

		public void BeginTransaction()
		{
			if (m_parentTransaction != null)
				throw new ApplicationException("A command already has a parent transaction associated with it.");

			m_Command.Transaction = Connection.BeginTransaction();
		}

		public void Commit()
		{
			if (m_Command.Transaction != null)
				m_Command.Transaction.Commit();
		}

		public void Rollback()
		{
			if (m_Command.Transaction != null)
				m_Command.Transaction.Rollback();
		}

		public SqlParameter AddParameter(string paramName, object paramValue)
		{
			return m_Command.Parameters.AddWithValue("@" + paramName, paramValue);
		}

		public SqlParameter AddParameter(string paramName, object paramValue, ParameterDirection dir)
		{
			var param = m_Command.Parameters.AddWithValue("@" + paramName, paramValue);
			param.Direction = dir;
			return param;
		}

		public SqlParameter AddParameter(string paramName, object paramValue, SqlDbType t)
		{
			var param = m_Command.Parameters.AddWithValue("@" + paramName, paramValue);
			param.SqlDbType = t;
			return param;
		}

		public IDataParameter[] MakeParameters(KeyValuePair<string, object>[] parameters)
		{
			var dParams = new IDataParameter[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
			{
				dParams[i] = AddParameter(parameters[i].Key, parameters[i].Value);
			}

			return dParams;
		}


		public SqlParameter MakeParameter(string paramName, object paramValue)
		{
			return AddParameter(paramName, paramValue);
		}

		public SqlParameter MakeParameter(string paramName, object paramValue, ParameterDirection dir)
		{
			return AddParameter(paramName, paramValue, dir);
		}

		public SqlParameter MakeParameter(string paramName, object paramValue, SqlDbType t)
		{
			return AddParameter(paramName, paramValue, t);
		}

		/// <summary>
		/// starts a batch of sql statements to be executed in one DB trip
		/// </summary>
		public void BeginBatch()
		{
			m_InBatch = true;
			m_BatchText = new StringBuilder();

		}

		/// <summary>
		/// come out of the batch mode
		/// </summary>
		public void EndBatch()
		{
			m_InBatch = false;
			m_BatchText = null;
		}


		/// <summary>
		/// Executes a batch of sql statements added by AddBatch
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		public void ExecuteNonQueryBatch(bool useTransaction)
		{
			ExecuteNonQueryBatch(useTransaction, m_Command.CommandTimeout);

		}

		/// <summary>
		/// Executes a batch of sql statements added by AddBatch
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public void ExecuteNonQueryBatch(bool useTransaction, int commandTimeout)
		{
			if (!m_InBatch) throw new Exception("Cannot Execute batch without first calling BeginBatch()");

			if (m_BatchText.ToString() == String.Empty) return;

			PrepareCommand(CommandType.Text, sm_BeginBatch + m_BatchText);

			var oldTimeout = m_Command.CommandTimeout;
			m_Command.CommandTimeout = commandTimeout;

			try
			{
				if (useTransaction) BeginTransaction();
				m_Command.ExecuteNonQuery();
				if (useTransaction) Commit();
			}
			catch (Exception)
			{
				if (useTransaction) Rollback();
				throw;
			}
			finally
			{
				m_Command.CommandTimeout = oldTimeout;
			}
		}

		/// <summary>
		/// Executes a batch of sql statements added by AddBatch
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public object ExecuteScalarBatch(bool useTransaction)
		{
			return ExecuteScalarBatch(useTransaction, m_Command.CommandTimeout);
		}

		/// <summary>
		/// Executes a batch of sql statements added by AddBatch
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public object ExecuteScalarBatch(bool useTransaction, int commandTimeout)
		{
			if (!m_InBatch) throw new Exception("Cannot Execute batch without first calling BeginBatch()");
			if (m_BatchText.ToString() == String.Empty) return -1;
			PrepareCommand(CommandType.Text, sm_BeginBatch + m_BatchText);
			var oldTimeout = m_Command.CommandTimeout;
			m_Command.CommandTimeout = commandTimeout;


			try
			{
				object rv = null;
				if (useTransaction) BeginTransaction();
				rv = m_Command.ExecuteScalar();
				if (useTransaction) Commit();
				return rv;
			}
			catch (Exception)
			{
				if (useTransaction) Rollback();
				throw;
			}
			finally
			{
				m_Command.CommandTimeout = oldTimeout;
			}
		}

		/// <summary>
		/// Executes a batch of sql statements added by AddBatch
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public SqlDataReader ExecuteReaderBatch(bool useTransaction)
		{
			return ExecuteReaderBatch(useTransaction, m_Command.CommandTimeout);
		}

		/// <summary>
		/// Executes a batch of sql statements added by AddBatch
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public SqlDataReader ExecuteReaderBatch(bool useTransaction, int commandTimeout)
		{

			if (!m_InBatch) throw new Exception("Cannot Execute batch without first calling BeginBatch()");
			if (m_BatchText.ToString() == String.Empty) return null;

			PrepareCommand(CommandType.Text, sm_BeginBatch + m_BatchText);
			var oldTimeout = m_Command.CommandTimeout;
			m_Command.CommandTimeout = commandTimeout;
			SqlDataReader m_Reader = null;

			try
			{
				if (useTransaction) BeginTransaction();
				m_Reader = m_Command.ExecuteReader();
				if (useTransaction) Commit();
				return m_Reader;
			}
			catch (Exception)
			{
				if (useTransaction) Rollback();
				throw;
			}
			finally
			{
				m_Command.CommandTimeout = oldTimeout;
			}

		}



		/// <summary>
		/// Executes a non-select query and parses the resultset for a status message
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public ServerResponse ExecuteServerResponse(bool useTransaction)
		{
			return ExecuteServerResponse(useTransaction, m_Command.CommandTimeout);
		}

		/// <summary>
		/// Executes a query, presumably one that does updates, and parses the first row
		/// of the resultset (if any) for a status message
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public ServerResponse ExecuteServerResponse(bool useTransaction, int commandTimeout)
		{
			if (!m_InBatch) throw new Exception("Cannot Execute batch without first calling BeginBatch()");

			var response = new ServerResponse();
			if (m_BatchText.ToString() == String.Empty) return response;


			PrepareCommand(CommandType.Text, m_BatchText.ToString());
			var oldTimeout = m_Command.CommandTimeout;
			m_Command.CommandTimeout = commandTimeout;
			SqlDataReader m_Reader = null;

			try
			{
				if (useTransaction) BeginTransaction();
				var hasErrors = false;
				try
				{
					m_Reader = m_Command.ExecuteReader();

					if (m_Reader != null)
					{
						if (m_Reader.Read())
						{
							response = new ServerResponse(m_Reader);
							if (!response.IsSuccesful) hasErrors = true;
						}

					}
				}
				catch (Exception)
				{
					throw;
				}
				finally
				{
					if (m_Reader != null) m_Reader.Close();
				}

				if (useTransaction && !hasErrors) Commit();
				else if (useTransaction && hasErrors) Rollback();
				return response;

			}
			catch (Exception)
			{
				if (useTransaction) Rollback();
				throw;
			}
			finally
			{
				m_Command.CommandTimeout = oldTimeout;
			}

		}

		/// <summary>
		/// Executes a batch of sql statements, presumably update queries, and parses any number
		/// of resultsets into a collection of status messages
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public List<ServerResponse> ExecuteServerResponseBatch(bool useTransaction, int commandTimeout)
		{
			if (!m_InBatch) throw new Exception("Cannot Execute batch without first calling BeginBatch()");


			var responses = new List<ServerResponse>();
			if (m_BatchText.ToString() == String.Empty) return responses;



			PrepareCommand(CommandType.Text, sm_BeginBatch + " " + m_BatchText);
			var oldTimeout = m_Command.CommandTimeout;
			m_Command.CommandTimeout = commandTimeout;
			SqlDataReader m_Reader = null;

			try
			{
				if (useTransaction) BeginTransaction();

				var hasErrors = false;
				try
				{
					m_Reader = m_Command.ExecuteReader();

					if (m_Reader != null)
					{
						while (m_Reader.Read())
						{
							var sr = new ServerResponse(m_Reader);
							if (!sr.IsSuccesful) hasErrors = true;
							responses.Add(sr);
						}
						while (m_Reader.NextResult())
						{
							while (m_Reader.Read())
							{
								var sr = new ServerResponse(m_Reader);
								if (!sr.IsSuccesful) hasErrors = true;
								responses.Add(sr);
							}
						}

					}
				}
				finally
				{
					if (m_Reader != null) m_Reader.Close();
				}

				if (useTransaction && !hasErrors) Commit();
				else if (useTransaction && hasErrors) Rollback();
				return responses;

			}
			catch (Exception)
			{
				if (useTransaction) Rollback();
				throw;
			}
			finally
			{
				m_Command.CommandTimeout = oldTimeout;
			}

		}

		/// <summary>
		/// Executes a batch of sql statements added by AddBatch, returning a collection of server 
		/// responses
		/// </summary>
		/// <param name="useTransaction">true if using ADO.NET tran</param>
		/// <param name="commandTimeout"></param>
		public List<ServerResponse> ExecuteServerResponseBatch(bool useTransaction)
		{

			return ExecuteServerResponseBatch(useTransaction, m_Command.CommandTimeout);

		}

		/// <summary>
		/// Adds the statement to a batch.  Call Execute*Batch to complete the operation
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParams"></param>
		public void AddBatch(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			if (!m_InBatch) throw new Exception("Cannot call AddBatch without first calling BeginBatch()");
			PrepareBatchCommand(commandType, commandText, commandParams);
		}

		/// <summary>
		/// Adds the batch of this object to the specified sql base component so one DASQLBase
		/// can use code from another DASQLBase within a single batch/transaction
		/// both components must already be operating in batch mode.
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParams"></param>
		public void MergeBatch(DASQLBase da)
		{
			if (!m_InBatch) throw new Exception("Cannot call AddBatch without first calling BeginBatch()");


			da.AddBatch(CommandType.Text, m_BatchText.ToString());

			m_BatchText = new StringBuilder();
			m_InBatch = false;
		}

		public void ExecuteNonQuery(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					m_Command.ExecuteNonQuery();
					break;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}


		public void ExecuteNonQuery(CommandType commandType, string commandText, int commandTimeout, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					var oldTimeout = m_Command.CommandTimeout;
					m_Command.CommandTimeout = commandTimeout;
					m_Command.ExecuteNonQuery();
					m_Command.CommandTimeout = oldTimeout;
					break;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}


		public void ExecuteNonQuery()
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					if (m_Command.CommandText == null || m_Command.CommandText == "")
						throw new ApplicationException("CommandText is not set");
					m_Command.Connection = Connection;
					m_Command.ExecuteNonQuery();
					break;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public object ExecuteReturnParam(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					var prm = new SqlParameter("Return Value", null);
					prm.Direction = ParameterDirection.ReturnValue;

					PrepareCommand(commandType, commandText, commandParams);

					m_Command.Parameters.Add(prm);
					m_Command.ExecuteNonQuery();

					return prm.Value;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		private void LogSqlError(Exception ex)
		{
			if (logger.IsErrorEnabled)
			{

				var errorMessage = new StringBuilder(ex.Message);
				try
				{
					if (m_Command != null)
					{
						errorMessage.AppendFormat("CommandType:{0} {1}", m_Command.CommandType, m_Command.CommandText).AppendLine();
						if (m_Command.Parameters != null)
							foreach (DbParameter param in m_Command.Parameters)
								switch (param.DbType)
								{
									case DbType.Byte:
									case DbType.Boolean:
									case DbType.Currency:
									case DbType.Decimal:
									case DbType.Double:
									case DbType.Single:
									case DbType.UInt16:
									case DbType.UInt32:
									case DbType.UInt64:
									case DbType.VarNumeric:
									case DbType.Int16:
									case DbType.Int32:
									case DbType.Int64:
									case DbType.SByte:
										errorMessage.AppendFormat(",{0}={1}", param.ParameterName, param.Value).AppendLine();
										break;

									case DbType.AnsiString:
									case DbType.Date:
									case DbType.DateTime:
									case DbType.Guid:
									case DbType.String:
									case DbType.Time:
									case DbType.AnsiStringFixedLength:
									case DbType.StringFixedLength:
									case DbType.Xml:
									case DbType.DateTime2:
									case DbType.DateTimeOffset:
									default:
										errorMessage.AppendFormat(",{0}='{1}'", param.ParameterName, param.Value).AppendLine();
										break;

								}

					}
				}
				catch
				{
					//ignore this exception          
				}

				logger.Error(errorMessage.ToString(), ex);
			}
		}

		/// <summary>
		/// Executes query returning dataset, but returning only the requested page of data
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="pageNumber">page of results to return (start at 0)</param>
		/// <param name="pageSize">size of resultset to be returned</param>
		/// <param name="totalRows">total rows that matched the query criteria</param>
		/// <param name="commandParams"></param>
		/// <returns></returns>
		public DataSet ExecuteDataSet(CommandType commandType, string commandText, int pageNumber, int pageSize,
			out int totalRows, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					//	by default no records returned
					totalRows = 0;

					PrepareCommand(commandType, commandText, pageNumber, pageSize, commandParams);
					m_DataAdapter.SelectCommand = m_Command;
					var ds = new DataSet();
					m_DataAdapter.Fill(ds);

					//	get number of records
					if (!Convert.IsDBNull(m_Command.Parameters["@totalRows"].Value))
						totalRows = Int32.Parse(m_Command.Parameters["@totalRows"].Value.ToString());

					return ds;

				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}


		public DataSet ExecuteDataSet(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					m_DataAdapter.SelectCommand = m_Command;
					var ds = new DataSet();
					m_DataAdapter.Fill(ds);
					return ds;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public DataSet ExecuteDataSet(CommandType commandType, string commandText, int commandTimeout, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					var oldTimeout = m_Command.CommandTimeout;
					m_Command.CommandTimeout = commandTimeout;
					m_DataAdapter.SelectCommand = m_Command;
					var ds = new DataSet();
					m_DataAdapter.Fill(ds);
					m_Command.CommandTimeout = oldTimeout;
					return ds;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public DataSet ExecuteDataSet(CommandType commandType, string commandText, int pageNumber, int pageSize,
			string sortCol, out int totalRows, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, pageNumber, pageSize, sortCol, commandParams);
					m_DataAdapter.SelectCommand = m_Command;
					var ds = new DataSet();
					m_DataAdapter.Fill(ds);
					totalRows = Int32.Parse(m_Command.Parameters["@totalRows"].Value.ToString());
					return ds;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}



		public DataSet ExecuteTypedDataSet(DataSet ds, CommandType commandType, string commandText, string[] tables, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);

					m_DataAdapter.SelectCommand = m_Command;
					if (m_DataAdapter.TableMappings.Count > 0) m_DataAdapter.TableMappings.Clear();
					for (var i = 0; i < tables.Length; i++)
					{
						var tableName = (i == 0) ? "Table" : "Table" + i;
						m_DataAdapter.TableMappings.Add(tableName, tables[i]);
					}
					m_DataAdapter.Fill(ds);
					return ds;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		/// <summary>
		/// Helper method that returns a data table
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParams"></param>
		/// <returns></returns>
		public DataTable ExecuteDataTable(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					m_DataAdapter.SelectCommand = m_Command;
					var dt = new DataTable();
					m_DataAdapter.Fill(dt);
					return dt;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public SqlDataReader ExecuteReader(CommandType commandType, string commandText, int commandTimeout, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					var oldTimeout = m_Command.CommandTimeout;
					m_Command.CommandTimeout = commandTimeout;
					var m_Reader = m_Command.ExecuteReader();
					m_Command.CommandTimeout = oldTimeout;
					return m_Reader;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public SqlDataReader ExecuteReader(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					var m_Reader = m_Command.ExecuteReader();
					return m_Reader;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}


		public object ExecuteScalar(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					return m_Command.ExecuteScalar();
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public object ExecuteScalar(CommandType commandType, string commandText, int commandTimeout, params IDataParameter[] commandParams)
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					PrepareCommand(commandType, commandText, commandParams);
					var oldTimeout = m_Command.CommandTimeout;
					m_Command.CommandTimeout = commandTimeout;
					var retVal = m_Command.ExecuteScalar();
					m_Command.CommandTimeout = oldTimeout;
					return retVal;
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}




		public object ExecuteScalar()
		{
			var attempt = 1;
			while (true)
			{
				try
				{
					if (m_Command.CommandText == null || m_Command.CommandText == "")
						throw new ApplicationException("CommandText is not set");
					m_Command.Connection = Connection;
					return m_Command.ExecuteScalar();
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public DataTable GetTableSchema(string tableName)
		{
			var attempt = 1;
			while (true)
			{
				var commandText = "SELECT * from " + tableName + " where 1=2";
				try
				{
					PrepareCommand(CommandType.Text, commandText, null);
					var reader =
						m_Command.ExecuteReader(CommandBehavior.SchemaOnly);
					return reader.GetSchemaTable();
				}
				catch (SqlException sqlEx)
				{
					LogSqlError(sqlEx);

					if (VerifyRetryAttempt(attempt, sqlEx, commandText) == false)
						throw;

					attempt++;
					Thread.Sleep(sqlRetryTimeout);
				}
				catch (Exception ex)
				{
					LogSqlError(ex);
					throw;
				}
			}
		}

		public void ClearParameters()
		{
			m_Command.Parameters.Clear();
		}

		protected bool InBatch
		{
			get { return m_InBatch; }
		}

		private bool VerifyRetryAttempt(int attempt, SqlException sqlEx)
		{
			if (m_Command.Transaction != null || attempt > sqlRetryMaxAttempts) return false;

			if (sqlEx.Message.ToLower().IndexOf(DEADLOCK_TEXT) > -1)
			{
				if (logger.IsWarnEnabled)
					logger.WarnFormat(
						"Deadlock exception detected in DASQLBase. This is attempt {0} with MAX retry {1}. \nException = {2}",
						attempt, sqlRetryMaxAttempts, sqlEx.Message);
				return true;
			}

			return false;
		}

		private bool VerifyRetryAttempt(int attempt, SqlException sqlEx, string commandText)
		{
			if (m_Command.Transaction != null || attempt > sqlRetryMaxAttempts) return false;

			if (sqlEx.Message.ToLower().IndexOf(DEADLOCK_TEXT) > -1)
			{
				if (logger.IsWarnEnabled)
					logger.WarnFormat(
						"Deadlock exception detected for {0}. This is attempt {1} with MAX retry {2}. \nException = {3}",
						commandText, attempt, sqlRetryMaxAttempts, sqlEx.Message);
				return true;
			}

			return false;
		}

		private void PrepareBatchCommand(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			if (commandType == CommandType.StoredProcedure)
			{
				m_BatchText.AppendFormat(" IF @rc >= 0 OR @rc IS NULL EXEC @rc = {0} ", commandText);
			}
			else if (commandType == CommandType.Text)
			{
				m_BatchText.Append(commandText);
			}
			if (commandParams != null)
			{
				for (var i = 0; i < commandParams.Length; i++)
				{

					var formatStr = String.Empty;
					if (commandParams[i].Value == null || commandParams[i].Value == DBNull.Value)
					{
						formatStr = " {0}=NULL{2}";
					}
					else
					{
						commandParams[i].Value =
							commandParams[i].Value.ToString().Replace("'", "''");

						switch (((SqlParameter)commandParams[i]).SqlDbType)
						{

							case SqlDbType.Int:
							case SqlDbType.Money:
							case SqlDbType.Decimal:
							case SqlDbType.Float:
							case SqlDbType.SmallMoney:
							case SqlDbType.SmallInt:
							case SqlDbType.Real:
								formatStr = " {0}={1}{2}";
								break;
							default:
								formatStr = " {0}='{1}'{2}";
								break;
						}
					}

					m_BatchText.AppendFormat(formatStr,
						commandParams[i].ParameterName,
						commandParams[i].Value,
						(i + 1 < commandParams.Length) ? ", " : "");
				}
			}
		}

		private void PrepareCommand(CommandType commandType, string commandText, params IDataParameter[] commandParams)
		{
			ClearParameters();
			m_Command.CommandType = commandType;
			m_Command.CommandText = commandText;

#if DEBUG
			Debug.Write("\r\n");
			Debug.WriteLine(commandText);
#endif
			if (commandParams != null)
			{
				foreach (SqlParameter param in commandParams)
					m_Command.Parameters.Add(param);

#if DEBUG
				Debug.Write("\r\n");
				foreach (SqlParameter param in commandParams)
					Debug.WriteLine("'" + param.Value + "', ");
#endif
			}
			m_Command.Connection = Connection;
		}

		private void PrepareCommand(CommandType commandType, string commandText,
			int pageNumber, int pageSize, params IDataParameter[] commandParams)
		{
			PrepareCommand(commandType, commandText, pageNumber, pageSize, null, commandParams);

		}



		private void PrepareCommand(CommandType commandType, string commandText,
			int pageNumber, int pageSize, string sortCol, params IDataParameter[] commandParams)
		{
			ClearParameters();
			m_Command.CommandType = commandType;
			m_Command.CommandText = "PageQuery";

			var spSql = new StringBuilder();
			spSql.AppendFormat("{0} ", commandText);

			if (commandParams != null)
			{
				for (var i = 0; i < commandParams.Length; i++)
				{
					spSql.AppendFormat(" {0}='{1}'{2}",
						commandParams[i].ParameterName,
						commandParams[i].Value,
						(i + 1 < commandParams.Length) ? ", " : "");
				}
			}
			m_Command.Parameters.AddWithValue("@spSql", spSql.ToString());
			m_Command.Parameters.AddWithValue("@serverName", Connection.DataSource);
			m_Command.Parameters.AddWithValue("@pageNumber", pageNumber);
			m_Command.Parameters.AddWithValue("@pageSize", pageSize);
			m_Command.Parameters.AddWithValue("@sortCol", sortCol);
			m_Command.Parameters.AddWithValue("@totalRows", -1);
			m_Command.Parameters[m_Command.Parameters.Count - 1].Direction = ParameterDirection.Output;

			m_Command.Connection = Connection;

		}



		protected void OpenConnection()
		{
			if (m_Connection == null)
				m_Connection = new SqlConnection(m_ConnectionString);
			if (m_Connection.State == ConnectionState.Broken || m_Connection.State == ConnectionState.Closed)
				m_Connection.Open();


		}


		/// <summary>
		/// converts local time to UTC
		/// </summary>
		/// <param name="date"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static DateTime ToUTC(DateTime date, int offset)
		{
			return date.AddMinutes(offset);
		}


		/// <summary>
		/// Converts UTC to local time
		/// </summary>
		/// <param name="date"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static DateTime ToLocalTime(DateTime date, int offset)
		{
			return date.AddMinutes(-offset);
		}

		/// <summary>
		///		Releases used resources.
		/// </summary>
		public virtual void Dispose()
		{
			//	keep it alive till the end of current method
			GC.KeepAlive(this);

			//	release resources. calling Dispose also calls Close().
			if (m_Connection != null)
				m_Connection.Dispose();
			if (m_Command != null)
				m_Command.Dispose();
			if (m_DataAdapter != null)
				m_DataAdapter.Dispose();
			if (m_Reader != null)
				m_Reader.Close();
		}

		private static string ConnectionStringFromTargetDB(TargetDB target)
		{
			string connectionString;
			switch (target)
			{
				case TargetDB.Application:
					connectionString = sm_ConnectionString;
					break;
				case TargetDB.ApplicationCore:
					connectionString = sm_ApplicationCoreConnectionString;
					break;
				case TargetDB.ApplicationResearch:
					connectionString = sm_ApplicationResearchConnectionString;
					break;
				case TargetDB.SalesLogix:
					connectionString = sm_SalesLogixConnectionString;
					break;
				case TargetDB.ApplicationPMLocal:
					connectionString = sm_ApplicationPMLocalConnectionString;
					break;
				case TargetDB.WhatsUp:
					connectionString = sm_WhatsUpConnectionString;
					break;
				case TargetDB.MarketData:
					connectionString = sm_MarketDataConnectionString;
					break;
				case TargetDB.DTCC:
					connectionString = sm_DTCCDataConnectionString;
					break;
				case TargetDB.Eze:
					connectionString = sm_EzeConnectionString;
					break;
				default:
					throw new ApplicationException(string.Format("DASQLBase does not know how to resolve {0}", target));
			}
			return connectionString;
		}



		public void DumpParameters()
		{
			var sbNames = new StringBuilder();
			var sbValues = new StringBuilder();

			foreach (SqlParameter p in m_Command.Parameters)
			{
				sbNames.Append(p.ParameterName + ", ");
				sbValues.Append(p.Value + ", ");
				Debug.WriteLine(p.ParameterName + " = " + p.Value);
			}

			char[] charsToTrim = { '/', ' ' };
			Debug.WriteLine(sbNames.ToString().TrimEnd(charsToTrim));
			Debug.WriteLine(sbValues.ToString().TrimEnd(charsToTrim));
		}

		public DataTable Pivot(DataTable dt, string keyColumn, string pivotNameColumn, string pivotValueColumn)
		{
			/*
			 *  keyColumn - 
			 *  pivotNameColumn - column containing values to spread across as column headers
			 *	pivotValueColumn - values going under the new columns
			 * 
			 */

			var tmp = new DataTable();
			DataRow r;
			var LastKey = "//dummy//";
			int i, pValIndex, pNameIndex;
			string s;
			var firstRow = true;

			// Add non-pivot columns to the data table:
			pValIndex = dt.Columns[pivotValueColumn].Ordinal;
			pNameIndex = dt.Columns[pivotNameColumn].Ordinal;

			for (i = 0; i <= dt.Columns.Count - 1; i++)
			{
				if (i != pValIndex && i != pNameIndex)
				{
					//System.Type tp = dt.Columns[i].DataType;
					tmp.Columns.Add(dt.Columns[i].ColumnName, dt.Columns[i].DataType); // GetType());           
				}
			}

			r = tmp.NewRow();

			// now, fill up the table with the data:
			for (var x = 0; x < dt.Rows.Count; x++)
			{
				// see if we need to start a new row
				if (dt.Rows[x][keyColumn].ToString() != LastKey)
				{
					// if this isn't the very first row, we need to add the last one to the table
					if (!firstRow)
						tmp.Rows.Add(r);
					r = tmp.NewRow();
					firstRow = false;
					// Add all non-pivot column values to the new row:
					for (i = 0; i <= dt.Columns.Count - 3; i++)
						r[i] = dt.Rows[x][tmp.Columns[i].ColumnName];
					LastKey = dt.Rows[x][keyColumn].ToString();
				}
				// assign the pivot values to the proper column; add new columns if needed:
				s = dt.Rows[x][pNameIndex].ToString();
				if (!tmp.Columns.Contains(s))
					tmp.Columns.Add(s, dt.Columns[pValIndex].DataType);
				r[s] = dt.Rows[x][pValIndex];
			}

			// add that final row to the datatable:
			tmp.Rows.Add(r);

			return tmp;
		}

		public void DumpDataTable(DataTable dt, int iMaxRows)
		{
			if (iMaxRows == 0)
				iMaxRows = dt.Rows.Count;

			for (var y = 0; y < dt.Columns.Count; y++)
				Debug.Write(dt.Columns[y].ColumnName + "\t");
			Debug.Write("\r\n");

			for (var x = 0; x < iMaxRows; x++)
			{
				for (var y = 0; y < dt.Columns.Count; y++)
					Debug.Write(dt.Rows[x][y] + "\t");

				Debug.Write("\r\n");
			}
		}

		public void DumpDataRow(DataRow dr, int iMaxRows)
		{
			for (var y = 0; y < dr.Table.Columns.Count; y++)
				Debug.Write(dr.Table.Columns[y].ColumnName + "\t");

			Debug.Write("\r\n");

			for (var y = 0; y < dr.Table.Columns.Count; y++)
				Debug.Write(dr[y] + "\t");

			Debug.Write("\r\n");
		}

		public void DumpDataRows(DataRow[] dr, int iMaxRows)
		{
			if (iMaxRows == 0)
				iMaxRows = dr.Length;

			for (var y = 0; y < dr[0].Table.Columns.Count; y++)
				Debug.Write(dr[0].Table.Columns[y].ColumnName + "\t");

			Debug.Write("\r\n");

			for (var x = 0; x < iMaxRows; x++)
			{
				for (var y = 0; y < dr[0].Table.Columns.Count; y++)
					Debug.Write(dr[x][y] + "\t");

				Debug.Write("\r\n");
			}
		}
		public void DumpDataTable(DataTable dt)
		{
			DumpDataTable(dt, 0);
		}
	}
}
