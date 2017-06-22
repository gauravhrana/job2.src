using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
	{		
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;

			GenericControlPath	= ApplicationCommon.GetControlPath("NotificationPublisherXEventType", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "NotificationPublisherXEventType";
			BreadCrumbObject	= Master.BreadCrumbObject;

			BtnUpdate			= btnUpdate;
			BtnClone			= btnClone;
			BtnCancel			= btnCancel;

		}

		#endregion
	}
}