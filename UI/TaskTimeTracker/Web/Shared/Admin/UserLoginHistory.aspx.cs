using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Net.Mail;
 

namespace Shared.UI.Web.Configuration
{
	public partial class UserLoginHistory : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				
			}
		}

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control();
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			SettingCategory = "UserLoginHistoryDefaultView";
			var sbm = this.Master.SubMenuObject;
			

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			//bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			//bcControl.Setup("User Login History");
			//bcControl.GenerateMenu();

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var data = new Framework.Components.LogAndTrace.UserLoginHistoryDataModel();
			if (oDateRange.FromDate != null)
			{
				data.FromSearchDate = oDateRange.FromDate;
			}
			if (oDateRange.ToDate != null)
			{
				data.ToSearchDate = oDateRange.ToDate;
			}

			data.UserName = txtSearchConditionName.Text;

			LoginHistoryGrid.DataSource = Framework.Components.LogAndTrace.UserLoginHistoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			LoginHistoryGrid.DataBind();
			PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.DateRangeFormat, oDateRange.DateRangeFormatId.ToString());
			PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.FromDateRange, oDateRange.FromDate.ToString());
			PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.ToDateRange, oDateRange.ToDate.ToString());
		}

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {

                var toEmail = txtEmail.Text.Trim();
                if (!string.IsNullOrEmpty(toEmail))
                {
                    var sb = new StringBuilder();
                    var sw = new StringWriter(sb);
                    var hw = new HtmlTextWriter(sw);
                    if (LoginHistoryGrid.DataSource == null)
                    {
                        var data = new Framework.Components.LogAndTrace.UserLoginHistoryDataModel();
                        if (oDateRange.FromDate != null)
                        {
                            data.FromSearchDate = oDateRange.FromDate;
                        }
                        if (oDateRange.ToDate != null)
                        {
                            data.ToSearchDate = oDateRange.ToDate;
                        }

                        data.UserName = txtSearchConditionName.Text;

                        LoginHistoryGrid.DataSource = Framework.Components.LogAndTrace.UserLoginHistoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
                        LoginHistoryGrid.DataBind();
                    }
                    LoginHistoryGrid.RenderControl(hw);

                    string strToEmail = String.Empty;
                    strToEmail = txtEmail.Text;

                    var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
                    nMail.IsBodyHtml = true;
                    nMail.Subject = "User Login History Results";                    

                    nMail.Body = sb.ToString();
                    var sClient = new SmtpClient();
                    sClient.Send(nMail);
                }
            }
            catch { }
        }

        #endregion

    }
}