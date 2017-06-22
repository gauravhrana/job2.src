using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.MenuCategory
{
	public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.MenuCategory, "MenuCategory");
		}

		override protected void btnHome_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}

		override protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Configuration/MenuCategory/Default.aspx");
		}
	}
}