using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using Framework.Components.DataAccess.ThirdParty;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using System.Configuration;
using System.Reflection;
using Log4Mongo;

namespace Framework.Components.DataAccess
{
	//[DebuggerStepThrough]
    public static class Log4Net
    {
        #region Static Constructor

        static Log4Net()
		{
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("Framework.Components.DataAccess.Log4Net.config");
            
			if (stream != null)
            {
                XmlConfigurator.Configure(stream);
            }

            log4net.Util.LogLog.InternalDebugging = true;
            
			var hierarchy = LogManager.GetRepository() as Hierarchy;

            if (hierarchy != null && hierarchy.Configured)
            {
                foreach (var appender in hierarchy.GetAppenders())
                {
                    if (appender is AdoNetAppender)
                    {
                        var adoNetAppender = (AdoNetAppender)appender;
                        adoNetAppender.ConnectionString = ConfigurationManager.ConnectionStrings["LoggingAndTrace"].ConnectionString;
                        adoNetAppender.ActivateOptions(); //Refresh AdoNetAppenders Settings
                    }
                    //else if (appender is MongoDBAppender)
                    //{
                    //    var mongoDBAppender = (MongoDBAppender)appender;
                    //    mongoDBAppender.ConnectionString = ConfigurationManager.ConnectionStrings["LoggingAndTraceMongoDB"].ConnectionString;
                    //    mongoDBAppender.ActivateOptions(); //Refresh AdoNetAppenders Settings
                    //}
                }
            }
		}

        #endregion

        #region Log4Net Logging Methods

