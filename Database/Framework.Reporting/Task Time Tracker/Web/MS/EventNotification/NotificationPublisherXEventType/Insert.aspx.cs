using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
	{

		#region private methods

		protected void InsertData()
		{
			var data = new NotificationPublisherXEventTypeDataModel();

			data.NotificationPublisherXEventTypeId = myGenericControl.NotificationPublisherXEventTypeId;
			data.NotificationPublisherId = myGenericControl.NotificationPublisherId;
			data.NotificationEventTypeId = myGenericControl.NotificationEventTypeId;
			data.CreatedDateId = myGenericControl.CreatedDateId;
			data.CreatedTimeId = myGenericControl.CreatedTimeId;

			Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.Create(data, SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{


		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			
			SettingCategory = "NotificationPublisherXEventTypeDefaultView";
			
		}

	

		#endregion
	}
}