using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{
	public partial class Edit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnBack_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("AuthenticationAndAuthorizationSubRoutes", new { EntityName = "ApplicationUser", Action = "Default", SetId = true }), false);
		}
	}
}