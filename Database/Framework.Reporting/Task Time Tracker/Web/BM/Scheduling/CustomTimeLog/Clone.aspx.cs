using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog
{
	public partial class Clone : PageClone
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.CustomTimeLog;
			PrimaryEntityKey = "CustomTimeLog";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("Schedule", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}

		#endregion
	}
}