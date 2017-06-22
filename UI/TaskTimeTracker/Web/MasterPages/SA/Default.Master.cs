using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.UI.Web.Controls.BreadCrumb;
using Shared.UI.Web.Controls.SubMenu;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web;
using ASPMenu = System.Web.UI.WebControls.Menu;
using TTTMenu = Framework.Components.Core.MenuDataManager;
using Framework.UI.Web.BaseClasses;
//using ApplicationContainer.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.MasterPages.SA
{
	public partial class DefaultMasterPage : PageDefaultMaster
	{
        public const int applicationId = 100065;

		public int ApplicationId
		{
			get
			{
				return applicationId;
			}
		}

		public override string TableName
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

		public override SubMenu SubMenuObject
		{
			get
			{
				//return oSubMenu;
				return oSliderMenu.SubMenuObject;
			}
			set
			{
				//oSubMenu = value;
				oSliderMenu.SubMenuObject = value;
			}
		}

		public override BreadCrumb BreadCrumbObject
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


		#region Methods

		public string GetItemCSSStyle(object menuItemValue)
		{
			return MenuHelper.GetMenuItemStyle(menuItemValue);
		}

		public override void Setup(string tableName)
		{
			TableName = tableName;
			oSliderMenu.Setup(TableName);
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			//BreadCrumbObject = oBreadCrumb;
			//SubMenuObject = oSubMenu;
			//MainMenu = NavigationMenu;

			if (!SessionVariables.UserAuthorized)
			{
				Response.Redirect("~/UnauthorizedAccess.htm", true);
            }

            Page.Header.DataBind();

            StartupCheck();

			lblPerson.Text = "User: " + ApplicationCommon.GetApplicationUserName() + " (" + SessionVariables.RequestProfile.AuditId + ")";

			if (!IsPostBack)
			{
				lblStatus.Text = "[I]";
				lblStatus.ToolTip = "ApplicationMode = " + oSliderMenu.ApplicationMode + " \n" + " MenuCategory = " + oSliderMenu.MenuCategory;
				var applicationInfo = ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId];
				lblProjectTitle.InnerText = applicationInfo.Description;
			}

			// TODO: review business case for logging 
			if (!IsPostBack)
			{
                AddUserLoginHistoryRecord();
			}

			// QuickSearchControl
			QuickSearchControlId.OnSearch += QuickSearch_buttonClick;
			QuickSearchControlId.SetUp();

			ScriptManager.RegisterStartupScript(Page, GetType(), "dyncss", "showhideborder(" + SessionVariables.IsTesting.ToString().ToLower() + ");", true);

			// testing ... will show later.
			if (false)
			{
				MenuNavPannelOptions.Visible = false;
				oSliderMenu.Visible = false;
			}

		}

		// should be in quick search control ... 
		protected void QuickSearch_buttonClick(object sender, EventArgs e)
		{
			var searchTextBox = QuickSearchControlId.FindControl("txtSearchName") as TextBox;

			// should be true ...
			if (searchTextBox != null)
			{
				var strEntityName = searchTextBox.Text;

				if (strEntityName != string.Empty)
					Response.Redirect("~/QuickSearchList.aspx?SN=" + strEntityName);
			}
		}


		#endregion

	}
}