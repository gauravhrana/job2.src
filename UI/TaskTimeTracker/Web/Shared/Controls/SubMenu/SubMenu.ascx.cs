using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Controls.SubMenu
{
    public partial class SubMenu : BaseControl
    {
	    public int ParentMenuId { get; set; }

	    public int ActiveMenuId { get; set; }

	    private string SubMenuBackgroundColor
		{
			get
			{
				if (ViewState["SubMenuBackgroundColor"] != null)
				{
					return Convert.ToString(ViewState["SubMenuBackgroundColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuBackgroundColor"] = value;
			}
		}

		private string SubMenuForegroundColor
		{
			get
			{
				if (ViewState["SubMenuForegroundColor"] != null)
				{
					return Convert.ToString(ViewState["SubMenuForegroundColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuForegroundColor"] = value;
			}
		}
				
		private string SubMenuHoverColor
		{
			get
			{
				if (ViewState["SubMenuHoverColor"] != null)
				{
					return Convert.ToString(ViewState["SubMenuHoverColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuHoverColor"] = value;
			}
		}

		private string SubMenuBorderStyle
		{
			get
			{
				if (ViewState["SubMenuBorderStyle"] != null)
				{
					return Convert.ToString(ViewState["SubMenuBorderStyle"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuBorderStyle"] = value;
			}
		}
				
		private string SubMenuBorderColor
		{
			get
			{
				if (ViewState["SubMenuBorderColor"] != null)
				{
					return Convert.ToString(ViewState["SubMenuBorderColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuBorderColor"] = value;
			}
		}

		private string SubMenuFontFamily
		{
			get
			{
				if (ViewState["SubMenuFontFamily"] != null)
				{
					return Convert.ToString(ViewState["SubMenuFontFamily"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuFontFamily"] = value;
			}
		}

		private string SubMenuFontSize
		{
			get
			{
				if (ViewState["SubMenuFontSize"] != null)
				{
					return Convert.ToString(ViewState["SubMenuFontSize"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuFontSize"] = value;
			}
		}

		private string TopBackgroundColor
		{
			get
			{
				if (ViewState["TopBackgroundColor"] != null)
				{
					return Convert.ToString(ViewState["TopBackgroundColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["TopBackgroundColor"] = value;
			}
		}
				
		private string SubMenuTopBorderColor
		{
			get
			{
				if (ViewState["SubMenuTopBorderColor"] != null)
				{
					return Convert.ToString(ViewState["SubMenuTopBorderColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SubMenuTopBorderColor"] = value;
			}
		}

		public void Setup()
        {
			ParentMenuId = int.Parse(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ParentMenuId, SettingCategory));
			ActiveMenuId = int.Parse(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ActiveMenuId, SettingCategory));            

            SetUpStandardSettings(SettingCategory);
        }

        public void Setup(int parentMenuId, int activeMenuId)
        {
            
            ParentMenuId = parentMenuId;
            ActiveMenuId = activeMenuId;

            SetUpStandardSettings(SettingCategory);
        }

        private void SetUpStandardSettings(string settingCategory)
        {
			PreferenceUtility.CreateUserPreferenceCategoryIfNotExists(settingCategory, settingCategory);
            
			var parentmenuId = int.Parse(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ParentMenuId, settingCategory));
            var activemenuId = int.Parse(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ActiveMenuId, settingCategory));

            if (parentmenuId < 0 && activemenuId < 0)
            {
                var constPostFix = "DefaultViewSubMenuControl";
                var entity = settingCategory.Remove(settingCategory.Length - constPostFix.Length);

                //var entity = settingCategory.Remove(settingCategory.Length - 11);
                entity                 = entity.Replace(" ", "");
                var menudata           = new MenuDataModel();
                menudata.Name          = entity;
				menudata.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
				var menudt = MenuDataManager.GetEntityDetails(menudata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
                
				if (menudt.Count == 1)
                {
                    try
                    {
                        parentmenuId = menudt[0].ParentMenuId.Value;
                        activemenuId = menudt[0].MenuId.Value;

                        PreferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ParentMenuId, parentmenuId.ToString());
                        PreferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ActiveMenuId, activemenuId.ToString());
                    }
                    catch (Exception ex) { }
                }
                else if (menudt.Count > 1)
                {
                    for (var i = 0; i < menudt.Count; i++)
                    {
                        if (!(menudt[i].NavigateURL.Equals("#")) && menudt[i].Name.Equals(entity))
                        {
                            try
                            {
                                parentmenuId = menudt[i].ParentMenuId.Value;
                                activemenuId = menudt[i].MenuId.Value;

                                PreferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ParentMenuId, parentmenuId.ToString());
                                PreferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ActiveMenuId, activemenuId.ToString());
                            }
                            catch (Exception ex) { }
                        }
                    }
                }

            }
            else
                return;
        }

		public void GenerateMenu()
		{
			var listSiteMenu = SessionVariables.SiteMenuData;

			var i = 0;

			if (listSiteMenu != null && listSiteMenu.Count > 0)
			{
				var filter = "[" + MenuDataModel.DataColumns.ParentMenuId + "] =" + ParentMenuId;
				var listMenuResult = listSiteMenu.Where(x => x.ParentMenuId != null && x.ParentMenuId.Value == ParentMenuId).ToList();

				if (listMenuResult.Count > 0)
				{

					TopBackgroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBackgroundColor);
					SubMenuTopBorderColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor);

					divTopBar.Attributes.Add("style", "border:2px solid;border-bottom-style:hidden;");
					divTopBar.Style.Add("border-color", SubMenuTopBorderColor);

					divTopBar.Style.Add("background-color", TopBackgroundColor);


					//if (TopBackgroundColor == String.Empty)
					//    divTopBar.Style.Add("background-color", "#465c71");
					//else


					hypSettings.Style.Add("visibility", "hidden");
					lnkClose.Style.Add("visibility", "hidden");

					divTopBar.Attributes.Add("onmouseover", "document.getElementById('" + hypSettings.ClientID + "').style.visibility='visible';document.getElementById('" + lnkClose.ClientID + "').style.visibility='visible';");
					divTopBar.Attributes.Add("onmouseout", "document.getElementById('" + hypSettings.ClientID + "').style.visibility='hidden';document.getElementById('" + lnkClose.ClientID + "').style.visibility='hidden';");

					Image1.Attributes.Add("style", "padding-left:10px;padding-top:2px");

					foreach (var objMenu in listMenuResult)
					{
						// Modified the below block of code for Menu Category based SubMenu
                        var listUPMenu = SessionVariables.UserPreferedMenuData;
                        var listUPs = listUPMenu.Where(x => x.MenuId.Value == objMenu.MenuId).ToList();
                        var isExists = listUPs.Count > 0;

						var menuText = objMenu.MenuDisplayName;

						if (SessionVariables.IsTesting && !string.IsNullOrEmpty(objMenu.PrimaryDeveloper))
						{
							menuText += " (" + objMenu.PrimaryDeveloper + ")";
						}

						var menuItem = new MenuItem(menuText);
						menuItem.Value = Convert.ToString(objMenu.MenuId);

						//
						GenerateMenuCategoryChildSubMenus(menuItem, listUPMenu);
						//MenuHelper.GenerateChildMenus(menuItem, dtMenu);
						//var tempNavURL = "";
						menuItem.NavigateUrl = objMenu.NavigateURL;
						var tempNavURL = "";
						if (menuItem.ChildItems.Count >= 1)
						{
							if (Convert.ToString(menuItem.NavigateUrl) == "#")
							{
							    tempNavURL = Convert.ToString(menuItem.ChildItems[0].NavigateUrl);
							    menuItem.NavigateUrl = tempNavURL;
							}

						}
						// Modified code ends here

						var lnk1 = new LinkButton();
						lnk1.ID = "Link" + i;

						if (menuItem.Value == ActiveMenuId.ToString())
						{
							lnk1.Style.Add("text-decoration", "none");
							lnk1.Style.Add("font-weight", "bold");
							lnk1.Style.Add("font-style", "italic");
							lnk1.PostBackUrl = String.Empty;
						}
						else
						{
							lnk1.Style.Add("text-decoration", "none");
							lnk1.Style.Add("font-weight", "normal");
							lnk1.PostBackUrl = menuItem.NavigateUrl;
						}
						lnk1.Text = menuItem.Text + "<br/>";

						SubMenuBackgroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBackgroundColor);
						SubMenuForegroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuForegroundColor);
						SubMenuHoverColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuHoverColor);
						SubMenuBorderStyle = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderStyle);
						SubMenuBorderColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderColor);
						SubMenuFontFamily = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontFamily);
						SubMenuFontSize = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontSize);


						if (SubMenuBackgroundColor == String.Empty)
							lnk1.Style.Add("background-color", "#465c71");
						else
							lnk1.Style.Add("background-color", SubMenuBackgroundColor);

						if (SubMenuForegroundColor == String.Empty)
							lnk1.Style.Add("color", "#dde4ec");
						else
							lnk1.Style.Add("color", SubMenuForegroundColor);

						if (SubMenuHoverColor == String.Empty)
							lnk1.Attributes.Add("onmouseover", "this.style.backgroundColor='#bfcbd6';");
						else
							lnk1.Attributes.Add("onmouseover", "this.style.backgroundColor='" + SubMenuHoverColor + "'");

						if (SubMenuBackgroundColor == String.Empty)
							lnk1.Attributes.Add("onmouseout", "this.style.backgroundColor='#465c71';");
						else
							lnk1.Attributes.Add("onmouseout", "this.style.backgroundColor='" + SubMenuBackgroundColor + "'");

						if (SubMenuBorderStyle == "Round")
						{
							divSubMenu.Attributes.Add("style", "border:2px solid; border-bottom-left-radius:10px !important;border-bottom-right-radius:10px !important;padding:10px 10px;");
							divSubMenu.Style.Add("border-color", SubMenuBorderColor);
							divSubMenu.Style.Add("background-color", SubMenuBackgroundColor);
						}

						if (SubMenuBorderStyle == "Box")
						{
							divSubMenu.Attributes.Add("style", "border:2px solid;");
							divSubMenu.Style.Add("border-color", SubMenuBorderColor);
						}
						if (SubMenuBorderStyle == "None")
							lnk1.Style.Add("border", "1px #4e667d none");

						lnk1.Style.Add("display", "block");
                        lnk1.Style.Add("text-align", "left");
						lnk1.Style.Add("line-height", "1.35em");
						lnk1.Style.Add("padding", "4px 20px");
						lnk1.Style.Add("white-space", "nowrap");

						if (SubMenuFontSize == String.Empty)
							lnk1.Style.Add("font-size", "9");
						else
							lnk1.Style.Add("font-size", SubMenuFontSize);

						if (SubMenuFontFamily == String.Empty)
							lnk1.Style.Add("font-family", "Helvetica, Verdana, sans-serif");
						else
							lnk1.Style.Add("font-family", SubMenuFontFamily);

						i++;
						plcSubMenuList.Controls.Add(lnk1);
					}	
					
				}
			}
		}

		public static void GenerateMenuCategoryChildSubMenus(MenuItem menuItem, List<MenuDataModel> listUPMenu)
		{
            if (listUPMenu != null && listUPMenu.Count > 0)
			{
                var listMenuResult = listUPMenu
							.Where(t => t.ParentMenuId != null && t.ParentMenuId == int.Parse(menuItem.Value))
							.Select(t => t)
							.OrderBy(t => t.SortOrder);


                foreach (var objMenu in listMenuResult)
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
					childMenuItem.Value = objMenu.MenuId.ToString();
					childMenuItem.NavigateUrl = objMenu.NavigateURL;

					var tempNavURL = "";

					GenerateMenuCategoryChildSubMenus(childMenuItem, listUPMenu);

					
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

        public void GenerateMenu(bool showMenuId)
        {
			var i = 0;

			plcSubMenuList.Controls.Clear();

            var listSiteMenu = SessionVariables.SiteMenuData;

            if (listSiteMenu != null && listSiteMenu.Count > 0)
            {
                var filter = "[" + MenuDataModel.DataColumns.ParentMenuId + "] =" + ParentMenuId;
                var listMenuResult = listSiteMenu.Where(x => x.ParentMenuId != null && x.ParentMenuId.Value == ParentMenuId).ToList();

                if (listMenuResult.Count > 0)
                {
					TopBackgroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBackgroundColor);
					SubMenuTopBorderColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBorderColor);

					divTopBar.Attributes.Add("style", "border:2px solid;border-bottom-style:hidden;");
					divTopBar.Style.Add("border-color", SubMenuTopBorderColor);
					
					divTopBar.Style.Add("background-color", TopBackgroundColor);					

					hypSettings.Style.Add("visibility", "hidden");
					lnkClose.Style.Add("visibility", "hidden");

					divTopBar.Attributes.Add("onmouseover", "document.getElementById('" + hypSettings.ClientID + "').style.visibility='visible';document.getElementById('" + lnkClose.ClientID + "').style.visibility='visible';");
					divTopBar.Attributes.Add("onmouseout", "document.getElementById('" + hypSettings.ClientID + "').style.visibility='hidden';document.getElementById('" + lnkClose.ClientID + "').style.visibility='hidden';");

                    foreach (var objMenu in listMenuResult)
                    {
                        var menuText = objMenu.MenuDisplayName;

                        if (showMenuId)
                        {
                            menuText += " (" + Convert.ToString(objMenu.MenuId) + ") ";
                        }

                        if (SessionVariables.IsTesting && !string.IsNullOrEmpty(objMenu.PrimaryDeveloper))
                        {
                            menuText += " (" + objMenu.PrimaryDeveloper + ")";
                        }

                        var menuItem = new MenuItem(menuText);
                        menuItem.Value = Convert.ToString(objMenu.MenuId);
                        menuItem.NavigateUrl = objMenu.NavigateURL;

                        MenuHelper.GenerateChildMenus(menuItem);


                        var lnk1 = new LinkButton();
                        lnk1.ID = "Link" + i;

                        if (menuItem.Value == ActiveMenuId.ToString())
                        {
                            lnk1.Style.Add("text-decoration", "none");
                            lnk1.Style.Add("font-weight", "bold");
                            lnk1.Style.Add("font-style", "italic");
                            lnk1.PostBackUrl = String.Empty;
                        }
                        else
                        {
                            lnk1.Style.Add("text-decoration", "none");
                            lnk1.Style.Add("font-weight", "normal");
							lnk1.PostBackUrl = "#";
                            //lnk1.PostBackUrl = menuItem.NavigateUrl;
                        }
                        lnk1.Text = menuItem.Text + "<br/>";
						SubMenuBackgroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBackgroundColor);
						SubMenuForegroundColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuForegroundColor);
						SubMenuHoverColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuHoverColor);
						SubMenuBorderStyle = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderStyle);
						SubMenuBorderColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuBorderColor);
						SubMenuFontFamily = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontFamily);
						SubMenuFontSize = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuFontSize);
						//TopBackgroundColor = ApplicationCommon.GetApplicationUserPreferenceByKey(ApplicationCommon.SubMenuTopBackgroundColor);
												

						//if (TopBackgroundColor == String.Empty)
						//    divTopBar.Style.Add("background-color", "#465c71");
						//else
						//    divTopBar.Style.Add("background-color", TopBackgroundColor);
																	
						
						if (SubMenuBackgroundColor == String.Empty)
							lnk1.Style.Add("background-color", "#465c71");
						else
							lnk1.Style.Add("background-color", SubMenuBackgroundColor);

						if (SubMenuForegroundColor == String.Empty)
							lnk1.Style.Add("color", "#dde4ec");
						else
							lnk1.Style.Add("color", SubMenuForegroundColor);

						if (SubMenuHoverColor == String.Empty)
							lnk1.Attributes.Add("onmouseover", "this.style.backgroundColor='#bfcbd6';");
						else
							lnk1.Attributes.Add("onmouseover", "this.style.backgroundColor='" + SubMenuHoverColor + "'");

						if (SubMenuBackgroundColor == String.Empty)
							lnk1.Attributes.Add("onmouseout", "this.style.backgroundColor='#465c71';");
						else
							lnk1.Attributes.Add("onmouseout", "this.style.backgroundColor='" + SubMenuBackgroundColor + "'");

						if (SubMenuBorderStyle == "Round")
						{
							divSubMenu.Attributes.Add("style", "border:2px solid; border-bottom-left-radius:10px !important;border-bottom-right-radius:10px !important;padding:10px 10px;");
							divSubMenu.Style.Add("border-color", SubMenuBorderColor);
							divSubMenu.Style.Add("background-color", SubMenuBackgroundColor);
						}

						if (SubMenuBorderStyle == "Box")
						{
							//lnk1.Style.Add("border", "1px #4e667d solid");
							divSubMenu.Attributes.Add("style", "border:2px solid;");
							divSubMenu.Style.Add("border-color", SubMenuBorderColor);
						}
						if (SubMenuBorderStyle == "None")
						{
							divSubMenu.Attributes.Add("style", "border:none;");
							lnk1.Style.Add("border", "1px #4e667d none");
						}
                        lnk1.Style.Add("display", "block");
                        lnk1.Style.Add("line-height", "1.35em");
                        lnk1.Style.Add("padding", "4px 20px");
                        lnk1.Style.Add("white-space", "nowrap");

						if(SubMenuFontSize==String.Empty)
							lnk1.Style.Add("font-size", "9");
						else
							lnk1.Style.Add("font-size", SubMenuFontSize);

						if(SubMenuFontFamily==String.Empty)
							lnk1.Style.Add("font-family", "Helvetica, Verdana, sans-serif");
						else
							lnk1.Style.Add("font-family", SubMenuFontFamily);

                        i++;
                        plcSubMenuList.Controls.Add(lnk1);

                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
            Visible = false;
            //this.Hidden = true;

            PreferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.ControlVisible, "false");
            
        }

    }
}