using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web
{
	public partial class TestFESsummaryChart : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SettingCategory = "FESSummaryChartDefaultView";
				oSearchFilter.SettingCategory = SettingCategory + "SearchControl";


			}

		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var sbm = this.Master.SubMenuObject;
			var bcControl = this.Master.BreadCrumbObject;


			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup("FES Summary Chart");
			bcControl.GenerateMenu();			

		}


	}
}