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
	public partial class UserLoginSearch : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtUserName.Text))
			{
				var userdata = UserLoginMongoDbDataManager.GetList();

				gridUserLogin.DataSource = userdata;
				gridUserLogin.DataBind();
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
			data.UserName = txtUserName.Text;

			var userdata = UserLoginMongoDbDataManager.GetList(data, SessionVariables.RequestProfile);

			gridUserLogin.DataSource = userdata;
			gridUserLogin.DataBind();
		}
	}
}