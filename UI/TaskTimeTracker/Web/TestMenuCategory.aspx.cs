using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web;
using ASPMenu = System.Web.UI.WebControls.Menu;
using TTTMenu = Framework.Components.Core.MenuDataManager;
using System.Web.Security;

namespace TaskTimeTracker.UI.Web
{
	public partial class TestMenuCategory : System.Web.UI.Page
	{
		#region Properties

		private static string MenuBackgroundColor { get; set; }

		private static string MenuForegroundColor { get; set; }

		private static string MenuHoverColor { get; set; }

		private static string MenuBorderColor { get; set; }

		private static string MenuFontFamily { get; set; }

		private static string MenuFontSize { get; set; }

		#endregion

		#region events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				bindMenuCateogry();
			}
			GenerateUserPreferenceTestMenu(NavigationTestMenu);
			GenerateMenu();
		}

		protected void drpMenuCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			GenerateUserPreferenceTestMenu(NavigationTestMenu);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var bcm = this.Master.BreadCrumbObject;


			bcm.SettingCategory = "TestMenuCategory" + "DefaultViewBreadCrumbControl";
			bcm.Setup("Default");
			bcm.GenerateMenuTestBootStrap();
		}

	

		#endregion

		#region methods

		public void GenerateMenu()
		{
			StringBuilder sbResult = new StringBuilder();              // Holds the Breadcrumb HTML.
			StringBuilder strActiveMenuUrl = new StringBuilder();
			StringBuilder strParentMenuUrl = new StringBuilder();
			var ShowFileName = false;
			var Separator = ">";
			var RootUrl = "/";
			var RootName = "Home";
			var spanTag = "<span class=\"divider\">></span>  ";

			sbResult.Append("<li><a href=\"" + RootUrl + "\">" + RootName + "</a> " + spanTag + "</li>");
			//sbResult.Append(Separator + " ");

			var strHostUrl = "http://" + Page.Request.ServerVariables["HTTP_HOST"] + "/";
			//sbBcUrl.Append(strHostUrl);

			var dtMenu = SessionVariables.SiteMenuData;

			if (dtMenu != null && dtMenu.Rows.Count > 0)
			{
				var ActiveMenuFilter = "[" + MenuDataModel.DataColumns.MenuId + "] =" + 964990;
				var drs = dtMenu.Select(ActiveMenuFilter);
				if (drs.Length > 0)
				{
					foreach (DataRow dr in drs)
					{
						var ActiveMenuText = Convert.ToString(dr[MenuDataModel.DataColumns.MenuDisplayName]);
						var ParentMenuText = String.Empty;
						var ActiveMenuUrl = String.Empty;


						if (Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]) != "#")
						{
							ActiveMenuUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]).Remove(0, 2);
						}
						else
						{
							ActiveMenuUrl = GetParentMenuUrl(dr, dtMenu);
						}

						var strEntityName = dr[MenuDataModel.DataColumns.Name].ToString();
						strActiveMenuUrl.Append(strHostUrl);
						strActiveMenuUrl.Append(ActiveMenuUrl);
						var strBreadText = ActiveMenuText;
						var ParentMenuTextId = Convert.ToString(dr[MenuDataModel.DataColumns.ParentMenuId]);

						while (!string.IsNullOrEmpty(Convert.ToString(ParentMenuTextId)))
						{
							var MainParentMenuTextFilter = "[" + MenuDataModel.DataColumns.MenuId + "] =" + ParentMenuTextId;
							var drs1 = dtMenu.Select(MainParentMenuTextFilter);

							if (drs1.Length <= 0) continue;

							foreach (var dr1 in drs1)
							{
								ParentMenuText = Convert.ToString(dr1[MenuDataModel.DataColumns.MenuDisplayName]);
								ParentMenuTextId = Convert.ToString(dr1[MenuDataModel.DataColumns.ParentMenuId]);

								var parentMenuUrl = String.Empty;
								var dtUPMenu = SessionVariables.UserPreferedMenuData;
								var drUPs = dtUPMenu.Select(MenuCategoryXMenuDataModel.DataColumns.MenuId + " = " + dr1[MenuDataModel.DataColumns.MenuId]);

								//var isExists = false;

								if (drUPs.Length > 0)
								{

									if (Convert.ToString(dr1[MenuDataModel.DataColumns.NavigateURL]) != "#")
									{
										parentMenuUrl = Convert.ToString(dr1[MenuDataModel.DataColumns.NavigateURL]).Remove(0, 2);
									}
									else
									{
										parentMenuUrl = GetParentMenuUrl(dr1, dtMenu);
									}
								}
								else
								{
									parentMenuUrl = ActiveMenuUrl;
								}

								strParentMenuUrl.Clear();
								strParentMenuUrl.Append(strHostUrl);
								strParentMenuUrl.Append(parentMenuUrl);

								//if (PageName != "")
								//{
								//    strBreadText = String.Format("<a href={0}>{1}</a>", strParentMenuUrl, ParentMenuText);
								//}
								//else
								strBreadText = String.Format("<li> <a href={0}>{1}</a> ", strParentMenuUrl, ParentMenuText) + spanTag + "</li>" + strBreadText;

							}
						}

						var index = strBreadText.LastIndexOf(">");
						strBreadText = strBreadText.Substring(0, index + 1);
						strBreadText += "  " + String.Format("<a href={0}>{1}</a> ", strHostUrl + ActiveMenuUrl, ActiveMenuText) + spanTag;
						var PageName = "TestMenuCategory";
						if (PageName != string.Empty)
						{
							strBreadText += "  " + String.Format("<li class=\"active\"><a href={0}>{1}</a></li>", '#', PageName);
						}

						sbResult.Append(strBreadText);

					}
				}
			}
		}

		private string GetParentMenuUrl(DataRow dr, DataTable dtMenu)
		{
			var menuText = Convert.ToString(dr[MenuDataModel.DataColumns.MenuDisplayName]);

			if (SessionVariables.IsTesting && !string.IsNullOrEmpty(Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper])))
			{
				menuText += " (" + Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper]) + ")";
			}

			var menuItem = new MenuItem(menuText);

			menuItem.Value = Convert.ToString(dr[MenuDataModel.DataColumns.MenuId]);
			menuItem.NavigateUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]);

			MenuHelper.GenerateChildMenus(menuItem, dtMenu);

			var tempNavURL = string.Empty;
			var parentMenuUrl = string.Empty;

			if (menuItem.ChildItems.Count >= 1)
			{
				if (Convert.ToString(menuItem.NavigateUrl) == "#")
				{

					tempNavURL = Convert.ToString(menuItem.ChildItems[0].NavigateUrl);
					menuItem.NavigateUrl = tempNavURL.Remove(0, 2);
					parentMenuUrl = menuItem.NavigateUrl;
				}

			}

			return parentMenuUrl;
		}
				
		public DataTable GetUserPreferedMenu()
		{
			DataTable dt = null;
			
			var data = new MenuCategoryXMenuDataModel();
			try
			{
				data.MenuCategoryId = Convert.ToInt32(drpMenuCategory.SelectedValue);
			}
			catch
			{
				data.MenuCategoryId = 0;
			}

			dt = Framework.Components.Core.MenuCategoryXMenuDataManager.Search(data, SessionVariables.AuditId);

			var dtSiteMenu = SessionVariables.SiteMenuData;

			var rows = dtSiteMenu.Select(MenuDataModel.DataColumns.PrimaryDeveloper + " = '" + SessionVariables.UserMenuCategory + "'");
			if (rows.Length > 0)
			{
				foreach (DataRow row in rows)
				{
					var newRow = dt.NewRow();
					newRow[MenuCategoryXMenuDataModel.DataColumns.MenuId] = row[MenuDataModel.DataColumns.MenuId];
					dt.Rows.Add(newRow);
				}
			}

			return dt;
		}


		public void GenerateUserPreferenceTestMenu(ASPMenu navigationMenu)
		{
			navigationMenu.Items.Clear();
			GenerateMenuStyle();
			DataTable dtMenu = SessionVariables.SiteMenuData;

			var data = new MenuCategoryXMenuDataModel();
			try
			{
				data.MenuCategoryId = Convert.ToInt32(drpMenuCategory.SelectedValue);
			}
			catch
			{
				data.MenuCategoryId = 0;
			}

			var dtUPMenu = Framework.Components.Core.MenuCategoryXMenuDataManager.Search(data, SessionVariables.AuditId);

			if (dtMenu != null && dtMenu.Rows.Count > 0)
			{
				var drs = dtMenu.AsEnumerable()
							.Where(t => t.Field<int?>(MenuDataModel.DataColumns.ParentMenuId) == null)
							.Select(t => t)
							.OrderBy(t => t.Field<int>("SortOrder"));

				foreach (DataRow dr in drs)
				{

					var drUPs = dtUPMenu.Select(MenuCategoryXMenuDataModel.DataColumns.MenuId + " = " + dr[MenuDataModel.DataColumns.MenuId]);
					var isExists = false;

					if (drUPs.Length > 0)
					{
						isExists = true;
					}

					var menuDisplayName = Convert.ToString(dr[MenuDataModel.DataColumns.MenuDisplayName]);
					var menuText = menuDisplayName;
					if (SessionVariables.IsTesting && !string.IsNullOrEmpty(Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper])))
					{
						menuText += " (" + Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper]) + ")";
					}
					var menuItem = new MenuItem(menuText);
					menuItem.Value = Convert.ToString(dr[MenuDataModel.DataColumns.MenuId]);

					var hasChild = GenerateUserPreferenceTestChildMenus(menuItem, dtMenu, dtUPMenu);

					navigationMenu.CssClass = "newClass";

					if (isExists || hasChild)
					{
						menuItem.NavigateUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]);

						if (menuDisplayName.ToLower() == "help")
						{
							AddFixedMenus(menuItem);
						}

						navigationMenu.Items.Add(menuItem);
					}
				}
			}
		}

		public static bool GenerateUserPreferenceTestChildMenus(MenuItem menuItem, DataTable dtMenu, DataTable dtUPMenu)
		{
			var hasChild = false;
			if (dtMenu != null && dtMenu.Rows.Count > 0)
			{
				var drs = dtMenu.AsEnumerable()
							.Where(t => t.Field<object>(MenuDataModel.DataColumns.ParentMenuId) != DBNull.Value)
							.Where(t => t.Field<int?>(MenuDataModel.DataColumns.ParentMenuId) != null)
							.Where(t => t.Field<int>(MenuDataModel.DataColumns.ParentMenuId) == int.Parse(menuItem.Value))
							.Select(t => t)
							.OrderBy(t => t.Field<int>("SortOrder"));


				foreach (DataRow dr in drs)
				{
					var menuText = Convert.ToString(dr[MenuDataModel.DataColumns.MenuDisplayName]);
					if (SessionVariables.IsTesting && !string.IsNullOrEmpty(Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper])))
					{
						if (!string.IsNullOrEmpty(Convert.ToString(dr[MenuDataModel.DataColumns.ParentMenuId])))
						{
							menuText += " (" + Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper]) + ")";
						}
					}

					var childMenuItem = new MenuItem(menuText);
					childMenuItem.Value = Convert.ToString(dr[MenuDataModel.DataColumns.MenuId]);
					childMenuItem.NavigateUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]);
					var isExistsRows = dtUPMenu.Select(MenuDataModel.DataColumns.MenuId + " = " + dr[MenuDataModel.DataColumns.MenuId]);
					var tempNavURL = "";

					var isChildExists = GenerateUserPreferenceTestChildMenus(childMenuItem, dtMenu, dtUPMenu);

					if (isChildExists || isExistsRows.Length > 0)
					{
						hasChild = true;
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
				div.newClass
				{
				}

				div.newClass ul
				{
					list-style: none;
					margin: 0px;
					padding: 0px;
					width: auto;
					z-index: 999;
				}
				
				div.newClass ul li a:active
				{
					background-color: #465c71;
					color: #cfdbe6;
					text-decoration: none;
				}
				div.newClass ul li a, div.newClass ul li a:visited
				{
				display: block;
				line-height: 1.35em;
				padding: 4px 20px;
				text-decoration: none;
				white-space: nowrap;
				border: 1px solid " + MenuBorderColor + ";color:" + MenuForegroundColor + " ;background-color:" + MenuBackgroundColor + ";font-family:" + MenuFontFamily + ";font-size:" + MenuFontSize + "; }div.newClass ul li a:hover	{color: #465c71;text-decoration: none;background-color:" + MenuHoverColor + ";} </style> "));

		}

		private void bindMenuCateogry()
		{
			//var dt = Framework.Components.Core.MenuCategory.GetList(SessionVariables.AuditId);
			var dt = SessionVariables.MenuCategoryList;
			UIHelper.LoadDropDown(dt, drpMenuCategory, MenuCategoryDataModel.DataColumns.Name,
				MenuCategoryDataModel.DataColumns.MenuCategoryId);

		}

		#endregion

		

	}
}