using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.UserLogin
{
	public partial class LoginDetails : PageCommon
	{        

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				lblFromDateFormat.Text = SessionVariables.UserDateFormat;
                lblToDateFormat.Text = SessionVariables.UserDateFormat;
			}
		}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SettingCategory = "UserLoginDefaultView";
            var sbm = this.Master.SubMenuObject;
            

            sbm.SettingCategory = SettingCategory + "SubMenuControl";
            sbm.Setup();
            sbm.GenerateMenu();

			//bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			//bcControl.Setup("User Login");
			//bcControl.GenerateMenu();

        }

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "LoginDetailsReportDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var strFromdate = 0;
			var strToDate = 0;
			if (!string.IsNullOrEmpty(txtSearchConditionFromDate.Text.Trim()))
			{
				string dateString = txtSearchConditionFromDate.Text;
				DateTime fromSearchDate = DateTime.ParseExact(dateString, SessionVariables.UserDateFormat, CultureInfo.InvariantCulture);
				string strfromSearchDate = fromSearchDate.ToString("yyyyMMdd");
				strFromdate = Convert.ToInt32(strfromSearchDate);
			}
			if (!string.IsNullOrEmpty(txtSearchConditionToDate.Text.Trim()))
			{
				strToDate = Convert.ToInt32(DateTime.ParseExact(txtSearchConditionToDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo).ToString("yyyyMMdd"));
			}

			LoginDetailsGrid.DataSource = Framework.Components.LogAndTrace.UserLoginDataManager.LoginDetails(strFromdate, strToDate);
			LoginDetailsGrid.DataBind();
		}
	}
}