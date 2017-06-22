using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web
{
	public partial class QuickSearchList : Shared.UI.WebFramework.BasePage
	{

		public void GenerateSearchList()
		{
			var strEntityName = Request.QueryString["SN"].ToString();
			var data = new MenuDataModel();
			data.Name = strEntityName;
			var i = 0;
			lblResult.Text = "";
			var searchList = Framework.Components.Core.MenuDataManager.Search(data, SessionVariables.RequestProfile);

			plcSearchList.Controls.Clear();			

			if (searchList.Rows.Count > 0)
			{
				foreach (DataRow dr in searchList.Rows)
				{
					var menuText = Convert.ToString(dr[StandardDataModel.StandardDataColumns.Name]);

					var menuItem = new MenuItem(menuText);
					menuItem.Value = Convert.ToString(dr[MenuDataModel.DataColumns.MenuId]);
					menuItem.NavigateUrl = Convert.ToString(dr[MenuDataModel.DataColumns.NavigateURL]);

					if (searchList.Rows.Count == 1)
					{
						Response.Redirect(menuItem.NavigateUrl);
						break;
					}

					if (searchList.Rows.Count == 2 && Convert.ToString(menuItem.NavigateUrl) != "#")
					{
						Response.Redirect(menuItem.NavigateUrl);
						break;
					}					

					if (Convert.ToString(menuItem.NavigateUrl) != "#")
					{
						var lnk1 = new LinkButton();
						lnk1.ID = "Link" + i;
						lnk1.Style.Add("text-decoration", "none");
						lnk1.Style.Add("font-weight", "normal");
						lnk1.PostBackUrl = menuItem.NavigateUrl;
						lnk1.Text =i+1 +". "+ menuItem.Text.ToString() + "<br/>";
						lnk1.Style.Add("display", "block");
						lnk1.Style.Add("line-height", "1.35em");
						lnk1.Style.Add("padding", "4px 20px");
						lnk1.Style.Add("white-space", "nowrap");
						lnk1.Style.Add("font-size", "9");
						lnk1.Style.Add("font-family", "Helvetica, Verdana, sans-serif");					

						i++;
						plcSearchList.Controls.Add(lnk1);
					}	
				}			
			}
			else
			lblResult.Text = "0 records";
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			GenerateSearchList();
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			SettingCategory = "QuickSearchListDefaultView";
			var sbm = this.Master.SubMenuObject;
			

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			//bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			//bcControl.Setup("Search List");
			//bcControl.GenerateMenu();

		}
	}
}