﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.ConnectionString
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.ConnectionString;

            GenericControlPath = ApplicationCommon.GetControlPath("ConnectionString", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey   = "ConnectionString";
            BreadCrumbObject   = Master.BreadCrumbObject;

            BtnUpdate          = btnUpdate;
            BtnClone           = btnClone;
            BtnCancel          = btnCancel;
        }

        
    }
}