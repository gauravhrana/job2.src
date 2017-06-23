using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;
using Menu = System.Web.UI.WebControls.Menu;

namespace Shared.UI.Web
{
    public class MenuHelper
    {
        #region Constructor

        static MenuHelper()
        {
        }

        #endregion

        #region Properties

        public static string staticSeperator = "###";

        private static string MenuBackgroundColor { get; set; }

        private static string MenuForegroundColor { get; set; }

        private static string MenuHoverColor { get; set; }

        private static string MenuBorderColor { get; set; }

        private static string MenuFontFamily { get; set; }

        private static string MenuFontSize { get; set; }

        private static string MenuColoredCategoryColor { get; set; }

        private static string MenuStyle { get; set; }

        private static string MenuItemStyle { get; set; }

        #endregion

        #region Static Methods
        
        public static string GetMenuItemUrl(DataRow dr)
        {
            string navigateUrl = string.Empty;

            navigateUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]);

            if (!navigateUrl.Contains(".aspx") && !navigateUrl.Contains("/Home"))
            {
                navigateUrl = navigateUrl.Replace("~/", "~/" + SessionVariables.CurrentApplicationCode + "/" + SessionVariables.CurrentApplicationModuleCode + "/"); 
            }

            return navigateUrl;
        }

        public static string GetMenuItemUrl(MenuDataModel objMenu)
        {
            string navigateUrl = string.Empty;

            navigateUrl = objMenu.NavigateURL;

            if (!navigateUrl.Contains(".aspx"))
            {
                navigateUrl = navigateUrl.Replace("~/", "~/" + SessionVariables.CurrentApplicationCode + "/" + SessionVariables.CurrentApplicationModuleCode + "/");
            }

            return navigateUrl;
        }

        public static string GetMenuItemStyle(object menuValue)
        {
            var itemClass = "stdMenuItem";

            if (SessionVariables.UserMenuCategoryType == ApplicationCommon.ColoredMenuCategory)
            {
                itemClass = "highlightMenuCategoryMenuItem";
            }

            try
            {
                Convert.ToInt32(menuValue.ToString());
            }
            catch
            {
                itemClass = "highlightMenuItem";
            }

            return itemClass;
        }

        public static List<MenuDataModel> GetUserPreferedMenu()
        {
            var listResult = new List<MenuDataModel>();
            
            var data = new MenuCategoryXMenuDataModel();
            try
            {
                data.MenuCategoryId = Convert.ToInt32(SessionVariables.UserMenuCategory);
            }
            catch
            {
                data.MenuCategoryId = 0;
            }

            var listMenuCategoryXMenu = MenuCategoryXMenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            var listSiteMenu = SessionVariables.SiteMenuData;

            // filter site menu for current application module           
            //listResult = listSiteMenu
            //    .Where(x => x.ApplicationModule == SessionVariables.CurrentApplicationModuleCode)
            //    .ToList();

            var applicableMenuIds = listMenuCategoryXMenu.Select(x => x.MenuId).ToList();
            try
            {
                // appply filter to retreived records from db
                listResult = listSiteMenu
                    .Where(x => applicableMenuIds.Contains(x.MenuId.Value)).ToList();
            }
            catch
            {
                listResult.Clear();
            }

            var searchResult = listSiteMenu.Where(x => x.PrimaryDeveloper == SessionVariables.UserMenuCategory).ToList();
            if (searchResult.Count > 0)
            {
                foreach (var objMenuDataModel in searchResult)
                {
                    if (!listResult.Contains(objMenuDataModel))
                    {
                        listResult.Add(objMenuDataModel);
                    }
                }
            }

            listResult = SetUserPreferenceMenu(listResult);

            return listResult;
        }

        public static string GetUserPreferedMenuCategory()
        {
            var userMenuCategoryType = string.Empty;

            var data = new MenuCategoryDataModel();

            try
            {
                data.MenuCategoryId = Convert.ToInt32(SessionVariables.UserMenuCategory);
            }
            catch
            {
                data.MenuCategoryId = 0;
            }

            var oMenu = Framework.Components.Core.MenuCategoryDataManager.GetDetails(data, SessionVariables.RequestProfile);

            userMenuCategoryType = oMenu.Description;

            return userMenuCategoryType;
        }

        public static List<MenuCategoryDataModel> GetMenuCategoryList()
        {
            var listMenuCategory = Framework.Components.Core.MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
            var listSiteMenu = SessionVariables.SiteMenuData;

            var lst = listSiteMenu.Select(x => x.PrimaryDeveloper.Trim()).Distinct().ToList();

            var resultMenuCategory = listMenuCategory.ToList();            

            foreach (var str in lst)
            {
                resultMenuCategory.Add(new MenuCategoryDataModel() { Name = "*" + str, MenuCategoryId = -1 });                
            }

            return resultMenuCategory;
        }

        public static void GenerateChildMenus(MenuItem menuItem)
        {
            var listSiteMenu = SessionVariables.SiteMenuData;
            if (listSiteMenu.Count <= 0) return;

            var listMenuResult = listSiteMenu
                            .Where(t => t.ParentMenuId != null && t.ParentMenuId.Value == int.Parse(menuItem.Value))
                            .Select(t => t)
                            .OrderBy(t => t.SortOrder);


            foreach (var objMenu in listMenuResult)
            {
                var menuText = objMenu.MenuDisplayName;

                if (SessionVariables.IsTesting && !string.IsNullOrEmpty(objMenu.PrimaryDeveloper))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(objMenu.ParentMenuId)))
                    {
                        menuText += " (" + objMenu.PrimaryDeveloper + ")";
                    }
                }

                var childMenuItem = new MenuItem(menuText);
                childMenuItem.Value = Convert.ToString(objMenu.MenuId);
                childMenuItem.NavigateUrl = GetMenuItemUrl(objMenu);
                var tempNavURL = string.Empty;

                GenerateChildMenus(childMenuItem);

                if (childMenuItem.ChildItems.Count >= 1)
                {
                    if (Convert.ToString(childMenuItem.NavigateUrl) == "#")
                    {
                        tempNavURL = Convert.ToString(childMenuItem.ChildItems[0].NavigateUrl);
                        childMenuItem.NavigateUrl = tempNavURL;
                    }
                    menuText += ApplicationCommon.DynamicMenuCharacter;
                    childMenuItem.Text = menuText;
                }

                //CheckMenuCategory(childMenuItem);

                menuItem.ChildItems.Add(childMenuItem);
            }
        }

        public static void GenerateUserPreferenceMenu(Menu navigationMenu)
        {
            navigationMenu.Items.Clear();
            navigationMenu.MaximumDynamicDisplayLevels = 5;

            var listUPMenu = SessionVariables.UserPreferedMenuData;

            // filter for current Application Module
            listUPMenu = listUPMenu
                .Where(x => x.ApplicationModule == SessionVariables.CurrentApplicationModuleCode)
                .ToList();

            if (listUPMenu != null && listUPMenu.Count > 0)
            {
                var parentMenus = listUPMenu
                            .Where(t => t.ParentMenuId == null)
                            .Select(t => t)
                            .OrderBy(t => t.SortOrder);

                MenuItem helpMenuItem = null;

                foreach (var objMenu in parentMenus)
                {
                    var menuDisplayName = objMenu.MenuDisplayName;
                    var menuText = menuDisplayName;

                    if (SessionVariables.IsTesting && !string.IsNullOrEmpty(objMenu.PrimaryDeveloper))
                    {
                        menuText += " (" + objMenu.PrimaryDeveloper + ")";
                    }

                    var menuItem = new MenuItem(menuText);
                    menuItem.Value = objMenu.MenuId.ToString();

                    GenerateUserPreferenceChildMenus(menuItem, listUPMenu);

                    navigationMenu.CssClass = "app-main-menu";

                    menuItem.NavigateUrl = GetMenuItemUrl(objMenu);

                    if (menuItem.NavigateUrl == "#" && menuItem.ChildItems.Count > 0)
                    {
                        menuItem.NavigateUrl = menuItem.ChildItems[0].NavigateUrl;
                    }

                    if (menuDisplayName.ToLower() == "help")
                    {
                        AddFixedMenus(menuItem);
                        helpMenuItem = menuItem;
                        continue;
                    }

                    navigationMenu.Items.Add(menuItem);
                }

                //add modules menu
                var modulesMenu = GetFixedModulesMenu();
                navigationMenu.Items.Add(modulesMenu);

                // if help menu is present add it last
                if (helpMenuItem != null)
                {
                    navigationMenu.Items.Add(helpMenuItem);
                }
            }
        }

        public static void GenerateUserPreferenceChildMenus(MenuItem menuItem, List<MenuDataModel> listUPMenu)
        {
            if (listUPMenu != null && listUPMenu.Count > 0)
            {
                var listResultMenu = listUPMenu
                            .Where(t => t.ParentMenuId != null && t.ParentMenuId == int.Parse(menuItem.Value))
                            .Select(t => t)
                            .OrderBy(t => t.SortOrder);

                foreach (var objMenu in listResultMenu)
                {

                    var menuText = objMenu.MenuDisplayName;

                    if (SessionVariables.IsTesting && !string.IsNullOrEmpty(objMenu.PrimaryDeveloper))
                    {
                        if (!string.IsNullOrEmpty(objMenu.PrimaryDeveloper))
                        {
                            menuText += " (" + objMenu.PrimaryDeveloper + ")";
                        }
                    }

                    var childMenuItem = new MenuItem(menuText);
                    childMenuItem.Value = Convert.ToString(objMenu.MenuId);
                    childMenuItem.NavigateUrl = GetMenuItemUrl(objMenu);

                    var tempNavURL = string.Empty;

                    GenerateUserPreferenceChildMenus(childMenuItem, listUPMenu);                    

                    if (childMenuItem.ChildItems.Count >= 1)
                    {
                        if (Convert.ToString(childMenuItem.NavigateUrl) == "#")
                        {
                            tempNavURL = Convert.ToString(childMenuItem.ChildItems[0].NavigateUrl);
                            childMenuItem.NavigateUrl = tempNavURL;
                        }

                        menuText += ApplicationCommon.DynamicMenuCharacter;
                        childMenuItem.Text = menuText;
                    }

                    menuItem.ChildItems.Add(childMenuItem);
                }
            }
        }

        public static List<MenuDataModel> SetUserPreferenceMenu(List<MenuDataModel> listUPMenu)
        {
            var listSiteMenu = SessionVariables.SiteMenuData;

            if (listSiteMenu != null && listSiteMenu.Count > 0)
            {
                var parentMenuList = listSiteMenu
                            .Where(t => t.ParentMenuId == null)
                            .Select(t => t)
                            .OrderBy(t => t.SortOrder);

                foreach (MenuDataModel objMenu in parentMenuList)
                {
                    var isExists = listUPMenu.Where(x => x.MenuId == objMenu.MenuId).Any();
                    var hasChild = SetUserPreferenceChildMenus(objMenu.MenuId.Value, listSiteMenu, listUPMenu);

                    if (isExists || hasChild)
                    {
                        if (!isExists)
                        {
                            listUPMenu.Add(objMenu);
                        }
                    }
                }
            }
            return listUPMenu;
        }

        public static bool SetUserPreferenceChildMenus(int parentMenuId, List<MenuDataModel> listSiteMenu, List<MenuDataModel> listUPMenu)
        {
            var hasChild = false;

            if (listSiteMenu != null && listSiteMenu.Count > 0)
            {
                var listMenuResult = listSiteMenu
                            .Where(t => t.ParentMenuId != null && t.ParentMenuId.Value == parentMenuId)
                            .Select(t => t)
                            .OrderBy(t => t.SortOrder);

                foreach (MenuDataModel objMenu in listMenuResult)
                {
                    //var tmpMenuId = dr[MenuDataModel.DataColumns.MenuId].ToString();

                    var isExists = listUPMenu.Where(x => x.MenuId == objMenu.MenuId).Any();
                    var tempNavURL = string.Empty;

                    var isChildExists = SetUserPreferenceChildMenus(objMenu.MenuId.Value, listSiteMenu, listUPMenu);

                    if (isChildExists || isExists)
                    {
                        if (!isExists)
                        {
                            listUPMenu.Add(objMenu);
                        }
                    }
                }
            }

            return hasChild;
        }        

        public static void AddFixedMenus(MenuItem menuItem)
        {

            var compilationModeText = "Compilation Mode - ";

#if DEBUG
            compilationModeText += "Debug";
#else
			compilationModeText += "Release";
#endif

            var childMenuItem = new MenuItem(compilationModeText);

            childMenuItem.Value = compilationModeText;

            menuItem.ChildItems.Add(childMenuItem);

        }

        public static MenuItem GetFixedModulesMenu()
        {
            var menuItemParent = new MenuItem();
            menuItemParent.Text = "Modules";

            var firstChildMenuUrl = string.Empty;
            var firstChildMenuText = string.Empty;

			string[] childMenuUrls = new string[3];
			string[] childMenuTexts = new string[3];

			if (SessionVariables.CurrentApplicationModuleCode == SessionVariables.CurrentApplicationCode)
			{
				firstChildMenuText = "Application Administration";
                firstChildMenuUrl = "~/" + SessionVariables.CurrentApplicationCode + "/AA/Home";
				//firstChildMenuUrl = "~/Configuration/Default.aspx";

                //("~/", "~/" + SessionVariables.CurrentApplicationCode + "/" + SessionVariables.CurrentApplicationModuleCode + "/")
			}
			else
			{
				var applicationInfo = ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId];
				firstChildMenuText = applicationInfo.Description;

				if (SessionVariables.CurrentApplicationCode == "RD")
				{
					firstChildMenuUrl = "~/ReferenceData/Home";
				}
				else if (SessionVariables.CurrentApplicationCode == "DC")
				{
					firstChildMenuUrl = "~/DayCare/Home";
				}
				else if (SessionVariables.CurrentApplicationCode == "CM")
				{
					firstChildMenuUrl += "~/CapitalMarkets/Home";
				}
				else if (SessionVariables.CurrentApplicationCode == "LEG")
				{
					firstChildMenuUrl += "~/Legal/Home";
				}
				else
				{
                    firstChildMenuUrl = "~/" + SessionVariables.CurrentApplicationCode + "/" + SessionVariables.CurrentApplicationCode + "/Home";
				}
            }

            childMenuTexts[0] = firstChildMenuText;
            childMenuUrls[0] = firstChildMenuUrl;

            childMenuTexts[1] = "Tools";
            childMenuUrls[1] = "#";

            childMenuTexts[2] = "Workflow Manager";
            childMenuUrls[2] = "#";

            menuItemParent.NavigateUrl = firstChildMenuUrl;

			for (int i = 0; i < childMenuUrls.Length; i++)
			{
				var childMenuItem = new MenuItem();
				childMenuItem.Text = childMenuTexts[i];
				childMenuItem.NavigateUrl = childMenuUrls[i];

				menuItemParent.ChildItems.Add(childMenuItem);
			}

            return menuItemParent;
        }

        #endregion

    }
}

