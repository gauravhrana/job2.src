using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ClientXProject
{
	public partial class Update : PageUpdate
	{

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.ClientXProject;

			GenericControlPath	= ApplicationCommon.GetControlPath("ClientXProject", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "ClientXProject";
			BreadCrumbObject	= Master.BreadCrumbObject;

			BtnUpdate			= btnUpdate;
			BtnClone			= btnClone;
			BtnCancel			= btnCancel;

		}

	}
}