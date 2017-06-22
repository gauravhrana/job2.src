using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Framework.UI.Web.BaseClasses
{
	public class PageSettings : PageCommon
	{		
		protected virtual void btnHome_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}

		protected virtual void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default" }), false);
		}
	}
}