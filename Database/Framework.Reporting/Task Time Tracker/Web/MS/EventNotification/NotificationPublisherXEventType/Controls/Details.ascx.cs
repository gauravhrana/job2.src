using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region properties

		#endregion

		#region private methods

		protected override void ShowData(int notificationPublisherXEventTypeId)
		{
			oDetailButtonPanel.SetId = SetId;

			var dataQuery = new NotificationPublisherXEventTypeDataModel();
			dataQuery.NotificationPublisherXEventTypeId = notificationPublisherXEventTypeId;

			var entityList = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetEntityList(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblNotificationPublisherXEventTypeId.Text	= entityItem.NotificationPublisherXEventTypeId.ToString();
					lblNotificationEventType.Text				= entityItem.NotificationEventTypeId.ToString();
					lblNotificationPublisher.Text				= entityItem.NotificationPublisherId.ToString();
					lblCreatedDateId.Text						= entityItem.CreatedDateId.ToString() + entityItem.CreatedTimeId.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType, notificationPublisherXEventTypeId, "NotificationPublisherXEventType");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "NotificationPublisherXEventType");
				}											
			}
			else
			{
				Clear();
			}			
		}

		protected override void Clear()
		{
			lblNotificationPublisherXEventTypeId.Text	= String.Empty;
			lblNotificationPublisher.Text				= String.Empty;
			lblNotificationEventType.Text				= String.Empty;
			lblCreatedDateId.Text						= String.Empty;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblNotificationPublisherXEventTypeIdText.Visible = isTesting;
				lblNotificationPublisherXEventTypeId.Visible = isTesting;
			}
		}

		

		#endregion
	}
}