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

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{
		#region Methods

		private DataTable GetNotificationPublisherList()
		{
			//var dt = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetList(Convert.ToInt32(txtSearchConditionApplicationName.Text), AuditId);
			var dt = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedNotificationPublishers(int NotificationEventTypeId)
		{
			var id = 0;
			if (drpNotificationEventType.SelectedValue != "")
			{
				id = Convert.ToInt32(drpNotificationEventType.SelectedValue);
			}

			var dt = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetByNotificationEventType(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByNotificationEventType(int NotificationEventTypeId, List<int> NotificationPublisherIds)
		{
			var id = Convert.ToInt32(drpNotificationEventType.SelectedValue);
			Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.DeleteByNotificationEventType(id, SessionVariables.RequestProfile);
			Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.CreateByNotificationEventType(id, NotificationPublisherIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetNotificationEventTypeList()
		{
			//var dt = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetList(Convert.ToInt32(txtSearchConditionApplicationName.Text), AuditId);
			var dt = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedNotificationEventTypes(int NotificationPublisherId)
		{
			var id = 0;
			if (drpNotificationPublisher.SelectedValue != "")
			{
				id = Convert.ToInt32(drpNotificationPublisher.SelectedValue);
			}

			var dt = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetByNotificationPublisher(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByNotificationPublisher(int NotificationPublisherId, List<int> NotificationEventTypeIds)
		{
			var id = Convert.ToInt32(drpNotificationPublisher.SelectedValue);
			Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.DeleteByNotificationPublisher(id, SessionVariables.RequestProfile);
			Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.CreateByNotificationPublisher(id, NotificationEventTypeIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpNotificationEventType.DataSource = GetNotificationEventTypeList();
			drpNotificationEventType.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpNotificationEventType.DataValueField = NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId;
			drpNotificationEventType.DataBind();

			drpNotificationPublisher.DataSource = GetNotificationPublisherList();
			drpNotificationPublisher.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpNotificationPublisher.DataValueField = NotificationPublisherDataModel.DataColumns.NotificationPublisherId;
			drpNotificationPublisher.DataBind();
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

			
			SettingCategory = "NotificationPublisherXEventTypeDefaultView";
			
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
			BucketOfNotificationEventType.ConfigureBucket("NotificationEventType", 1, GetNotificationEventTypeList, GetAssociatedNotificationEventTypes, SaveByNotificationPublisher);
			BucketOfNotificationPublisher.ConfigureBucket("NotificationPublisher", 1, GetNotificationPublisherList, GetAssociatedNotificationPublishers, SaveByNotificationEventType);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByNotificationPublisher")
			{
				dynNotificationPublisher.Visible = true;
				dynNotificationEventType.Visible = false;
				BucketOfNotificationEventType.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByNotificationEventType")
			{
				dynNotificationPublisher.Visible = false;
				dynNotificationEventType.Visible = true;
				BucketOfNotificationPublisher.ReloadBucketList();
			}
		}

		protected void drpNotificationEventType_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfNotificationPublisher.ReloadBucketList();
			//BucketOfNotificationPublisher.ConfigureBucket("NotificationPublisher", 1, 2, GetNotificationPublisherList, GetAssociatedNotificationPublishers, SaveByNotificationEventType);
		}

		protected void drpNotificationPublisher_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfNotificationEventType.ReloadBucketList();
		}

		protected void drpSearchConditionApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionApplicationName.Text = drpSearchConditionApplication.SelectedValue;

			BindLists();
			BucketOfNotificationEventType.ReloadBucketList(true);
			BucketOfNotificationPublisher.ReloadBucketList(true);
			BucketOfNotificationEventType.ConfigureBucket("NotificationEventType", 1, GetNotificationEventTypeList, GetAssociatedNotificationEventTypes, SaveByNotificationPublisher);
			BucketOfNotificationPublisher.ConfigureBucket("NotificationPublisher", 1, GetNotificationPublisherList, GetAssociatedNotificationPublishers, SaveByNotificationEventType);
			
		}

		#endregion
	}
}