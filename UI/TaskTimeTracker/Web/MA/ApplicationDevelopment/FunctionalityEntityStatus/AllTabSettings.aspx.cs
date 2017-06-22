using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
    public partial class AllTabSettings : System.Web.UI.Page
    {

        #region Methods

        private void bindPreferences(string userPreferenceCategory)
        {
            drpButtonPanel.SelectedValue    = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DetailsButtonPanelVisible, userPreferenceCategory);
            drpAEFLMode.SelectedValue       = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DetailsAEFLModeEnabled, userPreferenceCategory);
            drpPaging.SelectedValue         = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DetailsPagingEnabled, userPreferenceCategory);
            drpAllTabSelected.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.AllTabSelected, userPreferenceCategory);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindPreferences(drpUserPreferenceCategory.SelectedValue);
            }
        }

        protected void drpUserPreferenceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindPreferences(drpUserPreferenceCategory.SelectedValue);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            PreferenceUtility.UpdateApplicationUserPreference(drpUserPreferenceCategory.SelectedValue, ApplicationCommon.DetailsButtonPanelVisible, drpButtonPanel.SelectedValue);
            PreferenceUtility.UpdateApplicationUserPreference(drpUserPreferenceCategory.SelectedValue, ApplicationCommon.DetailsAEFLModeEnabled, drpAEFLMode.SelectedValue);
            PreferenceUtility.UpdateApplicationUserPreference(drpUserPreferenceCategory.SelectedValue, ApplicationCommon.DetailsPagingEnabled, drpPaging.SelectedValue);
            PreferenceUtility.UpdateApplicationUserPreference(drpUserPreferenceCategory.SelectedValue, ApplicationCommon.AllTabSelected, drpAllTabSelected.SelectedValue);

            Response.Redirect(Page.GetRouteUrl("FunctionalityEntityStatusEntityRoute", new { Action = "Default", SetId = true }), false); 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("FunctionalityEntityStatusEntityRoute", new { Action = "Default", SetId = true }), false); 
        }

        #endregion

    }
}