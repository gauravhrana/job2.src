﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.TasksAndWorkflow.TaskRun
{

	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRun;
			PrimaryEntityKey = "TaskRun";
			DetailsControlPath = ApplicationCommon.GetControlPath("TaskRun", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}


		#endregion

	}
}
