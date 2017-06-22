using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.UI.Web;
using Shared.UI.Web.Controls.BreadCrumb;
using Shared.UI.Web.Controls.SubMenu;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.UI.Web.BaseClasses;

namespace TaskTimeTracker.UI.Web.MasterPages.Schedule
{
	public partial class BootStrapDefault : PageDefaultMaster
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

		public Menu MainMenu
		{
			get
			{

				return NavigationMenu;
			}
		}

		public SubMenu SubMenuObject
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

		public BreadCrumb BreadCrumbObject
		{
			get
			{
				return oBreadCrumb;
			}
			set
			{
				oBreadCrumb = value;
			}
		}

		#endregion

		#region Methods

		public string GetItemCSSStyle(object menuItemValue)
		{
			return MenuHelper.GetMenuItemStyle(menuItemValue);
		}

		public void Setup(string tableName)
		{
			TableName = tableName;
			oSliderMenu.Setup(TableName);
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!SessionVariables.UserAuthorized)
			{
				Response.Redirect("~/UnauthorizedAccess.htm", true);
			}

			lblPerson.Text = "User: " + ApplicationCommon.GetApplicationUserName() + " (" + SessionVariables.AuditId + ")";

			if (!IsPostBack)
			{
				lblStatus.Text = "[I]";
				lblStatus.ToolTip = "ApplicationMode = " + oSliderMenu.ApplicationMode + " \n" + " MenuCategory = " + oSliderMenu.MenuCategory;
			}

			MenuHelper.GenerateUserPreferenceMenu(NavigationMenu);

			// TODO: review business case for logging 
			if (!IsPostBack)
			{
				var data = new UserLoginHistoryDataModel();

				data.UserId = SessionVariables.AuditId;
				data.UserName = ApplicationCommon.GetApplicationUserName();
				data.URL = Page.AppRelativeVirtualPath;
				data.ServerName = Environment.MachineName;

				UserLoginHistoryDataManager.Create(data, SessionVariables.AuditId);
			}

			// QuickSearchControl
			QuickSearchControlId.OnSearch += QuickSearch_buttonClick;
			QuickSearchControlId.SetUp();

			ScriptManager.RegisterStartupScript(Page, GetType(), "dyncss", "showhideborder(" + SessionVariables.IsTesting.ToString().ToLower() + ");", true);
		}

		// should be in quick search control ... 
		protected void QuickSearch_buttonClick(object sender, EventArgs e)
		{
			var SearchTextBox = QuickSearchControlId.FindControl("txtSearchName") as TextBox;

			// should be true ...
			if (SearchTextBox != null)
			{
				var strEntityName = SearchTextBox.Text;

				if (strEntityName != string.Empty)
					Response.Redirect("~/QuickSearchList.aspx?SN=" + strEntityName);
			}
		}

		#endregion

	}
}