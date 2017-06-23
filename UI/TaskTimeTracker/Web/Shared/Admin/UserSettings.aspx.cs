using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
	public partial class UserSettings : Framework.UI.Web.BaseClasses.PageBasePage
    {
		
        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);

				//if (!Page.IsPostBack)
			//	{
					var tabControl = ApplicationCommon.GetNewDetailTabControl();

					var lsControlPath = "~/Shared/Admin/Controls/LoginSettings.ascx";
					var usControlPath = "~/Shared/Admin/Controls/UserSettings.ascx";

					tabControl.Setup("UserSettingsDefaultView");

					var usControl = (Controls.UserSettings)Page.LoadControl(usControlPath);
					var lsControl = (Controls.LoginSettings)Page.LoadControl(lsControlPath);
					

					tabControl.AddTab("LoginSettings", lsControl, "Login Settings", true);
					tabControl.AddTab("UserSettings", usControl, "User Settings");

					plcUS.Controls.Add(tabControl);
				//}
            }
            catch { }
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			SettingCategory = "UserSettingsDefaultView";
			

		}

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

    }
}