using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
	public partial class ViewSettings : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);

                //
                //var tabControl = ApplicationCommon.GetNewDetailTab();

                var visibilityControlPath = "~/Shared/Admin/Controls/ControlVisibilitySettings.ascx";

                //tabControl.Setup("ViewSettingsDefaultView");


                var visbilityControl = (Controls.ControlVisibilitySettings)Page.LoadControl(visibilityControlPath);

                //tabControl.AddTab("VisibilitySettings", visbilityControl, "Control Visibility Settings");

                plcUS.Controls.Add(visbilityControl);
            }
            catch { }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            SettingCategory = "ListSettingsDefaultView";

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblViewName.Text = Convert.ToString(Page.RouteData.Values["ViewName"]);
        }

        #endregion

    }
}