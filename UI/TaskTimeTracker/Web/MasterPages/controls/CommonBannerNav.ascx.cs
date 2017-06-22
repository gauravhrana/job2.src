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
	public partial class CommonBannerNav : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var applicationInfo = ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId];
			lblProjectTitle.InnerText = applicationInfo.Description;

			QuickSearchControlId.OnSearch += new EventHandler(QuickSearch_buttonClick);
            QuickSearchControlId.SetUp();
		}

		protected void QuickSearch_buttonClick(object sender, EventArgs e)
        {
            var searchTextBox = QuickSearchControlId.FindControl("txtSearchName") as TextBox;

            var strEntityName = searchTextBox.Text;

			if (strEntityName != string.Empty)
                Response.Redirect("~/QuickSearchList.aspx?SN=" + strEntityName);
        }
	}
}