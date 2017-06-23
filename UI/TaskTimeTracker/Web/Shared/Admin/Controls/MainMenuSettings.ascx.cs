using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Controls
{
	public partial class MainMenuSettings : Shared.UI.WebFramework.BaseControl
	{
		//public Menu MainMenu
		//{
		//	get
		//	{
		//		return NavigationMenu;
		//	}
		//}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				txtMenuBackgroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBackgroundColor).Remove(0,1);
				txtMenuForegroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuForegroundColor).Remove(0,1);
				txtMenuHoverColor.Text      = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuHoverColor).Remove(0, 1);
				txtMenuBorderColor.Text     = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBorderColor).Remove(0, 1);
				txtFontfamily.Text          = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontFamily);
				txtFontSize.Text            = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontSize);
				txtMenuColoredCategory.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuColoredCategoryColor).Remove(0, 1);
			}

			//MenuHelper.GenerateSingleParentMenu(NavigationMenu);
		}

		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{
			var BackGroundColor          = String.Empty;
			var ForeGroundColor          = String.Empty;
			var HoverColor               = String.Empty;
			var BorderColor              = String.Empty;
			var MenuColoredCategoryColor = String.Empty;

			if (!txtMenuBackgroundColor.Text.StartsWith("#"))
				BackGroundColor = "#" + txtMenuBackgroundColor.Text.Trim();
			else
				BackGroundColor = txtMenuBackgroundColor.Text.Trim();

			if (!txtMenuForegroundColor.Text.StartsWith("#"))
				ForeGroundColor = "#" + txtMenuForegroundColor.Text.Trim();
			else
				ForeGroundColor = txtMenuForegroundColor.Text.Trim();

			if (!txtMenuBorderColor.Text.StartsWith("#"))
				BorderColor = "#" + txtMenuBorderColor.Text.Trim();
			else
				BorderColor = txtMenuBorderColor.Text.Trim();

			if (!txtMenuColoredCategory.Text.StartsWith("#"))
				MenuColoredCategoryColor = "#" + txtMenuColoredCategory.Text.Trim();
			else
				MenuColoredCategoryColor = txtMenuColoredCategory.Text.Trim();

			if (!txtMenuHoverColor.Text.StartsWith("#"))
				HoverColor = "#" + txtMenuHoverColor.Text.Trim();
			else
				HoverColor = txtMenuHoverColor.Text.Trim();

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuBackgroundColor, BackGroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuBorderColor, BorderColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuForegroundColor, ForeGroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuHoverColor, HoverColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuColoredCategoryColor, MenuColoredCategoryColor);

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuFontFamily, txtFontfamily.Text.Trim());
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuFontSize, txtFontSize.Text.Trim());

			//MenuHelper.GenerateMenuStyle();

			Response.Redirect(Request.RawUrl);
		}
	}
}