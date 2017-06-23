using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin
{
	public partial class MenuSettings : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);

				
				var tabControl = ApplicationCommon.GetNewDetailTabControl();

				var mmControlPath = "~/Shared/Admin/Controls/MainMenuSettings.ascx";
				var smControlPath = "~/Shared/Admin/Controls/SubMenuSettings.ascx";
				var scControlPath = "~/Shared/Admin/Controls/SearchConfigurationSettings.ascx";

				tabControl.Setup("MenuSettingsDefaultView");

				var mmControl = (Controls.MainMenuSettings)Page.LoadControl(mmControlPath);
				var smControl = (Controls.SubMenuSettings)Page.LoadControl(smControlPath);
				var scControl = (Controls.SearchConfigurationSettings)Page.LoadControl(scControlPath);

				tabControl.AddTab("MainMenuSettings", mmControl, "Main Menu", true);
				tabControl.AddTab("SubMenuSettings", smControl, "Sub Menu");
				tabControl.AddTab("SearchConfigurationSettings", scControl, "Search Configuration");
							
				
				plcUS.Controls.Add(tabControl);

				SettingCategory = "MenuSettingsDefaultView";
				BreadCrumbObject = Master.BreadCrumbObject;	
				
			}
			catch { }
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);				

		}

		protected void Page_Load(object sender, EventArgs e)
		{
			


			
		}

		#endregion
	}
}