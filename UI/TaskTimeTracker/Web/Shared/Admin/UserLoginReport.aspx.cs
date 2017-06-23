using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Framework.UI.Web.BaseClasses;
using System.Data;

namespace Shared.UI.Web.Admin
{
	public partial class UserLoginReport : Framework.UI.Web.BaseClasses.PageBasePage
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var userloginData = Framework.Components.LogAndTrace.UserLoginDataManager.GetList(SessionVariables.RequestProfile);               
				var distinctdata = userloginData.Select( x => x.UserName).Distinct().ToList();
				UIHelper.LoadDropDown(distinctdata, drpUserName,
				             Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName,
				             Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName);
			}
		}

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "UserLoginReportDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var mindate = 0;
			var maxdate = 0;
			if (!string.IsNullOrEmpty(txtSearchConditionMinDate.Text.Trim()))
				mindate = Convert.ToInt32(DateTime.ParseExact(txtSearchConditionMinDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo).ToString("yyyyMMdd"));

			if (!string.IsNullOrEmpty(txtSearchConditionMaxDate.Text.Trim()))
				maxdate = Convert.ToInt32(DateTime.ParseExact(txtSearchConditionMaxDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo).ToString("yyyyMMdd"));


			LoginReport.DataSource = Framework.Components.LogAndTrace.UserLoginDataManager.LoginReport(drpUserName.SelectedValue, 0, 0,
																							SessionVariables.RequestProfile);
			LoginReport.DataBind();
		}

	}
}