﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;

			GenericControlPath = ApplicationCommon.GetControlPath("FieldConfigurationModeXApplicationRole", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey   = "FieldConfigurationModeXApplicationRole";
			BreadCrumbObject   = Master.BreadCrumbObject;

			BtnUpdate          = btnUpdate;
			BtnClone           = btnClone;
			BtnCancel          = btnCancel;
		}

		#endregion

    }
}