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

namespace ApplicationContainer.UI.Web
{
    public partial class BootStrapDefault : PageDefaultMaster
    {

        #region Methods

        public string GetItemCSSStyle(object menuItemValue)
        {
            return MenuHelper.GetMenuItemStyle(menuItemValue);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionVariables.UserAuthorized)
            {
                Response.Redirect("~/UnauthorizedAccess.htm", true);
            }

            lblPerson.Text = "User: " + ApplicationCommon.GetApplicationUserName() + " (" + SessionVariables.RequestProfile.AuditId + ")";
			var applicationInfo = ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId];
			lblProjectTitle.InnerText = applicationInfo.Description;

            if (!IsPostBack)
            {
                lblStatus.Text = "[I]";
                lblStatus.ToolTip = "ApplicationMode = " + oSliderMenu.ApplicationMode + " \n" + " MenuCategory = " + oSliderMenu.MenuCategory;
            }

            MenuHelper.GenerateUserPreferenceMenu(NavigationMenu);

            // TODO: review business case for logging 
            if (!IsPostBack)
            {
                AddUserLoginHistoryRecord();
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