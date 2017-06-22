using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
	public partial class SliderMenuControl : UserControl
	{

		#region Properties
		public Shared.UI.Web.Controls.SubMenu.SubMenu SubMenuObject
		{
			get
			{
				return oSubMenu;
			}
			set
			{
				oSubMenu = value;
			}
		}

		public string TableName
		{
			get
			{
				if (ViewState["TableName"] == null)
				{
					ViewState["TableName"] = String.Empty;
				}

				return Convert.ToString(ViewState["TableName"]);
			}
			set
			{
				ViewState["TableName"] = value;
			}
		}

		public string MenuCategory
		{
			get
			{
				if (drpMenuCategory2.SelectedItem != null)
				{
					return drpMenuCategory2.SelectedItem.Text;
				}
				return String.Empty;
			}
		}

		public string ApplicationMode
		{
			get
			{
				if (DeveloperPanel.drpApplicationMode.SelectedItem != null)
				{
					return DeveloperPanel.drpApplicationMode.SelectedItem.Text;
				}
				return String.Empty;
			}
		}




		#endregion

		#region Methods


		public HtmlGenericControl GenerateAccordinHTML()
		{

			var xUl = new HtmlGenericControl("ul");

			var lstMenus = new List<string>();
			lstMenus.Add("DashBoard");
			lstMenus.Add("Tasks");
			lstMenus.Add("Calendar");

			var xLiFirst = new HtmlGenericControl("li");

			var xH3First = new HtmlGenericControl("h3");
			var subSpanFirst = new HtmlGenericControl("span");
			subSpanFirst.Attributes.Add("class", "icon-anchor");
			xH3First.Controls.Add(subSpanFirst);

			var subSpanSecond = new HtmlGenericControl("span");
			subSpanSecond.InnerText = "Recent URLs";
			xH3First.Controls.Add(subSpanSecond);

			xLiFirst.Controls.Add(xH3First);
			var subUlFirst = new HtmlGenericControl("ul");

			var currentPage = HttpContext.Current.CurrentHandler as Page;

			

			xLiFirst.Controls.Add(subUlFirst);
			xUl.Controls.Add(xLiFirst);

			foreach (var strLiHeader in lstMenus)
			{
				var xLi = new HtmlGenericControl("li");

				var xH3 = new HtmlGenericControl("h3");
				var subSpan = new HtmlGenericControl("span");
				subSpan.Attributes.Add("class", "icon-" + strLiHeader.ToLower());
				xH3.Controls.Add(subSpan);

				var subSpan2 = new HtmlGenericControl("span");
				subSpan2.InnerText = strLiHeader;
				xH3.Controls.Add(subSpan2);

				xLi.Controls.Add(xH3);

				var subUl = new HtmlGenericControl("ul");

				var subLi1 = new HtmlGenericControl("li");
				var subAnchor1 = new HtmlGenericControl("a");
				subAnchor1.Attributes.Add("href", "#");
				subAnchor1.InnerText = "Anchor 1";

				subLi1.Controls.Add(subAnchor1);
				subUl.Controls.Add(subLi1);

				var subLi2 = new HtmlGenericControl("li");
				var subAnchor2 = new HtmlGenericControl("a");
				subAnchor2.Attributes.Add("href", "#");
				subAnchor2.InnerText = "Anchor 2";

				subLi2.Controls.Add(subAnchor2);
				subUl.Controls.Add(subLi2);

				var subLi3 = new HtmlGenericControl("li");
				var subAnchor3 = new HtmlGenericControl("a");
				subAnchor3.Attributes.Add("href", "#");
				subAnchor3.InnerText = "Anchor 3";

				subLi3.Controls.Add(subAnchor3);
				subUl.Controls.Add(subLi3);

				xLi.Controls.Add(subUl);
				xUl.Controls.Add(xLi);
			}

			return xUl;

		}

		public void lnkEmailSession_Click(object sender, EventArgs e)
		{
			var temp = 0;
			InformationCommon.SendSessionInfo(InformationCommon.EmailType.Information, InformationCommon.UniqueValue);
		}


		public void lnkEmailApplication_Click(object sender, EventArgs e)
		{
			InformationCommon.SendApplicationInfo(InformationCommon.EmailType.Information, InformationCommon.UniqueValue);
		}

		public void lnkEmailSQL_Trace(object sender, EventArgs e)
		{
			InformationCommon.SendSQLTraceInfo(InformationCommon.UniqueValue);
		}

		public void Setup(string tableName)
		{
			TableName = tableName;
		}

		private void bindMenuCateogry()
		{
			var dt = SessionVariables.MenuCategoryList;

			UIHelper.LoadDropDown(dt, drpMenuCategory2, StandardDataModel.StandardDataColumns.Name,
				MenuCategoryDataModel.DataColumns.MenuCategoryId);
		}

		

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			bindMenuCateogry();
			DeveloperPanel.drpApplicationMode.SelectedValue = Convert.ToString(SessionVariables.UserApplicationMode);
			drpMenuCategory2.SelectedValue = Convert.ToString(SessionVariables.UserMenuCategory);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ServerAccordin.Controls.Add(GenerateAccordinHTML());
			helpimg.ImageAlign = ImageAlign.Middle;
			var strUserTheme = PerferenceUtility.GetUserTheme();
			
		}

		
		protected void drpMenuCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			var menuCategory = ((DropDownList)sender).SelectedValue;
			SessionVariables.UserMenuCategory = menuCategory;
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserMenuCategory, menuCategory);
			SessionVariables.UserPreferedMenuData = MenuHelper.GetUserPreferedMenu();
			Response.Redirect("~/Default.aspx", false);
			
		}

		protected void lnkHelp_Click(object sender, EventArgs e)
		{
			var data = new HelpPageDataModel();
			data.SystemEntityTypeId = ApplicationCommon.GetSystemEntityTypeId(TableName);
			data.HelpPageContextId = ApplicationCommon.GetHelpPageContextId("Default");

			var dt = Framework.Components.Core.HelpPageDataManager.Search(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count > 0)
			{
				var helpPageId = Convert.ToInt32(dt.Rows[0][HelpPageDataModel.DataColumns.HelpPageId]);
				Response.Redirect(Page.GetRouteUrl("HelpPageEntityRoute", new { Action = "Details", SetId = helpPageId }), false);
			}
			else
			{
				data.Name = "Default";
				data.Content = "Help Page Content";
				data.SortOrder = 1;
				var helpPageId = Framework.Components.Core.HelpPageDataManager.Create(data, SessionVariables.RequestProfile);
				Response.Redirect(Page.GetRouteUrl("HelpPageEntityRoute", new { Action = "Update", SetId = helpPageId }), false);
				
			}
		}

		#endregion

	}
}