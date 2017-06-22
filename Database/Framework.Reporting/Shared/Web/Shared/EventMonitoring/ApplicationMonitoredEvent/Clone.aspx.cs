using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationMonitoredEvent
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEvent;
			PrimaryEntityKey = "ApplicationMonitoredEvent";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("ApplicationMonitoredEvent", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}

		#endregion

    }
}