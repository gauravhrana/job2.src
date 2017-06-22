﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Activity
{
    public partial class Insert : PageInsert
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= SystemEntity.Activity;
            PrimaryEntityKey		= "Activity";
            PrimaryGenericControl	= myGenericControl;
            BreadCrumbObject		= Master.BreadCrumbObject;
        }

        #endregion
    }
}