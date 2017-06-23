using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Controls
{
	public partial class SearchConfigurationSettings : Shared.UI.WebFramework.BaseControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				txtSearchBackgroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBackgroundColor).Remove(0, 1);
				txtSearchForegroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchForegroundColor).Remove(0, 1);
				txtSearchBorderColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBorderColor).Remove(0, 1); 
				txtFontfamily.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchFontFamily);
				txtFontSize.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchFontSize);
				drpBorderStyle.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBorderStyle);
			}
		}

		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{
			var backGroundColor = String.Empty;
			var foreGroundColor = String.Empty;
			var borderColor = String.Empty;

			if (!txtSearchBackgroundColor.Text.StartsWith("#"))
				backGroundColor = "#" + txtSearchBackgroundColor.Text.Trim();
			else
				backGroundColor = txtSearchBackgroundColor.Text.Trim();

			if (!txtSearchForegroundColor.Text.StartsWith("#"))
				foreGroundColor = "#" + txtSearchForegroundColor.Text.Trim();
			else
				foreGroundColor = txtSearchForegroundColor.Text.Trim();

			if (!txtSearchBorderColor.Text.StartsWith("#"))
				borderColor = "#" + txtSearchBorderColor.Text.Trim();
			else
				borderColor = txtSearchBorderColor.Text.Trim();


			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchBackgroundColor, backGroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchForegroundColor, foreGroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchBorderColor, borderColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchFontFamily, txtFontfamily.Text);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchFontSize, txtFontSize.Text);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchBorderStyle, drpBorderStyle.SelectedValue);

			oSearchActionBar.SearchBackgroundColor = backGroundColor;
			oSearchActionBar.SearchForegroundColor = foreGroundColor;
			oSearchActionBar.SearchBorderColor = borderColor;
			oSearchActionBar.SearchFontFamily = txtFontfamily.Text;
			oSearchActionBar.SearchFontSize = txtFontSize.Text;
			oSearchActionBar.SearchBorderStyle = drpBorderStyle.SelectedValue;
			oSearchActionBar.SampleDisplay = true;

			oSearchActionBar.GenerateSearchStyle();
			oSearchActionBar.SetUp();
		}
	}
}