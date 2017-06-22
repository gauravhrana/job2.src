using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.Core;
using System.Data;
using ASPMenu = System.Web.UI.WebControls.Menu;
using TTTMenu = Framework.Components.Core.MenuDataManager;

namespace Shared.UI.Web.MenuDemo
{
	public partial class Test : Shared.UI.WebFramework.BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
				ASPMenu menu = (ASPMenu)this.Master.FindControl("NavigationMenu");
				//menu.Visible = false;
				PopulateMenu();
		
		}
		private void PopulateMenu()
		{

            /*
			var dtMenu = TTTMenu.GetList(AuditId);
			var dtSubMenu = SubMenu.GetList(AuditId);
			var dtChildMenu = ChildMenu.GetList(AuditId);
			var dtChildSubMenu = ChildSubMenu.GetList(AuditId);
			var mainmenu = new MenuItem();
			var submenu = new MenuItem();
			var childmenu = new MenuItem();
			var childsubmenu = new MenuItem();

			for (var i = 0; i < dtMenu.Rows.Count; i++ )
			{
				mainmenu = new MenuItem(dtMenu.Rows[i][Menu.DataColumns.Name].ToString());
				mainmenu.NavigateUrl = dtMenu.Rows[i][Menu.DataColumns.NavigateURL].ToString();
				NavigationMenu.Items.Add(mainmenu);
				for(int j = 0; j < dtSubMenu.Rows.Count; j++)
				{
					if(dtSubMenu.Rows[j][Framework.Components.Core.SubMenu.DataColumns.MenuId].ToString().Equals
						(dtMenu.Rows[i][Menu.DataColumns.MenuId].ToString()))

					{
						submenu = new MenuItem(dtSubMenu.Rows[j][Framework.Components.Core.SubMenu.DataColumns.Name].ToString());
						submenu.NavigateUrl = dtSubMenu.Rows[j][Framework.Components.Core.SubMenu.DataColumns.NavigateURL].ToString();
						NavigationMenu.Items[i].ChildItems.Add(submenu);
						for (int k = 0; k < dtChildMenu.Rows.Count; k++)
						{
							if (dtChildMenu.Rows[k][Framework.Components.Core.ChildMenu.DataColumns.SubMenuId].ToString().Equals
								(dtSubMenu.Rows[j][Framework.Components.Core.SubMenu.DataColumns.SubMenuId].ToString()))
							{
								childmenu = new MenuItem(dtChildMenu.Rows[k][Framework.Components.Core.ChildMenu.DataColumns.Name].ToString());
								childmenu.NavigateUrl = dtChildMenu.Rows[k][Framework.Components.Core.ChildMenu.DataColumns.NavigateURL].ToString();
								NavigationMenu.Items[i].ChildItems[NavigationMenu.Items[i].ChildItems.Count - 1].ChildItems.Add(childmenu);
								
								
								for (int l = 0; l < dtChildSubMenu.Rows.Count; l++)
								{
																						
									if (dtChildSubMenu.Rows[l][Framework.Components.Core.ChildSubMenu.DataColumns.ChildMenuId].ToString().Equals
										(dtChildMenu.Rows[k][Framework.Components.Core.ChildMenu.DataColumns.ChildMenuId].ToString()))
									{
										childsubmenu = new MenuItem(dtChildSubMenu.Rows[l][Framework.Components.Core.ChildSubMenu.DataColumns.Name].ToString());
										childsubmenu.NavigateUrl = dtChildSubMenu.Rows[l][Framework.Components.Core.ChildSubMenu.DataColumns.NavigateURL].ToString();
										NavigationMenu.Items[i].ChildItems[NavigationMenu.Items[i].ChildItems.Count - 1].ChildItems[NavigationMenu.Items[i].ChildItems
											[NavigationMenu.Items[i].ChildItems.Count - 1].ChildItems.Count-1].ChildItems.Add(childsubmenu);
									}
									
								}
							} 
						}
					}
				}
			}
			*/

		}
	}
}