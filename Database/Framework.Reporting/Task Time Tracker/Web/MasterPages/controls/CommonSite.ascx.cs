using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.MasterPages
{
	public partial class CommonSite : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            lblPerson.Text = "Logged in User: " + ApplicationCommon.GetApplicationUserName() + " (" + SessionVariables.RequestProfile.AuditId + ")";

			if (!IsPostBack)
			{
				//lblStatus.ToolTip = "ApplicationMode = " + oSliderMenu.ApplicationMode + " \n" + " MenuCategory = " + oSliderMenu.MenuCategory;
			}
		}

	}
}