using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Controls
{
	public partial class SubMenuSettings : Shared.UI.WebFramework.BaseControl
	{
		protected override void OnInit(EventArgs e)
		{
			oSubMenu.SettingCategory = "MilestoneDefaultViewSubMenuControl";
			oSubMenu.Setup();
			oSubMenu.GenerateMenu(true);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				txtSubMenuTopBarBackgroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBackgroundColor).Remove(0, 1);
				if(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor).StartsWith("#"))
					txtSubMenuTopBarBorderColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor).Remove(0, 1);
				else
					txtSubMenuTopBarBorderColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor);
				txtSubMenuBackgroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBackgroundColor).Remove(0, 1);
				txtSubMenuForegroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuForegroundColor).Remove(0, 1);
				txtSubMenuHoverColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuHoverColor).Remove(0, 1);
				txtSubMenuBorderColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderColor).Remove(0, 1);
				drpBorderStyle.SelectedValue = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderStyle);
				txtFontfamily.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontFamily);
				txtFontSize.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontSize);				
								
			}
			
		}

		
		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{
			string TopBackgroundColor = String.Empty;
			string TopBorderColor = String.Empty;
			string BackGroundColor = String.Empty;
			string ForeGroundColor = String.Empty;
			string HoverColor = String.Empty;
			string BorderColor = String.Empty;

			if (!txtSubMenuTopBarBackgroundColor.Text.StartsWith("#"))
				TopBackgroundColor = "#" + txtSubMenuTopBarBackgroundColor.Text.Trim();
			else
				TopBackgroundColor =  txtSubMenuTopBarBackgroundColor.Text.Trim();
			
			if(!txtSubMenuTopBarBorderColor.Text.StartsWith("#"))
				TopBorderColor = "#" + txtSubMenuTopBarBorderColor.Text.Trim();
			else
				TopBorderColor = txtSubMenuTopBarBorderColor.Text.Trim();

			if (!txtSubMenuBackgroundColor.Text.StartsWith("#"))
				BackGroundColor = "#" + txtSubMenuBackgroundColor.Text.Trim();
			else
				BackGroundColor = txtSubMenuBackgroundColor.Text.Trim();

			if (!txtSubMenuForegroundColor.Text.StartsWith("#"))
				ForeGroundColor = "#" + txtSubMenuForegroundColor.Text.Trim();
			else
				ForeGroundColor = txtSubMenuForegroundColor.Text.Trim();

			if (!txtSubMenuHoverColor.Text.StartsWith("#"))
				HoverColor = "#" + txtSubMenuHoverColor.Text.Trim();
			else
				HoverColor = txtSubMenuHoverColor.Text.Trim();

			if (!txtSubMenuBorderColor.Text.StartsWith("#"))
				BorderColor = "#" + txtSubMenuBorderColor.Text.Trim();
			else
				BorderColor = txtSubMenuBorderColor.Text.Trim();

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuBorderStyle, drpBorderStyle.SelectedValue);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuBorderColor, BorderColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuTopBackgroundColor, TopBackgroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuTopBorderColor, TopBorderColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuBackgroundColor, BackGroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuForegroundColor, ForeGroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuHoverColor, HoverColor);

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuFontFamily, txtFontfamily.Text.Trim());
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuFontSize, txtFontSize.Text.Trim());


			oSubMenu.SettingCategory = "MilestoneDefaultViewSubMenuControl";
			oSubMenu.Setup();
			oSubMenu.GenerateMenu(true);
		}
	}
}