using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Prototype.Mongodb
{
	public partial class UserLoginStatusSearch : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var appData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(appData, ddlApplication, DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.Name,
					DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.ApplicationId);

				ddlApplication.Items.Insert(0, new ListItem("All", "-1"));
			}

			if (string.IsNullOrEmpty(txtStatusName.Text) && ddlApplication.SelectedValue=="-1")
			{
				var userdata = UserLoginStatusMongoDbDataManager.GetList();

				gridUserLogin.DataSource = userdata;
				gridUserLogin.DataBind();
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var data = new DataModel.Framework.LogAndTrace.UserLoginStatusDataModel();

			if (Convert.ToInt32(ddlApplication.SelectedValue) == -1)
				data.ApplicationId = null;
			else
			data.ApplicationId = Convert.ToInt32(ddlApplication.SelectedValue);

			data.Name = txtStatusName.Text;

			var statusdata = UserLoginStatusMongoDbDataManager.GetList(data, SessionVariables.RequestProfile);

			gridUserLogin.DataSource = statusdata;
			gridUserLogin.DataBind();
		}
	}
}