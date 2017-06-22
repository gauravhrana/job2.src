using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Reflection;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using System.Configuration;
using log4net;

namespace Framework.Components.DataAccess
{
    public class Audit
    {
        public int SummaryId = -1;
        public int DetailsId = -1;

        #region AuditSummaryInsert

        public void AuditSummaryInsert(string process, string status, string message)
        {
            try
            {
				DBDataReader.MessageLog(message);

				string sql; SummaryId = -1;

				//process, message, status, username

				sql = "exec AuditSummaryInsert '" + process + "', '" + message.Replace("'", "''") + "', '" + status + "','" + Environment.UserName.Replace("'", "''");

                SummaryId = int.Parse(new DBDataReader("RFS", sql, "Default", true, true).ReturnValue.ToString());
            }
            catch
            {
                SummaryId = -1;
            }
        }

        #endregion AuditSummaryInsert

        #region AuditSummaryUpdate

        public void AuditSummaryUpdate(string process, string status, DateTime ended, string message)
        {
            try
            {
	            if (SummaryId == -1)
	            {
					DBDataReader.MessageLog("AuditSummaryUpdate(): No Summary record found\r\n");
					return;
	            }

				var sql = "exec AuditSummaryUpdate " + SummaryId + ", '" + ended.ToString("yyyy-MM-dd hh:mm:ss tt") + "', '" + message.Replace("'", "''") + "', '" + status + "'";

                DBDML.RunSQL(process, sql, "Default");
            }
            catch { }
        }

        #endregion AuditSummaryUpdate

        #region AuditDetailInsert

        public void AuditDetailInsert(string process, string status, string message)
        {
			//try
			//{
			//    // Common.Components.DataAccess.DataLog.Create(

			//    if (SummaryId == -1)
			//    {
			//        System.IO.File.AppendAllText(StartUp.LogFile, "AuditDetailInsert(): No Summary record found\r\n");

			//        //var x = DateTime.Now;
			//        // Common.Components.DataAccess.DataLog.Create("AuditDetailInsert(): No Summary record found\r\n", 1, "706");
			//        return;
			//    }

			//    var sql = "exec AuditDetailInsert " + SummaryId + ", '" + process + "', '" + status + "', '" + message.Replace("'", "''") + "'";

			//    var value = String.Empty;

			//    using(var reader = new DataAccess.DBDataReader(process, sql, "Default", true, true))
			//    {
			//        value = reader.ReturnValue.ToString();
			//    }
			//}
			//catch
			//{

			//}
        }

        #endregion AuditDetailInsert

        #region EndProcess

		//public static void EndProcess(string process, string LogFile, string message)
		//{
		//	StartUp.FeedsAudit.AuditDetailInsert(process, "OK", "End Process");
		//	StartUp.FeedsAudit.AuditSummaryUpdate(process, "Ended", DateTime.Now, "Success");
		//	try
		//	{
		//		System.IO.File.AppendAllText(StartUp.LogFile, message + "\t" + DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\r\n");
		//	}
		//	catch
		//	{ }
		//}

        #endregion EndProcess

        #region HandleError

        public static string HandleError(string process, string logEntry)
        {
            StartUp.FeedsAudit.AuditDetailInsert(process, "Error", logEntry);
            StartUp.FeedsAudit.AuditSummaryUpdate(process, "Error", DateTime.Now, logEntry);

            //if (LossAnalysis.Properties.Settings.Default.SendMail) LossAnalysis.SendMail.SendEmail("RFS " + process + " Error", LogEntry, null);
			//try
			//{
				DBDataReader.MessageLog(logEntry + "\t" + DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString());
			//}
			//catch
			//{

			//}

            return "Error";
        }

        #endregion HandleError

        #region Chatter

        public void Chatter(string process, string status, string logEntry)
        {
            StartUp.FeedsAudit.AuditDetailInsert(process, status, logEntry);

			//try
			//{            
				DBDataReader.MessageLog(logEntry + "\t" + DateTime.Now.ToLongTimeString());
			//}
			//catch { }
        }

        #endregion Chatter

        #region HandleExit

		//public static void HandleExit(string process, string LogEntry)
		//{
		//	StartUp.FeedsAudit.AuditDetailInsert(process, "Exit", LogEntry);
		//	StartUp.FeedsAudit.AuditSummaryUpdate(process, "Done", DateTime.Now, LogEntry);

		//	try
		//	{
		//		System.IO.File.AppendAllText(StartUp.LogFile, LogEntry + "\t" + DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\r\n");
		//	}
		//	catch { }
		//}

        #endregion HandleExit

		#region Log4Net Logging Methods

		//private static string GetParamValue(string param, string message)
		//{
		//    string value = String.Empty;

		//    var sql = message.ToLower();

		//    if (sql.Contains(param))
		//    {
		//        var lst = new List<string>();
		//        lst = sql.Split(new string[] { "=", " ", "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
		//        if (lst.Contains(param))
		//        {
		//            int index = lst.IndexOf(param);
		//            if (index != -1)
		//            {
		//                if (lst.Count > (index + 1))
		//                {
		//                    try { value = lst[index + 1]; }
		//                    catch { }
		//                }
		//            }
		//        }
		//    }

		//    return value;
		//}

		//private static void LoadConfig()
		//{
		//    var assembly = Assembly.GetExecutingAssembly();
		//    var stream = assembly.GetManifestResourceStream("Framework.Components.DataAccess.Log4Net.config");

		//    if (stream != null)
		//    {
		//        log4net.Config.XmlConfigurator.Configure(stream);
		//    }

		//    var hierarchy = LogManager.GetRepository() as Hierarchy;

		//    if (hierarchy != null && hierarchy.Configured)
		//    {
		//        foreach (var appender in hierarchy.GetAppenders())
		//        {
		//            if (appender is AdoNetAppender)
		//            {
		//                var adoNetAppender = (AdoNetAppender)appender;
		//                adoNetAppender.ConnectionString = ConfigurationManager.ConnectionStrings["LoggingAndTrace"].ConnectionString;
		//                adoNetAppender.ActivateOptions(); //Refresh AdoNetAppenders Settings
		//            }
		//        }
		//    }
		//}

		//public static void LogInfo(string message)
		//{
		//    try
		//    {
		//        if (!log4net.LogManager.GetLogger("MS-SQL").Logger.Repository.Configured)
		//        {
		//            LoadConfig();
		//        }

		//        log4net.MDC.Set("user", GetParamValue("@auditid", message));
		//        log4net.MDC.Set("ApplicationId", Convert.ToString(SetupConfiguration.ApplicationId));
		//        log4net.MDC.Set("Logger", "test");

		//        //var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		//        var log = LogManager.GetLogger("MS-SQL");
               
		//        log.Info(message);
		//    }
		//    catch { }
		//}

		//public static void LogError(string message, Exception ex)
		//{
		//    try
		//    {

		//        if (!log4net.LogManager.GetLogger("MS-SQL").Logger.Repository.Configured)
		//        {
		//            LoadConfig();
		//        }

		//        log4net.MDC.Set("user", GetParamValue("@auditid", message));
		//        log4net.MDC.Set("ApplicationId", Convert.ToString(SetupConfiguration.ApplicationId));                
		//        log4net.MDC.Set("Logger", "test");

		//        //var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		//        var log = LogManager.GetLogger("MS-SQL");                
		//        log.Error(message, ex);

		//    }
		//    catch { }
		//}

		#endregion

    }
}