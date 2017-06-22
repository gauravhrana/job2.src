using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;

namespace ApplicationContainer.Core.UI.Web.CapitalMarkets
{
	public partial class Nav : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			MenuHelper.GenerateUserPreferenceMenu(NavigationMenu);
		}

	}
}