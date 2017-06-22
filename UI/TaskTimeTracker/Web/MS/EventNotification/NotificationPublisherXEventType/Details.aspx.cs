using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;
			PrimaryEntityKey	= "NotificationPublisherXEventType";
			DetailsControlPath	= ApplicationCommon.GetControlPath("NotificationPublisherXEventType", ControlType.DetailsControl);
			PrimaryPlaceHolder	= plcDetailsList;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}