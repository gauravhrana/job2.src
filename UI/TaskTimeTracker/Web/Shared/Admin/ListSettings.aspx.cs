using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
	public partial class ListSettings : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            var fsControlPath = "~/Shared/Admin/Controls/FieldConfigurationSettings.ascx";
            var subControlPath = "~/Shared/Admin/Controls/SubControlVisibilitySettings.ascx";

            tabControl.Setup("UserSettingsDefaultView");

            var lsControl = (Controls.FieldConfigurationSettings)Page.LoadControl(fsControlPath);
            var usControl = (Controls.SubControlVisibilitySettings)Page.LoadControl(subControlPath);

            tabControl.AddTab("FCSettings", lsControl, "FC Settings", true);
            tabControl.AddTab("VisibilitySettings", usControl, "Sub Control Visibility Settings");

            plcSettings.Controls.Add(tabControl);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            SettingCategory = "ListSettingsDefaultView";
            //bccontrol.SettingCategory = SettingCategory + "BreadCrumbControl";
            //bccontrol.Setup("");
            //bccontrol.GenerateMenu();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            lblEntityName.Text = entityName;
        }

        #endregion

    }
}