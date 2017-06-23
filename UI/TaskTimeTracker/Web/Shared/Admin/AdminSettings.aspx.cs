using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
	public partial class AdminSettings : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpUpdateInfoStyle.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.UpdateInfoStyle);
                drpSortArrowStyle.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SortArrowStyle);
				drpDateRangeStyle.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateRangeStyle);
				drpDateControlLayout.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateControlLayout);
                txtTabHeaderBackgroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.TabHeaderBackgroundColor);
				txtDescCount.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridDefaultCharacterCount);
                drpEnableGroupBy.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.EnableGroupBy);
				drpEnableDebug.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DebugFlag);
            }
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			SettingCategory = "AdminSettingsDefaultView";
			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();
			
		}

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.UpdateInfoStyle, drpUpdateInfoStyle.SelectedValue);
            PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SortArrowStyle, drpSortArrowStyle.SelectedValue);
            PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.TabHeaderBackgroundColor, txtTabHeaderBackgroundColor.Text.Trim());
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.DateRangeStyle, drpDateRangeStyle.SelectedValue);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.DateControlLayout, drpDateControlLayout.SelectedValue);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.DateControlLayout, drpDateControlLayout.SelectedValue);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.GridDefaultCharacterCount, txtDescCount.Text.Trim());
            PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.EnableGroupBy, drpEnableGroupBy.SelectedValue);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.DebugFlag, drpEnableDebug.SelectedValue);

            Response.Redirect("~/Default.aspx");
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        #endregion

    }
}