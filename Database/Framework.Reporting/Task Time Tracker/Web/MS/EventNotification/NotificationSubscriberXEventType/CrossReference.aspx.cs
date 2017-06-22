using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationSubscriberXEventType
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{
		#region Methods

		private DataTable GetNotificationSubscriberList()
		{
			//var dt = Framework.Components.EventMonitoring.NotificationSubscriberDataManager.GetList(Convert.ToInt32(txtSearchConditionApplicationName.Text), AuditId);
			var dt = Framework.Components.EventMonitoring.NotificationSubscriberDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedNotificationSubscribers(int NotificationEventTypeId)
		{
			var id = 0;
			if (drpNotificationEventType.SelectedValue != "")
			{
				id = Convert.ToInt32(drpNotificationEventType.SelectedValue);
			}

			var dt = Framework.Components.EventMonitoring.NotificationSubscriberXEventTypeDataManager.GetByNotificationEventType(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByNotificationEventType(int NotificationEventTypeId, List<int> NotificationSubscriberIds)
		{
			var id = Convert.ToInt32(drpNotificationEventType.SelectedValue);
			Framework.Components.EventMonitoring.NotificationSubscriberXEventTypeDataManager.DeleteByNotificationEventType(id, SessionVariables.RequestProfile);
			Framework.Components.EventMonitoring.NotificationSubscriberXEventTypeDataManager.CreateByNotificationEventType(id, NotificationSubscriberIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetNotificationEventTypeList()
		{
			//var dt = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetList(Convert.ToInt32(txtSearchConditionApplicationName.Text), AuditId);
			var dt = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedNotificationEventTypes(int NotificationSubscriberId)
		{
			var id = 0;
			if (drpNotificationSubscriber.SelectedValue != "")
			{
				id = Convert.ToInt32(drpNotificationSubscriber.SelectedValue);
			}

			var dt = Framework.Components.EventMonitoring.NotificationSubscriberXEventTypeDataManager.GetByNotificationSubscriber(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByNotificationSubscriber(int NotificationSubscriberId, List<int> NotificationEventTypeIds)
		{
			var id = Convert.ToInt32(drpNotificationSubscriber.SelectedValue);
			Framework.Components.EventMonitoring.NotificationSubscriberXEventTypeDataManager.DeleteByNotificationSubscriber(id, SessionVariables.RequestProfile);
			Framework.Components.EventMonitoring.NotificationSubscriberXEventTypeDataManager.CreateByNotificationSubscriber(id, NotificationEventTypeIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpNotificationEventType.DataSource = GetNotificationEventTypeList();
			drpNotificationEventType.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpNotificationEventType.DataValueField = NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId;
			drpNotificationEventType.DataBind();

			drpNotificationSubscriber.DataSource = GetNotificationSubscriberList();
			drpNotificationSubscriber.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpNotificationSubscriber.DataValueField = NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId;
			drpNotificationSubscriber.DataBind();
		}

		private void LoadReferenceData()
		{
			// LoadDropDown
			var applicationData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(applicationData, drpSearchConditionApplication,
				StandardDataModel.StandardDataColumns.Name,
				BaseDataModel.BaseDataColumns.ApplicationId);
		}

		#endregion

		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "NotificationSubscriberXEventTypeDefaultView";
			
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadReferenceData();
				BindLists();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			BucketOfNotificationEventType.ConfigureBucket("NotificationEventType", 1, GetNotificationEventTypeList, GetAssociatedNotificationEventTypes, SaveByNotificationSubscriber);
			BucketOfNotificationSubscriber.ConfigureBucket("NotificationSubscriber", 1, GetNotificationSubscriberList, GetAssociatedNotificationSubscribers, SaveByNotificationEventType);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByNotificationSubscriber")
			{
				dynNotificationSubscriber.Visible = true;
				dynNotificationEventType.Visible = false;
				BucketOfNotificationEventType.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByNotificationEventType")
			{
				dynNotificationSubscriber.Visible = false;
				dynNotificationEventType.Visible = true;
				BucketOfNotificationSubscriber.ReloadBucketList();
			}
		}

		protected void drpNotificationEventType_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfNotificationSubscriber.ReloadBucketList();
		}

		protected void drpNotificationSubscriber_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfNotificationEventType.ReloadBucketList();
		}

		protected void drpSearchConditionApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionApplicationName.Text = drpSearchConditionApplication.SelectedValue;

			BindLists();
			BucketOfNotificationEventType.ReloadBucketList(true);
			BucketOfNotificationSubscriber.ReloadBucketList(true);
			BucketOfNotificationEventType.ConfigureBucket("NotificationEventType", 1, GetNotificationEventTypeList, GetAssociatedNotificationEventTypes, SaveByNotificationSubscriber);
			BucketOfNotificationSubscriber.ConfigureBucket("NotificationSubscriber", 1, GetNotificationSubscriberList, GetAssociatedNotificationSubscribers, SaveByNotificationEventType);

		}

		#endregion
	}
}