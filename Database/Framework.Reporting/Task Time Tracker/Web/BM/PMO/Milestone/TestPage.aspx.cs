using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using ApplicationContainer.UI.Web.CommonCode;
using System.Web.UI.HtmlControls;


namespace ApplicationContainer.UI.Web.Milestone
{
    public partial class TestPage : BasePage
    {
        private void GenerateMenu(int parentId)
        {
            
            //DataTable dtMenu = null;
            //int i=0;
            
            //if (SessionVariables.SiteMenuData == null)
            //{
            //    SessionVariables.SiteMenuData = null;
            //}

            //dtMenu = SessionVariables.SiteMenuData;

            //if (dtMenu != null && dtMenu.Rows.Count > 0)
            //{                
            //    var drs = dtMenu.Select("[" + MenuDataModel.DataColumns.ParentMenuId + "] ="+parentId);
            //    if (drs.Length > 0)
            //    {
            //        foreach (DataRow dr in drs)
            //        {
            //            var menuText = Convert.ToString(dr[MenuDataModel.DataColumns.MenuDisplayName]);
            //            if (SessionVariables.IsTesting && !string.IsNullOrEmpty(Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper])))
            //            {
            //                menuText += " (" + Convert.ToString(dr[MenuDataModel.DataColumns.PrimaryDeveloper]) + ")";
            //            }
            //            var menuItem = new MenuItem(menuText);
            //            menuItem.Value = Convert.ToString(dr[MenuDataModel.DataColumns.MenuId]);
            //            menuItem.NavigateUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]);
            //            GenerateChildMenus(menuItem, dtMenu);                                               
            //            LinkButton lnk1 = new LinkButton();
            //            lnk1.ID="Link"+i;
            //            lnk1.Text = menuItem.Text.ToString() + "<br/>";

            //            lnk1.Style.Add("background-color", "#465c71");
            //            lnk1.Style.Add("color", "#dde4ec");
            //            lnk1.Style.Add("border", "1px #4e667d solid");
            //            lnk1.Style.Add("display", "block");
            //            lnk1.Style.Add("line-height", "1.35em");
            //            lnk1.Style.Add("padding", "4px 20px");
            //            lnk1.Style.Add("text-decoration", "none");
            //            lnk1.Style.Add("white-space", "nowrap");
            //            lnk1.Style.Add("font-size", "9");
            //            lnk1.Style.Add("font-family", "Helvetica, Verdana, sans-serif");
            //            lnk1.Style.Add("font-weight", "normal");
            //            lnk1.PostBackUrl = menuItem.NavigateUrl;
            //            i++;
            //            plcSubMenuList.Controls.Add(lnk1);
                                               
            //        }

            //    }
            //}
        }

        private void GenerateChildMenus(MenuItem menuItem, DataTable dtMenu)
        {
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

                    GenerateChildMenus(childMenuItem, dtMenu);

                    if (childMenuItem.ChildItems.Count >= 1)
                    {
                        menuText += ApplicationCommon.DynamicMenuCharacter;
                        childMenuItem.Text = menuText;

                    }
                    CheckMenuCategory(childMenuItem);
                    menuItem.ChildItems.Add(childMenuItem);
                }
            }
        }

        private void CheckMenuCategory(MenuItem childMenuItem)
        {
            return;

            var testing = "Testing";
            var dt = MenuCategoryXMenuDataManager.GetByMenu(Convert.ToInt32(childMenuItem.Value), SessionVariables.RequestProfile);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (SessionVariables.IsTesting && (Convert.ToString(dr[MenuCategoryXMenuDataModel.DataColumns.MenuCategory]) == testing))
                    {
                        childMenuItem.Text = "<div style='color:Yellow'>" + childMenuItem.Text + "</div>";
                        break;
                    }

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            GenerateMenu(23);
        }
    }
}