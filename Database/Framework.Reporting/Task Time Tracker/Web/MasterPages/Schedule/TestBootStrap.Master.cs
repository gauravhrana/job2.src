using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.UI.Web;
using Shared.UI.Web.Controls.BreadCrumb;
using Shared.WebCommon.UI.Web;

namespace TaskTimeTracker.UI.Web.MasterPages.Schedule
{
	public partial class TestBootStrap : MasterPage
	{
		
		public const int applicationId = 100047;

		public int ApplicationId
		{
			get
			{
				return applicationId;
			}
		}

		#region Properties

		public BreadCrumb BreadCrumbObject
		{
			get
			{
				return oBreadCrumb;
			}
		}

		#endregion

		#region Methods

		public string GetItemCSSStyle(object menuItemValue)
		{
			return MenuHelper.GetMenuItemStyle(menuItemValue);
		}

		#endregion

		#region events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!SessionVariables.UserAuthorized)
			{
				Response.Redirect("~/UnauthorizedAccess.htm", true);
			}

			var isTesting = SessionVariables.IsTesting;
			lblPerson.Text = "Logged in User: " + ApplicationCommon.GetApplicationUserName() + " (" + SessionVariables.AuditId + ")";

			if (!IsPostBack)
			{
				lblStatus.Text = "[I]";
				lblStatus.ToolTip = "ApplicationMode = " + oSliderMenu.ApplicationMode + " \n" + " MenuCategory = " + oSliderMenu.MenuCategory;

			}

			MenuHelper.GenerateUserPreferenceMenu(NavigationMenu);
			var strURL = String.Empty;
			if (ApplicationCommon.GetSetId() != 0)
			{
				strURL = Page.AppRelativeVirtualPath.Substring(0, Page.AppRelativeVirtualPath.LastIndexOf(".")) + "/" + ApplicationCommon.GetSetId();
			}
			else
			{
				strURL = Page.AppRelativeVirtualPath;
			}

			var data = new UserLoginHistoryDataModel();

			data.UserId = SessionVariables.AuditId;
			data.UserName = ApplicationCommon.GetApplicationUserName();
			data.URL = strURL;
			data.ServerName = Environment.MachineName;

			UserLoginHistoryDataManager.Create(data, SessionVariables.AuditId);

			QuickSearchControlId.OnSearch += new EventHandler(QuickSearch_buttonClick);
			QuickSearchControlId.SetUp();
		}

		protected void QuickSearch_buttonClick(object sender, EventArgs e)
		{
			var SearchTextBox = QuickSearchControlId.FindControl("txtSearchName") as TextBox;
			var strEntityName = SearchTextBox.Text;
			if (strEntityName != "")
				Response.Redirect("~/QuickSearchList.aspx?SN=" + strEntityName);

		}

		protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
		{
		}

		#endregion
	}
}