#region removed

//private static void CheckMenuCategory(MenuItem childMenuItem)
//{
//	return;

//	var testing = "Testing";
//	var dt = Framework.Components.Core.MenuCategoryXMenuDataManager.GetByMenu(Convert.ToInt32(childMenuItem.Value), SessionVariables.RequestProfile.AuditId);
//	if (dt != null && dt.Rows.Count > 0)
//	{
//		foreach (System.Data.DataRow dr in dt.Rows)
//		{
//			if (SessionVariables.IsTesting && (Convert.ToString(dr[MenuCategoryXMenuDataModel.DataColumns.MenuCategory]) == testing))
//			{
//				childMenuItem.Text = "<div style='color:Yellow'>" + childMenuItem.Text + "</div>";
//				break;
//			}

//		}
//	}
//}

//		public static void SetMenuStyles()
//		{
//			 TODO: Move dynamic css 
//			MenuBackgroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBackgroundColor);
//			MenuForegroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuForegroundColor);
//			MenuHoverColor      = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuHoverColor);
//			MenuBorderColor     = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBorderColor);

//			MenuFontFamily      = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontFamily);
//			MenuFontSize        = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontSize);
//			MenuColoredCategoryColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuColoredCategoryColor);

//			MenuStyle = @"<style type='text/css'> 
//				            div.newClass
//				            {
//				            }
//
//				            div.newClass ul
//				            {
//					            list-style: none;
//					            padding: 0px;
//                                margin: 0px;
//					            width: auto;
//					            z-index: 999;
//				            }
//				
//				            div.newClass ul li a:active
//				            {
//					            /* background-color: #465c71; */ 
//					            color: #cfdbe6;
//					            text-decoration: none;
//				            }
//
//				            div.newClass ul li a, div.newClass ul li a:visited
//				            {
//				                display: block;
//				                /* line-height: 1.35em;
//                                padding: 4px 20px; */
//                                padding: 0px;
//				                text-decoration: none;
//				                white-space: nowrap;
//				                border: 1px solid " + MenuBorderColor + ";" +
//							"} " +

