using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;


namespace ApplicationContainer.UI.Web.Prototype.Mongodb
{
	public partial class UserLoginData : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}
		
		protected void lnkUserLogin_Click(object sender, EventArgs e)
		{
			gridUserLogin.DataSource = UserLoginMongoDbDataManager.GetList();
			gridUserLogin.DataBind();
		}

		protected void lnkUserLoginHistory_Click(object sender, EventArgs e)
		{
			gridUserLogin.DataSource = UserLoginHistoryMongoDbDataManager.GetList();
			gridUserLogin.DataBind();
		}

		protected void lnkUserLoginStatus_Click(object sender, EventArgs e)
		{
			gridUserLogin.DataSource = UserLoginStatusMongoDbDataManager.GetList();
			gridUserLogin.DataBind();
		}
	}
}