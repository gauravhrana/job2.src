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

namespace ApplicationContainer.UI.Web.EventNotification.NotificationRegistrar.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? NotificationRegistrarId
        {
            get
            {
                if (txtNotificationRegistrarId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtNotificationRegistrarId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtNotificationRegistrarId.Text);
                }
            }
			set
			{
				txtNotificationRegistrarId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? NotificationEventTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtNotificationEventType.Text.Trim());
                else
                    return int.Parse(drpNotificationEventTypeList.SelectedItem.Value);
            }
			set
			{
				txtNotificationEventType.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

		public int? NotificationPublisherId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtNotificationPublisher.Text.Trim());
				else
					return int.Parse(drpNotificationPublisherList.SelectedItem.Value);
			}
			set
			{
				txtNotificationPublisher.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

        public string Message
        {
            get
            {
                return txtMessage.Text.Trim();
            }
			set
			{
				txtMessage.Text = value ?? String.Empty;
			}
        }
      

        #endregion properties

        #region private methods

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var NotificationEventTypeData = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(NotificationEventTypeData, drpNotificationEventTypeList, StandardDataModel.StandardDataColumns.Name,
                 NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId);

			var notificationPublisherData = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetList(SessionVariables.RequestProfile);
			
			UIHelper.LoadDropDown(notificationPublisherData, drpNotificationPublisherList,
			StandardDataModel.StandardDataColumns.Name,
			NotificationPublisherDataModel.DataColumns.NotificationPublisherId);

            if (isTesting)
            {
                drpNotificationEventTypeList.AutoPostBack = true;
				drpNotificationPublisherList.AutoPostBack = true;

                if (drpNotificationEventTypeList.Items.Count > 0)
                {
					if (!string.IsNullOrEmpty(txtNotificationEventType.Text.Trim()))
					{
						drpNotificationEventTypeList.SelectedValue = txtNotificationEventType.Text;						
					}
					else
					{
						txtNotificationEventType.Text = drpNotificationEventTypeList.SelectedItem.Value;
					}
                }


				if (drpNotificationPublisherList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtNotificationPublisher.Text.Trim()))
					{
						drpNotificationPublisherList.SelectedValue = txtNotificationPublisher.Text;
					}
					else
					{
						txtNotificationPublisher.Text = drpNotificationPublisherList.SelectedItem.Value;
					}
				}

				txtNotificationEventType.Visible = true;
				txtNotificationPublisher.Visible = true;

            }
            else
            {
                if (!string.IsNullOrEmpty(txtNotificationEventType.Text.Trim()))
                {
                    drpNotificationEventTypeList.SelectedValue = txtNotificationEventType.Text;
                }
				if (!string.IsNullOrEmpty(txtNotificationPublisher.Text.Trim()))
				{
					drpNotificationPublisherList.SelectedValue = txtNotificationPublisher.Text;
				}
            }
        }

		public override int? Save(string action)
		{
			var data = new NotificationRegistrarDataModel();

			data.NotificationRegistrarId = NotificationRegistrarId;
			data.NotificationEventTypeId = NotificationEventTypeId;			
			data.NotificationPublisherId = NotificationPublisherId;
			data.Message = Message;

			if (action == "Insert")
			{
				if(!Framework.Components.EventMonitoring.NotificationRegistrarDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.EventMonitoring.NotificationRegistrarDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.EventMonitoring.NotificationRegistrarDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return NotificationRegistrarId;
		}

        public override void SetId(int setId, bool chkNotificationRegistrarId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkNotificationRegistrarId);
            txtNotificationRegistrarId.Enabled = chkNotificationRegistrarId;
            //txtDeveloper.Enabled = !chkNotificationRegistrarId;
            //txtName.Enabled = !chkNotificationRegistrarId;
            //txtFeatureOwnerStatusId.Enabled = !chkNotificationRegistrarId;
        }

		public void LoadData(int notificationRegistrarId, bool showId)
		{
			// clear UI				

			Clear();

			var data = new NotificationRegistrarDataModel();
			data.NotificationRegistrarId = notificationRegistrarId;

			var items = Framework.Components.EventMonitoring.NotificationRegistrarDataManager.GetEntityList(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			NotificationRegistrarId = item.NotificationRegistrarId;
			NotificationEventTypeId = item.NotificationEventTypeId;
			NotificationPublisherId = item.NotificationPublisherId;
			Message =item. Message;			

			if (!showId)
			{

				txtNotificationRegistrarId.Text = item.NotificationRegistrarId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.NotificationRegistrar, notificationRegistrarId, "NotificationRegistrar");
			}
			else
			{
				txtNotificationRegistrarId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new NotificationRegistrarDataModel();

			NotificationRegistrarId = data.NotificationRegistrarId;
			NotificationEventTypeId = data.NotificationEventTypeId;
			NotificationPublisherId = data.NotificationPublisherId;
			Message = data.Message;			
		}	

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();
            }
            var isTesting = SessionVariables.IsTesting;
            txtNotificationRegistrarId.Visible = isTesting;
            lblNotificationRegistrarId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationRegistrar;
			PrimaryEntityKey = "NotificationRegistrar";
			FolderLocationFromRoot = "NotificationRegistrar";

			PlaceHolderCore = dynNotificationRegistrarId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

		}

        protected void drpNotificationEventTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNotificationEventType.Text = drpNotificationEventTypeList.SelectedItem.Value;
        }

		protected void drpNotificationPublisherList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtNotificationPublisher.Text = drpNotificationPublisherList.SelectedItem.Value;
		}

        #endregion

    }
}