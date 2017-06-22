using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.Menu
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        protected override Control GetTabControl(int setId, Control updateControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("MenuUpdateView");

            var selected = false;

            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
            {
                selected = true;
            }

            tabControl.AddTab("Menu", updateControl, String.Empty, selected);

            // not making sense ?
            selected = false;

            if (Request.QueryString["tab"] == "2")
            {
                selected = true;
            }

            var menuDisplayNameControlPath = "~/Configuration/Menu/Controls/MenuDisplayName.ascx";

            var menuDisplayNameControl = (Shared.UI.Web.Configuration.Menu.Controls.MenuDisplayName)Page.LoadControl(menuDisplayNameControlPath);
            menuDisplayNameControl.Setup(setId);
            tabControl.AddTab("MenuDisplayName", menuDisplayNameControl, "Menu Display Name");

            return tabControl;
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.Menu;

            GenericControlPath = ApplicationCommon.GetControlPath("Menu", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey   = "Menu";
            BreadCrumbObject   = Master.BreadCrumbObject;

            BtnUpdate          = btnUpdate;
            BtnClone           = btnClone;
            BtnCancel          = btnCancel;
        }

    }
}