using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Globalization;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties	

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

		public int? NotificationPublisherXEventTypeId
		{
			get
			{
				if (txtNotificationPublisherXEventTypeId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtNotificationPublisherXEventTypeId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtNotificationPublisherXEventTypeId.Text);
				}
			}
		}

		public int NotificationPublisherId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtNotificationPublisherId.Text.Trim());
				else
					return int.Parse(drpNotificationPublisherList.SelectedItem.Value);
			}
		}

		public int NotificationEventTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtNotificationEventTypeId.Text.Trim());
				else
					return int.Parse(drpNotificationEventTypeList.SelectedItem.Value);
			}
		}

		public int CreatedDateId
		{
			get
			{
                return int.Parse(DateTime.ParseExact(txtCreatedDateId.Text, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("yyyyMMdd"));
			}
		}

		public int CreatedTimeId
		{
			get
			{
				return int.Parse(DateTime.ParseExact(txtCreatedDateId.Text, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("HHMMSS"));
			}
		}		

		#endregion properties

		#region private methods

        private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var NotificationPublisherData = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(NotificationPublisherData, drpNotificationPublisherList,
				StandardDataModel.StandardDataColumns.Name,
				NotificationPublisherDataModel.DataColumns.NotificationPublisherId);

			var NotificationEventTypeData = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(NotificationEventTypeData, drpNotificationEventTypeList,
				StandardDataModel.StandardDataColumns.Name,
				NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId);

			if (isTesting)
			{
				
				drpNotificationPublisherList.AutoPostBack = true;
				drpNotificationEventTypeList.AutoPostBack = true;
				
				
				if (drpNotificationPublisherList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtNotificationPublisherId.Text.Trim()))
					{
						drpNotificationPublisherList.SelectedValue = txtNotificationPublisherId.Text;
					}
					else
					{
						txtNotificationPublisherId.Text = drpNotificationPublisherList.SelectedItem.Value;
					}
				}
				if (drpNotificationEventTypeList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtNotificationEventTypeId.Text.Trim()))
					{
						drpNotificationEventTypeList.SelectedValue = txtNotificationEventTypeId.Text;
					}
					else
					{
						txtNotificationEventTypeId.Text = drpNotificationEventTypeList.SelectedItem.Value;
					}
				}
				
				txtNotificationPublisherId.Visible = true;
				txtNotificationEventTypeId.Visible = true;
				
			}
			else
			{
				
				if (!string.IsNullOrEmpty(txtNotificationPublisherId.Text.Trim()))
				{
					drpNotificationPublisherList.SelectedValue = txtNotificationPublisherId.Text;
				}
				if (!string.IsNullOrEmpty(txtNotificationEventTypeId.Text.Trim()))
				{
					drpNotificationEventTypeList.SelectedValue = txtNotificationEventTypeId.Text;
				}
				
				txtNotificationPublisherId.Visible = false;
				txtNotificationEventTypeId.Visible = false;
				
			}
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtNotificationPublisherXEventTypeId.Enabled = chkApplicationId;			
							
		}

		public void LoadData(int notificationPublisherXEventTypeid, bool showId)
		{
			var dataQuery = new NotificationPublisherXEventTypeDataModel();
			dataQuery.NotificationPublisherXEventTypeId = notificationPublisherXEventTypeid;

			var entityList = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetEntityList(dataQuery, SessionVariables.RequestProfile);


			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					if (!showId)
					{

						txtNotificationPublisherXEventTypeId.Text = entityItem.NotificationPublisherXEventTypeId.ToString();
						oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType, notificationPublisherXEventTypeid, "NotificationPublisherXEventType");
						dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "NotificationPublisherXEventType");
					}
					else
					{
						txtNotificationPublisherXEventTypeId.Text = String.Empty;
					}


					txtNotificationPublisherId.Text = entityItem.NotificationPublisherId.ToString();
					txtNotificationEventTypeId.Text = entityItem.NotificationEventTypeId.ToString();
					txtCreatedDateId.Text = entityItem.CreatedDateId.ToString() + entityItem.CreatedTimeId.ToString();

					drpNotificationPublisherList.SelectedValue = entityItem.NotificationPublisherId.ToString();
					drpNotificationEventTypeList.SelectedValue = entityItem.NotificationEventTypeId.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);
				}
			}
			else
			{

				txtNotificationPublisherId.Text = String.Empty;
				txtNotificationEventTypeId.Text = String.Empty;
				txtCreatedDateId.Text = String.Empty;
				txtCreatedDateId.Text = String.Empty;


				drpNotificationPublisherList.SelectedValue = "-1";
				drpNotificationEventTypeList.SelectedValue = "-1";


			}
		}

		

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                SetupDropdown();

                //CalendarExtenderCreatedDate.Format = SessionVariables.UserDateFormat;
                lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;
			}

			var isTesting = SessionVariables.IsTesting;
			txtNotificationPublisherXEventTypeId.Visible = isTesting;
            lblNotificationPublisherXEventTypeId.Visible = isTesting;
		}
		
		protected void drpNotificationPublisherList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtNotificationPublisherId.Text = drpNotificationPublisherList.SelectedItem.Value;
		}

		
		protected void drpNotificationEventTypeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtNotificationEventTypeId.Text = drpNotificationEventTypeList.SelectedItem.Value;
		}

		#endregion

	}
}