        private static string GetParamValue(string param, string message)
        {
            var value = String.Empty;
            var sql = message.ToLower();
            sql = sql.Replace("\r", string.Empty);
            sql = sql.Replace("\n", string.Empty);
            sql = sql.Replace("\t", string.Empty);

            if (sql.Contains(param))
            {
                var lst = sql.Split(new [] { "=", " ", "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (lst.Contains(param))
                {
                    var index = lst.IndexOf(param);

                    if (index != -1)
                    {
                        if (lst.Count > (index + 1))
                        {
                            try { value = lst[index + 1]; }
                            catch { }
                        }
                    }
                }
            }

			if (param == "@applicationid" && string.IsNullOrEmpty(value))
			{
				return SetupConfiguration.ApplicationId.ToString();
			}
            else if (value == "null")
            {
                value = SetupConfiguration.ApplicationId.ToString();
            }

            return value;
        }

	    private static string LoggerName(int framesBack = 3)
	    {
		    var loggerName = "General";

		    try
		    {
			    var stackTrace = new StackTrace(); // get call stack
			    var stackFrames = stackTrace.GetFrames(); // get method calls (frames)

			    framesBack = framesBack > stackFrames.Length - 1? stackFrames.Length - 1 : framesBack;

				var callingFrame = stackFrames[framesBack];
			    var method = callingFrame.GetMethod();

				loggerName = method.DeclaringType + "-" + method.Name;

				if (loggerName.Contains("DBDataTable-.ctor") || loggerName.Contains("DBDataTable - GetData"))
			    {
					callingFrame = stackFrames[4];
					method = callingFrame.GetMethod();
					loggerName = method.DeclaringType + "-" + method.Name;
			    }

		    }
		    catch (Exception)
		    {
			    // TODO ... remove try/catch or figure out why
		    }

		    return loggerName;
        }

		public struct SqlStatementStackMessage
		{
			public DateTime TimeStamp;
			public string Message;

			public SqlStatementStackMessage(string message)
			{
				TimeStamp = DateTime.Now;
				Message = message;
			}
		}

		private static int _sqlStatementStackMaxLength = 100;
		public static int SqlStatementStackMaxLength
		{
			get { return _sqlStatementStackMaxLength; }
			set
			{
				_sqlStatementStackMaxLength = value;

				Log4Net.SqlStatementStack = Log4Net.SqlStatementStack.Clone(value);
			}
		}

		public static FixedSizeStack SqlStatementStack = new FixedSizeStack(SqlStatementStackMaxLength);

		public class TraceContextDetail
		{
			public string AuditId;
			public string Message;
			public string ConnectionKey;

			public string LoggerName;
			public string ApplicationId;

			public string StackTrace;
			public string Computer;

			public void SetSetttings(string message, string loggerName, string applicationId, string connectionKey, string auditId, string trace)
			{
				LoggerName = loggerName;
 				ApplicationId = applicationId;
		        StackTrace = trace;
		        Computer = SetupConfiguration.UserMachineName;
		        ConnectionKey =  connectionKey;

		        if (string.IsNullOrEmpty(auditId))
		        {
					AuditId = GetParamValue("@auditid", message);
		        }
			}
		}

		public class TraceContext
		{
			public Stopwatch Stopwatch;
			public TraceContextDetail Detail;

			public TraceContext()
			{
				Stopwatch = new Stopwatch();
			}

			public TraceContext(TraceContextDetail detail) : this()
			{
				Detail = detail;
				Stopwatch.Start();
			}
		}

		public static TraceContext LogSqlInfoStart(string sql, string dataStoreKey, dynamic parameters, int stackBack = 4)
		{
			var message = dataStoreKey + "." + sql + " \r\n" + DataAccessBase.ToSQLInfo(parameters);

			SqlStatementStack.Push(new SqlStatementStackMessage(message));

			var tcDetail = LogInfo(message, dataStoreKey, stackBack);

			return new TraceContext(tcDetail);
		}

		public static void LogSQLInfoStop(TraceContext traceContext, int count = 0, int stackBack = 3)
		{
			if (traceContext != null)
			{
				traceContext.Stopwatch.Stop();

				var message = string.Format("Elapsed Milliseconds {0}, RecordCount {1}", traceContext.Stopwatch.ElapsedMilliseconds, count);

				//LogInfo(message, traceContext.Detail.LoggerName, traceContext.Detail.ApplicationId, traceContext.Detail.ConnectionKey, traceContext.Detail.AuditId);

				SetMDC(traceContext.Detail);

		        var log = LogManager.GetLogger(traceContext.Detail.LoggerName);
		        log.Info(message);
			}
			else
			{
				var message = string.Format("RecordCount {0}", count);

				LogInfo(message, null, stackBack);
			}

			//LogInfo(message, string.Empty, 4);
			//LogInfo(message, string.Empty, 6);
			//LogInfo(message, string.Empty, 5);
			//LogInfo(message, string.Empty, 7);
			//LogInfo(message, string.Empty, 8);
		}

		public static TraceContextDetail LogInfo(string message, string connectionKey = "", int framesBack = 3)
        {
			var loggerName = LoggerName(framesBack);

			if (loggerName == null) System.Diagnostics.Debugger.Break();

			var applicationId = Convert.ToInt32(GetParamValue("@applicationid", message)).ToString();
			return LogInfo(message, loggerName, applicationId, connectionKey);
        }

        public static TraceContextDetail LogInfo(string message, string logger, string applicationId, string connectionKey = "", string auditId = "")
        {
			var result = new TraceContextDetail();
			//try
			//{
		        result.SetSetttings(message, logger, applicationId, connectionKey, auditId, Environment.StackTrace);

		        SetMDC(result);

		        var log = LogManager.GetLogger(logger);
		        log.Info(message);
			//}
			//catch (Exception e)
			//{
			//	Debug.WriteLine(e.Message);
			//}

	        return result;
        }

	    private static void SetMDC(TraceContextDetail result)
	    {
		    MDC.Set("user", result.AuditId);
		    MDC.Set("ApplicationId", result.ApplicationId);
		    MDC.Set("StackTrace", result.StackTrace);
		    MDC.Set("Computer", result.Computer);
		    MDC.Set("ConnectionKey", result.ConnectionKey);
	    }

	    public static void LogDebug(string message, string connectionKey = "", int framesBack = 3)
        {
			var loggerName = LoggerName(framesBack);

			var applicationId = Convert.ToInt32(GetParamValue("@applicationid", message));
			
			LogDebug(message, loggerName, applicationId, connectionKey);
        }

		public static void LogDebug(string message, string logger, int applicationId , string connectionKey = "", string auditId="")
        {
			var result = new TraceContextDetail();
            try
            {
				result.SetSetttings(message, logger, applicationId.ToString(), connectionKey, auditId, Environment.StackTrace);

				SetMDC(result);

                var log = LogManager.GetLogger(logger);
                log.Debug(message);
            }
            catch { }
        }

        public static void LogError(string message, Exception ex, string connectionKey = "")
        {
            var loggerName = LoggerName();

			var applicationId = Convert.ToInt32(GetParamValue("@applicationid", message));
			LogError(message, loggerName, applicationId, ex, connectionKey);
        }

        public static void LogError(string message, string logger, int applicationId, Exception ex, string connectionKey = "")
        {
            try
            {
                MDC.Set("user", GetParamValue("@auditid", message));
                MDC.Set("ApplicationId", Convert.ToString(applicationId));
				MDC.Set("StackTrace", Environment.StackTrace);
                MDC.Set("Computer", SetupConfiguration.UserMachineName);
                MDC.Set("ConnectionKey", connectionKey);

                var log = LogManager.GetLogger(logger);
                log.Error(message, ex);
            }
            catch { }
        }

        #endregion
    }

}
