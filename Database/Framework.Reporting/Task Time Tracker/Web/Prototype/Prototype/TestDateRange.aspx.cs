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

namespace Shared.UI.Web.ApplicationManagement.Development
{
	public partial class TestDateRange : Framework.UI.Web.BaseClasses.PageBasePage
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
			if (oDate.FromDate != null)
			{
				data.FromSearchDate = oDate.FromDate;
			}
			if (oDate.ToDate != null)
			{
				data.ToSearchDate = oDate.ToDate;
			}



			LoginHistoryGrid.DataSource = Framework.Components.LogAndTrace.UserLoginHistoryDataManager.Search(data, SessionVariables.RequestProfile);
			LoginHistoryGrid.DataBind();
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.DateRangeFormat, oDate.DateRangeFormatId.ToString());
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.FromDateRange, oDate.FromDate.ToString());
			//ApplicationCommon.UpdateUserPreference("General", ApplicationCommon.ToDate, oDate.ToDate.ToString());
		}

		

		#endregion
	}
}