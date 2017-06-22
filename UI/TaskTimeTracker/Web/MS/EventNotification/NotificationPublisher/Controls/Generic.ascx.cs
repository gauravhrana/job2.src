using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisher.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new NotificationPublisherDataModel();

            data.NotificationPublisherId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				if(!Framework.Components.EventMonitoring.NotificationPublisherDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
					Framework.Components.EventMonitoring.NotificationPublisherDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.EventMonitoring.NotificationPublisherDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.NotificationPublisherId;
        }

        public override void SetId(int setId, bool chkNotificationPublisherId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkNotificationPublisherId);
            CoreSystemKey.Enabled = chkNotificationPublisherId;
            //txtDescription.Enabled = !chkNotificationPublisherId;
            //txtName.Enabled = !chkNotificationPublisherId;
            //txtSortOrder.Enabled = !chkNotificationPublisherId;
        }

        public void LoadData(int notificationPublisherId, bool showId)
        {
            Clear();

            var data = new NotificationPublisherDataModel();
            data.NotificationPublisherId = SystemKeyId;

			var items = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.NotificationPublisherId;
                oHistoryList.Setup(PrimaryEntity, notificationPublisherId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new NotificationPublisherDataModel();

            SetData(data);
        }

        public void SetData(NotificationPublisherDataModel data)
        {
            SystemKeyId = data.NotificationPublisherId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblNotificationPublisherId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationPublisher;
            PrimaryEntityKey = "NotificationPublisher";
            FolderLocationFromRoot = "NotificationPublisher";

            PlaceHolderCore = dynNotificationPublisherId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtNotificationPublisherId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}