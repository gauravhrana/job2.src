using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class Clone : PageClone
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;
			PrimaryEntityKey		= "NotificationPublisherXEventType";
			BreadCrumbObject		= Master.BreadCrumbObject;
			GenericControlPath		= ApplicationCommon.GetControlPath("NotificationPublisherXEventType", ControlType.GenericControl);
			PrimaryGenericControl	= myGenericControl;

		}

		#endregion

	}
}