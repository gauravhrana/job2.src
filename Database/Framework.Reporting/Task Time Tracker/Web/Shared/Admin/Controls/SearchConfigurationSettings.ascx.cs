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
				txtSearchBackgroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBackgroundColor).Remove(0, 1);
				txtSearchForegroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchForegroundColor).Remove(0, 1);
				txtSearchBorderColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBorderColor).Remove(0, 1); 
				txtFontfamily.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchFontFamily);
				txtFontSize.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchFontSize);
				drpBorderStyle.SelectedValue = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBorderStyle);
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


			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchBackgroundColor, backGroundColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchForegroundColor, foreGroundColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchBorderColor, borderColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchFontFamily, txtFontfamily.Text);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchFontSize, txtFontSize.Text);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SearchBorderStyle, drpBorderStyle.SelectedValue);

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