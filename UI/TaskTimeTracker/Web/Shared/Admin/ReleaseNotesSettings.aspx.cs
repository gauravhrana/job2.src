using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin
{
	public partial class ReleaseNotesSettings : Framework.UI.Web.BaseClasses.PageBasePage
	{
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				txtReleaseNotesRowStyleForeColor.Text				= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleForeColor).Remove(0, 1);
				txtReleaseNotesRowStyleBackColor.Text				= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleBackColor).Remove(0, 1); 
				txtReleaseNotesAlternatingRowStyleBackColor.Text	= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor).Remove(0, 1);
				txtReleaseNotesHeaderBackColor.Text					= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderBackColor).Remove(0, 1);
				txtReleaseNotesHeaderForeColor.Text					= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderForeColor).Remove(0, 1);
				drpGridLines.SelectedValue							= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesGridLines);
				txtRowStyleFontSize.Text							= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleFontSize);
				txtAlternatingRowStyleFontSize.Text					= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize);
				txtRowStyleHeight.Text								= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleHeight);
				txtAlternatingRowStyleHeight.Text					= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight);
				txtHeaderStyleHeight.Text							= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleHeight);
				txtAlternatingHeaderStyleFontSize.Text				= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleFontSize);

			}
		}

		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{

			string ReleaseNotesRowStyleForeColor = String.Empty;
			string ReleaseNotesRowStyleBackColor = String.Empty;
			string ReleaseNotesAlternatingRowStyleBackColor = String.Empty;
			string ReleaseNotesHeaderBackColor = String.Empty;
			string ReleaseNotesHeaderForeColor = String.Empty;

			if (!txtReleaseNotesRowStyleForeColor.Text.StartsWith("#"))
				ReleaseNotesRowStyleForeColor = "#" + txtReleaseNotesRowStyleForeColor.Text.Trim();
			else
				ReleaseNotesRowStyleForeColor = txtReleaseNotesRowStyleForeColor.Text.Trim();

			if (!txtReleaseNotesRowStyleBackColor.Text.StartsWith("#"))
				ReleaseNotesRowStyleBackColor = "#" + txtReleaseNotesRowStyleBackColor.Text.Trim();
			else
				ReleaseNotesRowStyleBackColor = txtReleaseNotesRowStyleBackColor.Text.Trim();
			
			if (!txtReleaseNotesAlternatingRowStyleBackColor.Text.StartsWith("#"))
				ReleaseNotesAlternatingRowStyleBackColor = "#" + txtReleaseNotesAlternatingRowStyleBackColor.Text.Trim();
			else
				ReleaseNotesAlternatingRowStyleBackColor = txtReleaseNotesAlternatingRowStyleBackColor.Text.Trim();

			if (!txtReleaseNotesHeaderBackColor.Text.StartsWith("#"))
				ReleaseNotesHeaderBackColor = "#" + txtReleaseNotesHeaderBackColor.Text.Trim();
			else
				ReleaseNotesHeaderBackColor = txtReleaseNotesHeaderBackColor.Text.Trim();

			if (!txtReleaseNotesHeaderForeColor.Text.StartsWith("#"))
				ReleaseNotesHeaderForeColor = "#" + txtReleaseNotesHeaderForeColor.Text.Trim();
			else
				ReleaseNotesHeaderForeColor = txtReleaseNotesHeaderForeColor.Text.Trim();

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleForeColor, ReleaseNotesRowStyleForeColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleBackColor, ReleaseNotesRowStyleBackColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor, ReleaseNotesAlternatingRowStyleBackColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderBackColor, ReleaseNotesHeaderBackColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderForeColor, ReleaseNotesHeaderForeColor);

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesGridLines, drpGridLines.SelectedValue);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleFontSize, txtRowStyleFontSize.Text.Trim());

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize, txtAlternatingRowStyleFontSize.Text.Trim());
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleHeight, txtRowStyleHeight.Text.Trim());

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight, txtAlternatingRowStyleHeight.Text.Trim());
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderStyleHeight, txtHeaderStyleHeight.Text.Trim());
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderStyleFontSize, txtAlternatingHeaderStyleFontSize.Text.Trim());
		}

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);
				
				SettingCategory = "ReleaseNotesSettingsDefaultView";
				BreadCrumbObject = Master.BreadCrumbObject;

			}
			catch { }
		}
	}
}