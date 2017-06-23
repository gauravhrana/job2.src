﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SearchKey
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.SearchKey;
			
			GenericControlPath	= ApplicationCommon.GetControlPath("SearchKey", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "SearchKey";
			BreadCrumbObject	= Master.BreadCrumbObject;

            BtnUpdate			= btnUpdate;
            BtnClone			= btnClone;
            BtnCancel			= btnCancel;
		}
		
        #endregion
	}
}