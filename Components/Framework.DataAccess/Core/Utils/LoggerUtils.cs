using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Repository.Hierarchy;

namespace Framework.CommonServices.BusinessDomain.Utils
{
    /// <summary>
    /// Logger Utility Methods
    /// </summary>
    public static class LoggerUtils
    {
        /// <summary>
        /// Writes to the info log only in debug. 
        /// </summary>        
        public static void WriteInfoLogInDebug(ILog logger, string message)
        {
			#if DEBUG 
            if (logger.IsInfoEnabled)
                logger.Info(message);
			#endif
        }

        public static void WriteErrorLog(ILog logger, string message)
        {
            if (logger.IsErrorEnabled)
                logger.Error(message);
        }

        public static void WriteErrorLogInDebug(ILog logger, string message)
        {
			#if DEBUG
            if (logger.IsErrorEnabled)
                logger.Error(message);
			#endif
        }

        public static void WriteErrorLog(ILog logger, string message, Exception e)
        {
            if (logger.IsErrorEnabled)
                logger.Error(message, e);
        }

        public static void WriteErrorLogInDebug(ILog logger, string message, Exception e)
        {
			#if DEBUG
            if (logger.IsErrorEnabled)
                logger.Error(message, e);
			#endif
        }
    }
}
