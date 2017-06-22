using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;

namespace ApplicationContainer.UI.Web.MasterPages
{
	public partial class CommonNav : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			MenuHelper.GenerateUserPreferenceMenu(NavigationMenu);
		}
	}
}