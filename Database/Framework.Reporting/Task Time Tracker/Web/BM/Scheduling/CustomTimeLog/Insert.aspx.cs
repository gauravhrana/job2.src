using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog
{
	public partial class Insert : PageInsert
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.CustomTimeLog;
			PrimaryEntityKey = "CustomTimeLog";
			PrimaryGenericControl = mygenericControl;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}