using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Styles
{
	public partial class StyleMenu : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public string GetMenuBorderColor()
		{
			var menuBorderColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBorderColor);
			return menuBorderColor;
		}

		public string GetMenuHoverColor()
		{
			var menuHoverColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuHoverColor);
			return menuHoverColor;
		}

		public string GetMenuForeGroundColor()
		{
			var menuForeGroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuForegroundColor);
			return menuForeGroundColor;
		}

		public string GetMenuBackGroundColor()
		{
			var menuBackGroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBackgroundColor);
			return menuBackGroundColor;
		}

		public string GetMenuFontFamily()
		{
			var menuFontFamily = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontFamily);
			return menuFontFamily;
		}

		public string GetMenuFontSize()
		{
			var menuFontSize = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontSize);
			return menuFontSize;
		}

		public string GetMenuColoredCategoryColor()
		{
			var menuColoredCategoryColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuColoredCategoryColor);
			return menuColoredCategoryColor;
		}
		
	}
}