using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using System.Drawing;
using Shared.UI.Web;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.ComponentModel;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Controls.BreadCrumb
{
    public partial class BreadCrumb : BaseControl
    {

        #region Variables

        private StringBuilder sbResult = new StringBuilder();              // Holds the Breadcrumb HTML.
        private StringBuilder sbBcUrl = new StringBuilder();               // Holds the URL of the breadcrumb.  Directories are appended in succession to the root.
        private StringBuilder strActiveMenuUrl = new StringBuilder();
        private StringBuilder strParentMenuUrl = new StringBuilder();

	    public bool ShowFileName { get; set; }

	    public string Separator { get; set; }

	    // RootUrl.  URL of the root directory.  Default is "/".
	    public string RootUrl { get; set; }

	    public string RootName { get; set; }

	    public string PageName
        {
            get
            {
                return Convert.ToString(ViewState["PageName"]);
            }
            set
            {
                ViewState["PageName"] = value;
            }
        }

	    public int ActiveMenuId { get; set; }

	    public bool IsAddedInVisibilityManager { get; set; }

        #endregion

        #region Methods

        public void Setup(string pageName)
        {
			ActiveMenuId = int.Parse(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ActiveMenuId, SettingCategory));             
            PageName = pageName;
            SetUpStandardSettings(SettingCategory);
        }

        private void SetUpStandardSettings(string settingCategory)
        {
            
			var parentmenuId = int.Parse(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ParentMenuId, settingCategory));
            var activemenuId = int.Parse(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ActiveMenuId, settingCategory));

            if (parentmenuId < 0 || activemenuId < 0)
            {
                const string constpostfix = "DefaultViewBreadCrumbControl";

                var entity = settingCategory.Remove(settingCategory.Length - constpostfix.Length);

                entity = entity.Replace(" ", string.Empty);
                
				var menudata = new MenuDataModel();
                menudata.Name = entity;
				menudata.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
				var menudt = MenuDataManager.Search(menudata, SessionVariables.RequestProfile);
                
				if (menudt.Rows.Count == 1)
                {
                    try
                    {
                        parentmenuId = int.Parse(menudt.Rows[0][MenuDataModel.DataColumns.ParentMenuId].ToString());
                        activemenuId = int.Parse(menudt.Rows[0][MenuDataModel.DataColumns.MenuId].ToString());

                        PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ParentMenuId, parentmenuId.ToString());
                        PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ActiveMenuId, activemenuId.ToString());
                    }
                    catch (Exception ex) { }
                }

                if (menudt.Rows.Count > 1)
                {
                    for (var i = 0; i < menudt.Rows.Count; i++)
                    {
                        if (!(menudt.Rows[i][MenuDataModel.DataColumns.NavigateURL].ToString().Equals("#")) &&
                            menudt.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString().Equals(entity))
                        {
                            try
                            {
                                parentmenuId = int.Parse(menudt.Rows[i][MenuDataModel.DataColumns.ParentMenuId].ToString());
                                activemenuId = int.Parse(menudt.Rows[i][MenuDataModel.DataColumns.MenuId].ToString());

                                PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ParentMenuId, parentmenuId.ToString());
                                PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ActiveMenuId, activemenuId.ToString());
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
        }

        private string GetParentMenuUrl(MenuDataModel objMenu)
        {
            var menuText = objMenu.MenuDisplayName;

            if (SessionVariables.IsTesting && !string.IsNullOrEmpty(objMenu.PrimaryDeveloper))
            {
                menuText += " (" + objMenu.PrimaryDeveloper + ")";
            }

	        var menuItem = new MenuItem(menuText);
            
			menuItem.Value = Convert.ToString(objMenu.MenuId);		
			menuItem.NavigateUrl = MenuHelper.GetMenuItemUrl(objMenu);
			
            MenuHelper.GenerateChildMenus(menuItem);
            
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

		//public void GenerateMenu()
		//{
		//	ShowFileName = false;
		//	Separator = ">";
		//	RootUrl = "/";
		//	RootName = "Home";

		//	sbResult.Append("<a href=\"" + RootUrl + "\">" + RootName + "</a> ");
		//	//sbResult.Append(Separator + " ");

		//	var strHostUrl = "http://" + Page.Request.ServerVariables["HTTP_HOST"] + "/";
		//	sbBcUrl.Append(strHostUrl);

		//	var dtMenu = SessionVariables.SiteMenuData;

		//	if (dtMenu != null && dtMenu.Rows.Count > 0)
		//	{
		//		var ActiveMenuFilter = "[" + MenuDataModel.DataColumns.MenuId + "] =" + ActiveMenuId;
		//		var drs = dtMenu.Select(ActiveMenuFilter);
		//		if (drs.Length > 0)
		//		{
		//			foreach (DataRow dr in drs)
		//			{
		//				var ActiveMenuText = Convert.ToString(dr[MenuDataModel.DataColumns.MenuDisplayName]);
		//				var ParentMenuText = String.Empty;
		//				var ActiveMenuUrl = String.Empty;
						

		//				if (Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]) != "#")
		//				{
		//					ActiveMenuUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]).Remove(0, 2);
		//				}
		//				else
		//				{
		//					ActiveMenuUrl = GetParentMenuUrl(dr, dtMenu);
		//				}
																
		//				var strEntityName = dr[MenuDataModel.DataColumns.Name].ToString();
		//				strActiveMenuUrl.Append(strHostUrl);
		//				strActiveMenuUrl.Append(ActiveMenuUrl);
		//				var strBreadText = ActiveMenuText;
		//				var ParentMenuTextId = Convert.ToString(dr[MenuDataModel.DataColumns.ParentMenuId]);

		//				while (!string.IsNullOrEmpty(Convert.ToString(ParentMenuTextId)))
		//				{
		//					var MainParentMenuTextFilter = "[" + MenuDataModel.DataColumns.MenuId + "] =" + ParentMenuTextId;
		//					var drs1 = dtMenu.Select(MainParentMenuTextFilter);

		//					if (drs1.Length <= 0) continue;

		//					foreach (var dr1 in drs1)
		//					{
		//						ParentMenuText = Convert.ToString(dr1[MenuDataModel.DataColumns.MenuDisplayName]);
		//						ParentMenuTextId = Convert.ToString(dr1[MenuDataModel.DataColumns.ParentMenuId]);

		//						var parentMenuUrl = String.Empty;
		//						var dtUPMenu = SessionVariables.UserPreferedMenuData;
		//						var drUPs = dtUPMenu.Select(MenuCategoryXMenuDataModel.DataColumns.MenuId + " = " + dr1[MenuDataModel.DataColumns.MenuId]);
		                        
		//						//var isExists = false;

		//						if (drUPs.Length > 0)
		//						{

		//							if (Convert.ToString(dr1[MenuDataModel.DataColumns.NavigateURL]) != "#")
		//							{
		//								parentMenuUrl = Convert.ToString(dr1[MenuDataModel.DataColumns.NavigateURL]).Remove(0, 2);
		//							}
		//							else
		//							{
		//								parentMenuUrl = GetParentMenuUrl(dr1, dtMenu);
		//							}
		//						}
		//						else
		//						{
		//							parentMenuUrl = ActiveMenuUrl;
		//						}

		//						strParentMenuUrl.Clear();
		//						strParentMenuUrl.Append(strHostUrl);
		//						strParentMenuUrl.Append(parentMenuUrl);

		//						//if (PageName != "")
		//						//{
		//						//    strBreadText = String.Format("<a href={0}>{1}</a>", strParentMenuUrl, ParentMenuText);
		//						//}
		//						//else
		//						strBreadText = String.Format("<a href={0}>{1}</a>", strParentMenuUrl, ParentMenuText) + strBreadText;

		//					}
		//				}

		//				var index = strBreadText.LastIndexOf(">");
		//				strBreadText = strBreadText.Substring(0, index+1);
		//				strBreadText += "  " + String.Format("<a href={0}>{1}</a>", strHostUrl+ActiveMenuUrl, ActiveMenuText);

		//				if (PageName != string.Empty)
		//				{
		//					strBreadText += "  " + String.Format("<a href={0}>{1}</a>", '#', PageName);
		//				}
                       							
		//			   sbResult.Append(strBreadText);					

		//			}
		//		}
		//	}
		//}

		public void GenerateMenu()
		{
			ShowFileName = false;
			Separator = ">";
			RootUrl = "/";
			RootName = "Home";
			var spanTag = "<span class=\"divider\"></span>  ";

			sbResult.Append("<li><a href=\"" + RootUrl + "\">" + RootName + "</a> " + spanTag + "</li>");
			//sbResult.Append(Separator + " ");

			var strHostUrl = "http://" + Page.Request.ServerVariables["HTTP_HOST"] + "/";
			sbBcUrl.Append(strHostUrl);

			var listSiteMenu = SessionVariables.SiteMenuData;

			if (listSiteMenu != null && listSiteMenu.Count > 0)
			{
				var listResultMenu = listSiteMenu.Where(x => x.MenuId.Value == ActiveMenuId).ToList();

				if (listResultMenu.Count > 0)
				{
					foreach (var objMenu in listResultMenu)
					{
                        var activeMenuText = objMenu.MenuDisplayName;
						var parentMenuText = String.Empty;
						var activeMenuUrl = String.Empty;

						if (objMenu.NavigateURL != "#")
						{
							activeMenuUrl = MenuHelper.GetMenuItemUrl(objMenu).Remove(0, 2);
						}
						else
						{
                            activeMenuUrl = GetParentMenuUrl(objMenu);
						}
						
						var strEntityName = objMenu.Name.ToString();

						strActiveMenuUrl.Append(strHostUrl);
						strActiveMenuUrl.Append(activeMenuUrl);

						var strBreadText = activeMenuText;

						var parentMenuTextId = objMenu.ParentMenuId;

						while (!string.IsNullOrEmpty(Convert.ToString(parentMenuTextId)))
						{
                            var drs1 = listSiteMenu.Where(x => x.MenuId.Value == parentMenuTextId).ToList();

							if (drs1.Count <= 0) continue;

							foreach (var objChildMenu in drs1)
							{
								parentMenuText = objChildMenu.MenuDisplayName;
								parentMenuTextId = objChildMenu.ParentMenuId;

								var parentMenuUrl = String.Empty;
								var listUPMenu = SessionVariables.UserPreferedMenuData;
                                var listUPs = listUPMenu.Where(x => x.MenuId.Value == objChildMenu.MenuId).ToList();

								//var isExists = false;

                                if (listUPs.Count > 0)
								{
									if (objChildMenu.NavigateURL != "#")
									{
										parentMenuUrl = objChildMenu.NavigateURL.Remove(0, 2);
									}
									else
									{
                                        parentMenuUrl = GetParentMenuUrl(objChildMenu);
									}
								}
								else
								{
									parentMenuUrl = activeMenuUrl;
								}

								strParentMenuUrl.Clear();
								strParentMenuUrl.Append(strHostUrl);
								strParentMenuUrl.Append(parentMenuUrl);

								//if (PageName != "")
								//{
								//    strBreadText = String.Format("<a href={0}>{1}</a>", strParentMenuUrl, ParentMenuText);
								//}
								//else
								strBreadText = String.Format("<li><a href={0}>{1}</a> ", strParentMenuUrl, parentMenuText) + spanTag + "</li>" + strBreadText;

							}
						}

						var index = strBreadText.LastIndexOf(">");
						strBreadText = strBreadText.Substring(0, index + 1);
						strBreadText += "  " + String.Format("<li><a href={0}>{1}</a> ", strHostUrl + activeMenuUrl, activeMenuText) + spanTag + "</li>";

						if (PageName != string.Empty)
						{
							strBreadText += "  " + String.Format("<li><a href={0}>{1}</a></li>", '#', PageName);
						}

						sbResult.Append(strBreadText);

					}
				}
			}
		}

        #endregion

        #region Events

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(sbResult.ToString());
        }

        public void Setup(int activeMenuId, string pageName)
        {
            ActiveMenuId = activeMenuId;
            PageName = pageName;
        }

        #endregion

    }
}





