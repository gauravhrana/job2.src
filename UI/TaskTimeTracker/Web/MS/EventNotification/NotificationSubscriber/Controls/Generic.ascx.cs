using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationSubscriber.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new NotificationSubscriberDataModel();

            data.NotificationSubscriberId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				if(!Framework.Components.EventMonitoring.NotificationSubscriberDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
					Framework.Components.EventMonitoring.NotificationSubscriberDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.EventMonitoring.NotificationSubscriberDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.NotificationSubscriberId;
        }

        public override void SetId(int setId, bool chkNotificationSubscriberId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkNotificationSubscriberId);
            CoreSystemKey.Enabled = chkNotificationSubscriberId;
            //txtDescription.Enabled = !chkNotificationSubscriberId;
            //txtName.Enabled = !chkNotificationSubscriberId;
            //txtSortOrder.Enabled = !chkNotificationSubscriberId;
        }

        public void LoadData(int notificationSubscriberId, bool showId)
        {
            Clear();

            var data = new NotificationSubscriberDataModel();
            data.NotificationSubscriberId = SystemKeyId;

			var items = Framework.Components.EventMonitoring.NotificationSubscriberDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.NotificationSubscriberId;
                oHistoryList.Setup(PrimaryEntity, notificationSubscriberId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new NotificationSubscriberDataModel();

            SetData(data);
        }

        public void SetData(NotificationSubscriberDataModel data)
        {
            SystemKeyId = data.NotificationSubscriberId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblNotificationSubscriberId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationSubscriber;
            PrimaryEntityKey = "NotificationSubscriber";
            FolderLocationFromRoot = "NotificationSubscriber";

            PlaceHolderCore = dynNotificationSubscriberId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtNotificationSubscriberId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}