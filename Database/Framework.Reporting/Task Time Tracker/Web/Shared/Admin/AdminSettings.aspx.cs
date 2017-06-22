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
                drpUpdateInfoStyle.SelectedValue = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.UpdateInfoStyle);
                drpSortArrowStyle.SelectedValue = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SortArrowStyle);
				drpDateRangeStyle.SelectedValue = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateRangeStyle);
				drpDateControlLayout.SelectedValue = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateControlLayout);
                txtTabHeaderBackgroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.TabHeaderBackgroundColor);
				txtDescCount.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridDefaultCharacterCount);
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
            PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.UpdateInfoStyle, drpUpdateInfoStyle.SelectedValue);
            PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SortArrowStyle, drpSortArrowStyle.SelectedValue);
            PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.TabHeaderBackgroundColor, txtTabHeaderBackgroundColor.Text.Trim());
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.DateRangeStyle, drpDateRangeStyle.SelectedValue);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.DateControlLayout, drpDateControlLayout.SelectedValue);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.DateControlLayout, drpDateControlLayout.SelectedValue);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.GridDefaultCharacterCount, txtDescCount.Text.Trim());
			
            Response.Redirect("~/Default.aspx");
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        #endregion

    }
}