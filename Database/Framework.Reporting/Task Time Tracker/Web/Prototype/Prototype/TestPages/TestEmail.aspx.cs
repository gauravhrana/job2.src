using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Configuration;

namespace Shared.UI.Web.ApplicationManagement.Development.TestPages
{
	public partial class TestEmail : Framework.UI.Web.BaseClasses.PageBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            string strToEmail = String.Empty;
            strToEmail = txtEmail.Text;

            StringBuilder sBody = new StringBuilder();
            MailMessage nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
            nMail.IsBodyHtml = true;
            nMail.Subject = "New Test Email";

            sBody.AppendLine("<table cellspacing='1' cellpadding='1' style='font-family: Verdana,Geneva,Arial,Helvetica,Sans-Serif;font-size: 11px;font-style: normal;font-weight: normal;text-align: justify;'>");

            sBody.AppendLine("<tr><td>");
            
            sBody.AppendLine("</td></tr>");

            sBody.AppendLine("</table>");

            nMail.Body = sBody.ToString();
            SmtpClient sClient = new SmtpClient();
            sClient.Send(nMail);
        }
    }
}