//							" div.newClass ul li a:hover	 " +
//							" { " +
//							"       color: #465c71; " +
//							"       text-decoration: none; " +
//							"       background-color:" + MenuHoverColor + " !important ; " +
//							" }  " +

//						"  </style> ";

//			var menuItemStyle = new StringBuilder();

//			menuItemStyle.AppendLine("<style type='text/css'>");

//			menuItemStyle.AppendLine("   span.stdMenuItem                                                           ");
//			menuItemStyle.AppendLine("   {                                                                      ");
//			menuItemStyle.AppendLine("       color:" + MenuForegroundColor + " !important ;                     ");
//			menuItemStyle.AppendLine("       background-color:" + MenuBackgroundColor + " !important;           ");
//			menuItemStyle.AppendLine("       font-family:" + MenuFontFamily + " !important;                     ");
//			menuItemStyle.AppendLine("       font-size:" + MenuFontSize + " !important;                         ");
//			menuItemStyle.AppendLine("       padding: 4px 20px !important;                     ");
//			menuItemStyle.AppendLine("       line-height: 1.35em;            ");
//			menuItemStyle.AppendLine("       display: block; text-decoration: none; white-space: nowrap;        ");
//			menuItemStyle.AppendLine("   } ");

//			menuItemStyle.AppendLine(" span.stdMenuItem:hover	                            ");
//			menuItemStyle.AppendLine(" {                                                    ");
//			menuItemStyle.AppendLine("      color: black  !important ;                      ");
//			menuItemStyle.AppendLine("      text-decoration: none;                          ");
//			menuItemStyle.AppendLine("      background:" + MenuHoverColor + " !important ;  ");
//			menuItemStyle.AppendLine(" }                                                    ");

