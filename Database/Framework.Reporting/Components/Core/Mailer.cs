using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using log4net;

namespace Library.CommonServices.Utils
{
    public class Mailer
    {
        private static readonly string sm_DefaultSMTPServerName = ConfigurationManager.AppSettings["SMTPServer"];
        private static readonly string sm_DevSupportEmailFrom = ConfigurationManager.AppSettings["DevSupportEmailFrom"];
        private static readonly string sm_DevSupportEmailTo = ConfigurationManager.AppSettings["DevSupportEmailTo"];
        private static readonly string sm_DevSupportEmailCC = ConfigurationManager.AppSettings["DevSupportEmailCc"];
        

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string DefaultXMTPServerName
        {
            get
            {
                return sm_DefaultSMTPServerName;
            }
        }
        public static string DevSupportEmailFrom
        {
            get
            {
                return sm_DevSupportEmailFrom;
            }
        }
        public static string DevSupportEmailTo
        {
            get
            {
                return sm_DevSupportEmailTo;
            }
        }
        public static string DevSupportEmailCC
        {
            get
            {
                return sm_DevSupportEmailCC;
            }
        }
        

        private EmailAttachment[] m_Attachments;
        private string m_Cc;
        private bool m_InternalOnly;
        private string m_MailServer;
        private string m_MessageBody;
        private string m_Subject;
        private string m_To;
        private string m_From;

        public Mailer()
        {
        }

        public Mailer(string mailServer)
        {
            m_MailServer = mailServer;
        }

        public string From
        {
// ReSharper disable ValueParameterNotUsed
            set
// ReSharper restore ValueParameterNotUsed
            {
#if(DEBUG)
                m_From = sm_DevSupportEmailFrom;
#else
                m_From = value;
#endif
            }
            get { return m_From; }
        }

        public string Cc
        {
// ReSharper disable ValueParameterNotUsed
            set
// ReSharper restore ValueParameterNotUsed
            {
#if(DEBUG)
                m_Cc = sm_DevSupportEmailCC;
#else
                m_Cc = value;
#endif

            }
            get { return m_Cc; }
        }

        public string To
        {
// ReSharper disable ValueParameterNotUsed
            set
// ReSharper restore ValueParameterNotUsed
            {
#if(DEBUG)
                m_To = DevSupportEmailTo;
#else
                m_To = value.Trim();
#endif
            }
            get { return m_To; }
        }

        public string Subject
        {
            set { m_Subject = value; }
            get { return m_Subject; }
        }

        public string MessageBody
        {
            set { m_MessageBody = value; }
            get { return m_MessageBody; }
        }

        public EmailAttachment[] Attachments
        {
            get { return m_Attachments; }
            set { m_Attachments = value; }
        }

        public bool InternalOnly
        {
            get { return m_InternalOnly; }
            set { m_InternalOnly = value; }
        }

        public static void SendEmail(string from, string to, string subject, string body, EmailAttachment[] attachments)
        {
            SendEmail(from, to, subject, body, false, false, attachments);
        }

        public static void SendEmail(string from, string to, string subject, string body, bool isHtml, EmailAttachment[] attachments)
        {
            SendEmail(from, to, subject, body, isHtml, false, attachments);
        }

        public static void SendEmail(string from, string to, string subject, string body)
        {
            SendEmail(from, to, subject, body, false, false);
        }

        public static void SendEmail(string from, string to, string subject, string body, bool isHtml)
        {
            SendEmail(from, to, subject, body, isHtml, false);
        }

        public static void SendEmail(string from, string to, string subject, string body, bool isHtml, bool bInternal)
        {
            SendEmail(from, to, subject, body, isHtml, bInternal, null);
        }

        public static void SendEmail(string from, string to, string subject, Exception e)
        {
            SendEmail(from, to, subject, e, String.Empty, String.Empty);
        }

        public static void SendEmail(string from, string to, string subject, Exception e, string requestUrl, string username)
        {
            string body = GetMailBodyFromException(e, requestUrl, username);
            string errSubject = string.Format("{0} - {1}", subject, Environment.MachineName);
            SendEmail(from, to, errSubject, body);
        }

        public static void SendException(string username, Exception ex, string subject)
        {
            SendEmail(sm_DevSupportEmailFrom,
                      sm_DevSupportEmailTo,
                      subject,
                      GetMailBodyFromException(ex, Environment.MachineName, username),
                      false,
                      false,
                      null,
                      sm_DevSupportEmailCC);
        }

        public static void SendException(Exception ex, string appName)
        {
            SendEmail(sm_DevSupportEmailFrom, sm_DevSupportEmailTo, appName, ex);
        }

