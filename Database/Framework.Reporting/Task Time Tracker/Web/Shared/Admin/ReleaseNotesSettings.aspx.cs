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
				txtReleaseNotesRowStyleForeColor.Text				= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleForeColor).Remove(0, 1);
				txtReleaseNotesRowStyleBackColor.Text				= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleBackColor).Remove(0, 1); 
				txtReleaseNotesAlternatingRowStyleBackColor.Text	= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor).Remove(0, 1);
				txtReleaseNotesHeaderBackColor.Text					= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderBackColor).Remove(0, 1);
				txtReleaseNotesHeaderForeColor.Text					= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderForeColor).Remove(0, 1);
				drpGridLines.SelectedValue							= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesGridLines);
				txtRowStyleFontSize.Text							= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleFontSize);
				txtAlternatingRowStyleFontSize.Text					= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize);
				txtRowStyleHeight.Text								= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleHeight);
				txtAlternatingRowStyleHeight.Text					= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight);
				txtHeaderStyleHeight.Text							= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleHeight);
				txtAlternatingHeaderStyleFontSize.Text				= PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleFontSize);

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

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleForeColor, ReleaseNotesRowStyleForeColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleBackColor, ReleaseNotesRowStyleBackColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor, ReleaseNotesAlternatingRowStyleBackColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderBackColor, ReleaseNotesHeaderBackColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderForeColor, ReleaseNotesHeaderForeColor);

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesGridLines, drpGridLines.SelectedValue);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleFontSize, txtRowStyleFontSize.Text.Trim());

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize, txtAlternatingRowStyleFontSize.Text.Trim());
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesRowStyleHeight, txtRowStyleHeight.Text.Trim());

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight, txtAlternatingRowStyleHeight.Text.Trim());
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderStyleHeight, txtHeaderStyleHeight.Text.Trim());
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.ReleaseNotesHeaderStyleFontSize, txtAlternatingHeaderStyleFontSize.Text.Trim());
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