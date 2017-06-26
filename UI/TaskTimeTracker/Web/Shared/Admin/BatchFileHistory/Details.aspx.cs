﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.BatchFileHistory
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= SystemEntity.BatchFileHistory;
			PrimaryEntityKey	= "BatchFileHistory";
			DetailsControlPath	= ApplicationCommon.GetControlPath("BatchFileHistory", ControlType.DetailsControl);
			PrimaryPlaceHolder	= plcDetailsList;
			BreadCrumbObject	= Master.BreadCrumbObject;
		}

		#endregion

    }
}