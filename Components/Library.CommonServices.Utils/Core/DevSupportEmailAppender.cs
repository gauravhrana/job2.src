using log4net.Core;

namespace Library.CommonServices.Utils
{
    class DevSupportEmailAppender : log4net.Appender.AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level == Level.Error || loggingEvent.Level == Level.Fatal)
                Mailer.SendException(loggingEvent.RenderedMessage, loggingEvent.Domain);
        }
    }
}
