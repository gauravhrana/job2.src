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
				txtSubMenuTopBarBackgroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBackgroundColor).Remove(0, 1);
				if(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor).StartsWith("#"))
					txtSubMenuTopBarBorderColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor).Remove(0, 1);
				else
					txtSubMenuTopBarBorderColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor);
				txtSubMenuBackgroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBackgroundColor).Remove(0, 1);
				txtSubMenuForegroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuForegroundColor).Remove(0, 1);
				txtSubMenuHoverColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuHoverColor).Remove(0, 1);
				txtSubMenuBorderColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderColor).Remove(0, 1);
				drpBorderStyle.SelectedValue = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderStyle);
				txtFontfamily.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontFamily);
				txtFontSize.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontSize);				
								
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

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuBorderStyle, drpBorderStyle.SelectedValue);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuBorderColor, BorderColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuTopBackgroundColor, TopBackgroundColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuTopBorderColor, TopBorderColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuBackgroundColor, BackGroundColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuForegroundColor, ForeGroundColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuHoverColor, HoverColor);

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuFontFamily, txtFontfamily.Text.Trim());
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.SubMenuFontSize, txtFontSize.Text.Trim());


			oSubMenu.SettingCategory = "MilestoneDefaultViewSubMenuControl";
			oSubMenu.Setup();
			oSubMenu.GenerateMenu(true);
		}
	}
}