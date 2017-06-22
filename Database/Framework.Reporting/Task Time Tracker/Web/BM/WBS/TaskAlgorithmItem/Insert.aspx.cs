﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem
{
	public partial class Insert : PageInsert
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.TaskAlgorithmItem;
			PrimaryEntityKey = "TaskAlgorithmItem";
			PrimaryGenericControl = myGenericControl;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}