//			menuItemStyle.AppendLine();

//			menuItemStyle.AppendLine("  span.highlightMenuItem                                                      ");
//			menuItemStyle.AppendLine("  {                                                                        ");
//			menuItemStyle.AppendLine("       background-color: yellow !important;                                ");
//			menuItemStyle.AppendLine("       font-family:" + MenuFontFamily + " !important;                      ");
//			menuItemStyle.AppendLine("       font-size:" + MenuFontSize + " !important;                          ");
//			menuItemStyle.AppendLine("       padding: 6px 22px !important;                  ");
//			menuItemStyle.AppendLine("       line-height: 1.35em;            ");
//			menuItemStyle.AppendLine("       display: block; text-decoration: none; white-space: nowrap;        ");
//			menuItemStyle.AppendLine("   } ");

//			menuItemStyle.AppendLine();

//			menuItemStyle.AppendLine("  span.highlightMenuCategoryMenuItem                                                      ");
//			menuItemStyle.AppendLine("  {                                                                        ");
//			menuItemStyle.AppendLine("       background-color:" + MenuColoredCategoryColor + " !important;                                ");
//			menuItemStyle.AppendLine("       font-family:" + MenuFontFamily + " !important;                      ");
//			menuItemStyle.AppendLine("       font-size:" + MenuFontSize + " !important;                          ");
//			menuItemStyle.AppendLine("       padding: 6px 22px !important;                  ");
//			menuItemStyle.AppendLine("       line-height: 1.35em;            ");
//			menuItemStyle.AppendLine("       display: block; text-decoration: none; white-space: nowrap;        ");
//			menuItemStyle.AppendLine("   } ");

//			menuItemStyle.AppendLine("</style>");

//			MenuItemStyle = menuItemStyle.ToString();
//		}
#endregion removed