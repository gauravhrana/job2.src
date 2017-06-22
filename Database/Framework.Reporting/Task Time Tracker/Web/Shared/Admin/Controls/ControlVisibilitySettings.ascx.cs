using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Controls
{
    public partial class ControlVisibilitySettings : System.Web.UI.UserControl
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
                var settingCategory = Convert.ToString(Page.RouteData.Values["ViewName"]);

                drpBreadCrumbVisible.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, settingCategory + "BreadCrumbControl");
                drpSubMenuVisible.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, settingCategory + "SubMenuControl");
                drpSearchFilterVisible.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, settingCategory + "SearchControl");
            }
            catch { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            var settingCategory = Convert.ToString(Page.RouteData.Values["ViewName"]);

            PerferenceUtility.UpdateUserPreference(settingCategory + "BreadCrumbControl", ApplicationCommon.ControlVisible, drpBreadCrumbVisible.SelectedValue);
            PerferenceUtility.UpdateUserPreference(settingCategory + "SubMenuControl", ApplicationCommon.ControlVisible, drpSubMenuVisible.SelectedValue);
            PerferenceUtility.UpdateUserPreference(settingCategory + "SearchControl", ApplicationCommon.ControlVisible, drpSearchFilterVisible.SelectedValue);
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        #endregion

    }
}