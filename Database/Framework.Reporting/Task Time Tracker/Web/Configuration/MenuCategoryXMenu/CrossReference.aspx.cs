using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web;
using ASPMenu = System.Web.UI.WebControls.Menu;
using TTTMenu = Framework.Components.Core.MenuDataManager;
using System.Web.Security;


namespace Shared.UI.Web.Configuration.MenuCategoryXMenu
{
	public partial class CrossReference : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region Properties

		private static string MenuBackgroundColor { get; set; }

		private static string MenuForegroundColor { get; set; }

		private static string MenuHoverColor { get; set; }

		private static string MenuBorderColor { get; set; }

		private static string MenuFontFamily { get; set; }

		private static string MenuFontSize { get; set; }

		#endregion

		#region Methods

		private DataTable GetMenuList()
		{
			var dt = Framework.Components.Core.MenuDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedMenus(int menuCategoryId)
		{
			var id = Convert.ToInt32(drpMenuCategory.SelectedValue);
			var dt = Framework.Components.Core.MenuCategoryXMenuDataManager.GetByMenuCategory(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByMenuCategory(int menuCategoryId, List<int> menuIds)
		{
			var id = Convert.ToInt32(drpMenuCategory.SelectedValue);
			Framework.Components.Core.MenuCategoryXMenuDataManager.DeleteByMenuCategory(id, SessionVariables.RequestProfile);
			Framework.Components.Core.MenuCategoryXMenuDataManager.CreateByMenuCategory(id, menuIds.ToArray(), SessionVariables.RequestProfile);

            SessionVariables.UserPreferedMenuData = Shared.UI.Web.MenuHelper.GetUserPreferedMenu();
		}

		private DataTable GetMenuCategoryList()
		{
			var dt = Framework.Components.Core.MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}


		private void BindLists()
		{
			drpMenuCategory.DataSource = GetMenuCategoryList();
			drpMenuCategory.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpMenuCategory.DataValueField = MenuCategoryDataModel.DataColumns.MenuCategoryId;
			drpMenuCategory.DataBind();
            try
            {
                drpMenuCategory.SelectedValue = SessionVariables.UserMenuCategory;
            }
            catch { }
		}

        public List<MenuDataModel> GetUserPreferedMenu()
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

            var listMenuCategoryXMenu = Framework.Components.Core.MenuCategoryXMenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            var listSiteMenu = SessionVariables.SiteMenuData;

            // filter site menu for current application module           
            listResult = listSiteMenu
                .Where(x => x.ApplicationModule == SessionVariables.CurrentApplicationModuleCode)
                .ToList();

            var applicationMenuIds = listMenuCategoryXMenu.Select(x => x.MenuId).ToList();
            try
            {
                // appply filter to retreived records from db
                listResult = listResult
                    .Where(x => applicationMenuIds.Contains(x.MenuId.Value)).ToList();
            }
            catch
            {
                listResult.Clear();
            }

            //var filterCondition = MenuDataModel.DataColumns.PrimaryDeveloper + " = '" + SessionVariables.UserMenuCategory + "'";
            //filterCondition += "and " + MenuDataModel.DataColumns.ApplicationModule + " = '" + SessionVariables.CurrentApplicationModuleCode + "'";

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

            listResult = MenuHelper.SetUserPreferenceMenu(listResult);

            return listResult;
		}

		public void GenerateUserPreferenceTestMenu(ASPMenu navigationMenu)
		{
			 
			navigationMenu.Items.Clear();
			GenerateMenuStyle();
            var listUPMenu = SessionVariables.UserPreferedMenuData;

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

                    GenerateUserPreferenceTestChildMenus(menuItem, listUPMenu);

                    navigationMenu.CssClass = "app-main-menu";

                    menuItem.NavigateUrl = MenuHelper.GetMenuItemUrl(objMenu);

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

                // if help menu is present add it last
                if (helpMenuItem != null)
                {
                    navigationMenu.Items.Add(helpMenuItem);
                }
            }
		}

        public static void GenerateUserPreferenceTestChildMenus(MenuItem menuItem, List<MenuDataModel> listUPMenu)
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
                    childMenuItem.NavigateUrl = MenuHelper.GetMenuItemUrl(objMenu);

                    var tempNavURL = string.Empty;

                    GenerateUserPreferenceTestChildMenus(childMenuItem, listUPMenu);

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
			//childMenuItem.NavigateUrl = "#";

			menuItem.ChildItems.Add(childMenuItem);

		}

		private static void GenerateMenuStyle()
		{
			MenuBackgroundColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBackgroundColor);
			MenuForegroundColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuForegroundColor);
			MenuHoverColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuHoverColor);
			MenuBorderColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuBorderColor);

			MenuFontFamily = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontFamily);
			MenuFontSize = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.MenuFontSize);

			var currentPage = HttpContext.Current.CurrentHandler as Page;

			currentPage.Header.Controls.Add(new LiteralControl(@"<style type='text/css'> 
				div.app-main-menu
				{
				}

				div.app-main-menu ul
				{
					list-style: none;
					margin: 0px;
					padding: 0px;
					width: auto;
					z-index: 999;
				}
				
				div.app-main-menu ul li a:active
				{
					background-color: #465c71;
					color: #cfdbe6;
					text-decoration: none;
				}
				div.app-main-menu ul li a, div.app-main-menu ul li a:visited
				{
				display: block;
				line-height: 1.35em;
				padding: 4px 20px;
				text-decoration: none;
				white-space: nowrap;
				border: 1px solid " + MenuBorderColor + ";color:" + MenuForegroundColor + " ;background-color:" + MenuBackgroundColor + ";font-family:" + MenuFontFamily + ";font-size:" + MenuFontSize + "; }div.menu ul li a:hover	{color: #465c71;text-decoration: none;background-color:" + MenuHoverColor + ";} </style> "));

		}
		
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			BindLists();
			BucketOfMenu.ConfigureBucket("Menu", 1, GetMenuList, GetAssociatedMenus, SaveByMenuCategory);			
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			SettingCategory = "MenuCategoryXMenuDefaultView";
			var sbm = this.Master.SubMenuObject;
			

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			//bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			//bcControl.Setup("Cross Reference");
			//bcControl.GenerateMenu();
			GenerateUserPreferenceTestMenu(NavigationTestMenu);

		}

		
		protected void drpMenuCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfMenu.ReloadBucketList();
			GenerateUserPreferenceTestMenu(NavigationTestMenu);
		}



		#endregion
	}
}