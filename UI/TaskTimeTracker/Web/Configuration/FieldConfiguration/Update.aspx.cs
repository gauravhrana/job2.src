using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfiguration
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region Methods

        protected override Control GetTabControl(int setId, Control updateControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("FieldConfigurationUpdateView");

            var selected = false;

            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
            {
                selected = true;
            }

            tabControl.AddTab("FieldConfiguration", updateControl, String.Empty, selected);

            // not making sense ?
            selected = false;

            if (Request.QueryString["tab"] == "2")
            {
                selected = true;
            }

            var fieldConfigurationDisplayNameControlPath = "~/Configuration/FieldConfiguration/Controls/FieldConfigurationDisplayName.ascx";

            var fieldConfigurationDisplayNameControl = (Shared.UI.Web.Configuration.FieldConfiguration.Controls.FieldConfigurationDisplayName)Page.LoadControl(fieldConfigurationDisplayNameControlPath);
            fieldConfigurationDisplayNameControl.Setup(setId);
            tabControl.AddTab("FieldConfigurationDisplayName", fieldConfigurationDisplayNameControl, "Field Configuration Display Name");

            return tabControl;
        }
        
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfiguration;

			GenericControlPath = ApplicationCommon.GetControlPath("FieldConfiguration", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "FieldConfiguration";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;

        }

        #endregion

    }
}