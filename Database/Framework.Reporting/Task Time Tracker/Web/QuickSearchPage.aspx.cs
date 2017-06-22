using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web
{
	public partial class QuickSearchPage : PageCommon
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
				QuickSearchControlId.OnSearch += new EventHandler(QuickSearch_buttonClick);
				QuickSearchControlId.SetUp();
			
		}

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "QuickSearchPageDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected void QuickSearch_buttonClick(object sender, EventArgs e)
		{
			TextBox SearchTextBox = QuickSearchControlId.FindControl("txtSearchName") as TextBox;
			string strEntityName = SearchTextBox.Text;
			if (strEntityName != "")
			Response.Redirect("QuickSearchList.aspx?SN=" + strEntityName);
			
		}
	}
}