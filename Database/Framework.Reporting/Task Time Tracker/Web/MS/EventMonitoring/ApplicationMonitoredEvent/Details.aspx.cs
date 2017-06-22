using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationMonitoredEvent
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEvent;
			PrimaryEntityKey = "ApplicationMonitoredEvent";
			DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationMonitoredEvent", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    }
}