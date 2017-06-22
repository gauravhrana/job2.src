using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityParentalHierarchy
{
	public partial class Settings : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityParentalHierarchy, "ApplicationEntityParentalHierarchy");
		}

		protected void btnHome_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx"); 
		}

		protected void btnBack_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityParentalHierarchy", Action = "Default" }), false);
		}
	}
}