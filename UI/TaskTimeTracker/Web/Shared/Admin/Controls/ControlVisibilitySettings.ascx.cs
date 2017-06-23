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

                drpBreadCrumbVisible.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, settingCategory + "BreadCrumbControl");
                drpSubMenuVisible.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, settingCategory + "SubMenuControl");
                drpSearchFilterVisible.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, settingCategory + "SearchControl");
            }
            catch { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            var settingCategory = Convert.ToString(Page.RouteData.Values["ViewName"]);

            PreferenceUtility.UpdateUserPreference(settingCategory + "BreadCrumbControl", ApplicationCommon.ControlVisible, drpBreadCrumbVisible.SelectedValue);
            PreferenceUtility.UpdateUserPreference(settingCategory + "SubMenuControl", ApplicationCommon.ControlVisible, drpSubMenuVisible.SelectedValue);
            PreferenceUtility.UpdateUserPreference(settingCategory + "SearchControl", ApplicationCommon.ControlVisible, drpSearchFilterVisible.SelectedValue);
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        #endregion

    }
}