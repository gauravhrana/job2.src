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
				txtMenuBackgroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBackgroundColor).Remove(0,1);
				txtMenuForegroundColor.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuForegroundColor).Remove(0,1);
				txtMenuHoverColor.Text      = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuHoverColor).Remove(0, 1);
				txtMenuBorderColor.Text     = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBorderColor).Remove(0, 1);
				txtFontfamily.Text          = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontFamily);
				txtFontSize.Text            = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontSize);
				txtMenuColoredCategory.Text = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuColoredCategoryColor).Remove(0, 1);
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

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuBackgroundColor, BackGroundColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuBorderColor, BorderColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuForegroundColor, ForeGroundColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuHoverColor, HoverColor);
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuColoredCategoryColor, MenuColoredCategoryColor);

			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuFontFamily, txtFontfamily.Text.Trim());
			PerferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.MenuFontSize, txtFontSize.Text.Trim());

			//MenuHelper.GenerateMenuStyle();

			Response.Redirect(Request.RawUrl);
		}
	}
}