        public static void SendException(string message, string appName)
        {
            SendEmail(sm_DevSupportEmailFrom, sm_DevSupportEmailTo, appName, message);
        }

        public static void SendEmail(string to, string appName, string message)
        {

            SendEmail(sm_DevSupportEmailFrom, to, appName, message);
        }

        public static void SendEmail(string to, string appName, string message,bool isHtml)
        {

            SendEmail(sm_DevSupportEmailFrom, to, appName, message,isHtml);
        }

        private static void SendEmail(string from, string to,
                                      string subject, string body, bool isHtml,
                                      bool bInternal, EmailAttachment[] attachments)
        {

            SendEmail(from, to, subject, body, isHtml, bInternal, attachments, "");
        }

        private static void SendEmail(string from, string to,
                                      string subject, string body, bool isHtml,
                                      bool bInternal, EmailAttachment[] attachments, string cc)
        {
            var email = new Mailer();
            email.m_InternalOnly = bInternal;
            email.From = from;
            email.To = to;
            email.Cc = cc;
            email.Subject = subject;
            email.MessageBody = body;
            email.Attachments = attachments;
            email.Send(isHtml);
        }

        public void Send()
        {
            Send(false);
        }

        public void Send(bool isHtml)
        {
            string[] sAddr = To.Split(';');
            var oMsg = new MailMessage();

            for (var i = 0; i < sAddr.Length; i++)
                if (sAddr[i].Trim() != "")
                    oMsg.To.Add(sAddr[i]);

            oMsg.From = new MailAddress(From);
            oMsg.Subject = m_Subject;
            oMsg.Body = m_MessageBody;

            if (m_InternalOnly)
                oMsg.Subject += " [INTERNAL USE ONLY]";

            //body
            if (isHtml)
            {
                m_MessageBody += string.Format("<br><br><br>Sent from {0}", Environment.MachineName);
            }
            else
            {
                m_MessageBody += string.Format("\r\n\r\n\r\nSent from {0}", Environment.MachineName);
            }
            oMsg.IsBodyHtml = isHtml;
            oMsg.Body = m_MessageBody;
            // attachment
            if (m_Attachments != null)
            {
                foreach (EmailAttachment attach in m_Attachments)
                {
                    if (attach != null)
                        oMsg.Attachments.Add(attach.ToAttachment());
                }
            }

            // add cc if any exist
            if (!string.IsNullOrEmpty(m_Cc))
            {

                foreach (string oneCC in m_Cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress cc = new MailAddress(oneCC);
                    oMsg.CC.Add(cc);
                }
            }

            if (string.IsNullOrEmpty(m_MailServer))
            {
                m_MailServer = sm_DefaultSMTPServerName;
                if (string.IsNullOrEmpty(m_MailServer))
                {
                    if (m_MailServer == null)
                        m_MailServer = "localhost";
                }
            }
            var smtpClient = new SmtpClient(m_MailServer);
            smtpClient.Credentials = new System.Net.NetworkCredential("contact", "PassW0rd");
            try
            {
                smtpClient.Send(oMsg);
            }
            //TODO: figure email problems and remove try/catch here or throw exception back
            catch(Exception ex)
            {
               logger.Error("Sending email failed.", ex);
                //throw;
            }
        }

        private static string GetMailBodyFromException(Exception ex, string requestUrl, string username)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("User: {0}\r\n\r\n", username);

            sb.AppendFormat("Request URL: {0}\r\n\r\n", requestUrl);
            sb.AppendFormat("Exception: {0}\r\n\r\n", ex.GetType());
            sb.AppendFormat("Message: {0}\r\n\r\n", ex.Message);
            sb.AppendFormat("Server: {0}\r\n\r\n", Environment.MachineName);
            sb.AppendFormat("StackTrace: {0}\r\n\r\n", ex.StackTrace);
            return sb.ToString();
        }
    }

    public class EmailAttachment
    {
        private readonly byte[] m_Content;
        private readonly string m_DisplayName;

        public EmailAttachment(string displayName, byte[] content)
        {
            m_DisplayName = displayName;
            m_Content = content;
        }

        public Stream ContentStream
        {
            get
            {
                var ms = new MemoryStream();
                var bw = new BinaryWriter(ms);
                bw.Write(m_Content);
                bw.Flush();
                ms.Position = 0;
                return ms;
            }
        }

        public Attachment ToAttachment()
        {
            return new Attachment(ContentStream, m_DisplayName, "application/pdf");
        }
    }
}