using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Controls
{
    public partial class SubControlVisibilitySettings : Shared.UI.WebFramework.BaseControl
    {

        #region Methods

        private void UpdateData(string entityName)
        {
            var upCategory = entityName + "DefaultView" + "ListControl";

            PreferenceUtility.UpdateUserPreference(upCategory + "ButtonPanel", ApplicationCommon.ControlVisible, drpButtonPanelVisible.SelectedValue);
            PreferenceUtility.UpdateUserPreference(upCategory + "AdvancedButtonPanel", ApplicationCommon.ControlVisible, drpAdvancedButtonPanelVisible.SelectedValue);
            PreferenceUtility.UpdateUserPreference(entityName + "DefaultViewSearchControl", ApplicationCommon.ControlVisible, drpSearchFilterVisible.SelectedValue);

            PreferenceUtility.UpdateUserPreference(upCategory + "PagingPanel", ApplicationCommon.ControlVisible, drpPagingPanelVisible.SelectedValue);
            PreferenceUtility.UpdateUserPreference(upCategory + "SortingPanel", ApplicationCommon.ControlVisible, drpSortingPanelVisible.SelectedValue);
            PreferenceUtility.UpdateUserPreference(upCategory + "CheckedBoxColumn", ApplicationCommon.ControlVisible, drpCheckedColumnVisible.SelectedValue);
            PreferenceUtility.UpdateUserPreference(upCategory + "FontPanel", ApplicationCommon.ControlVisible, drpFontPanelVisible.SelectedValue);
            
            PreferenceUtility.UpdateUserPreference(upCategory, ApplicationCommon.GroupListBindActiveTabKey, drpOnlyBindActive.SelectedValue);

            if (orientationContainer.Visible)
            {
                PreferenceUtility.UpdateUserPreference(upCategory, ApplicationCommon.TabOrientation, drpTabOrientation.SelectedValue);
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
                var entityName                              = Convert.ToString(Page.RouteData.Values["EntityName"]);
                var upCategory                              = entityName + "DefaultView" + "ListControl";                
                
                drpButtonPanelVisible.SelectedValue         = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, upCategory + "ButtonPanel");
                drpAdvancedButtonPanelVisible.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, upCategory + "AdvancedButtonPanel");
                drpSearchFilterVisible.SelectedValue        = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, entityName + "DefaultViewSearchControl");

                drpPagingPanelVisible.SelectedValue         = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, upCategory + "PagingPanel");
                drpSortingPanelVisible.SelectedValue        = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, upCategory + "SortingPanel");
                drpCheckedColumnVisible.SelectedValue       = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, upCategory + "CheckedBoxColumn");
                drpFontPanelVisible.SelectedValue           = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ControlVisible, upCategory + "FontPanel");

                drpOnlyBindActive.SelectedValue             = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GroupListBindActiveTabKey, upCategory);

                if (SessionVariables.UserPreferences != null)
                {
                    var preference = SessionVariables.UserPreferences.Find(item => item.UserPreferenceKey.ToLower() == ApplicationCommon.TabOrientation.ToLower() && item.UserPreferenceCategory.ToLower() == upCategory.ToLower());

        			//check if current user has preference of for the key
                    if (preference != null)
                    {
                        orientationContainer.Visible = true;
                        drpTabOrientation.SelectedValue = preference.value;                    
                    }
                }
            
            }
            catch { }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            UpdateData(entityName);
        }

        protected void btnUpdateReturn_OnClick(object sender, EventArgs e)
        {
            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            UpdateData(entityName);
            Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Default" }), false);
        }        

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]); 
            Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Default" }), false);
        }

        #endregion

    }
}