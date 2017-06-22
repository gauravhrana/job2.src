using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationEventType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new NotificationEventTypeDataModel();

            data.NotificationEventTypeId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				var dtNotificationEventType = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtNotificationEventType.Rows.Count == 0)
                {
					Framework.Components.EventMonitoring.NotificationEventTypeDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.EventMonitoring.NotificationEventTypeDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.NotificationEventTypeId;
        }

        public override void SetId(int setId, bool chkNotificationEventTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkNotificationEventTypeId);
            CoreSystemKey.Enabled = chkNotificationEventTypeId;
            //txtDescription.Enabled = !chkNotificationEventTypeId;
            //txtName.Enabled = !chkNotificationEventTypeId;
            //txtSortOrder.Enabled = !chkNotificationEventTypeId;
        }

        public void LoadData(int notificationEventTypeId, bool showId)
        {
            Clear();

            var data = new NotificationEventTypeDataModel();
            data.NotificationEventTypeId = SystemKeyId;

			var items = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.NotificationEventTypeId;
                oHistoryList.Setup(PrimaryEntity, notificationEventTypeId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new NotificationEventTypeDataModel();

            SetData(data);
        }

        public void SetData(NotificationEventTypeDataModel data)
        {
            SystemKeyId = data.NotificationEventTypeId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblNotificationEventTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationEventType;
            PrimaryEntityKey = "NotificationEventType";
            FolderLocationFromRoot = "NotificationEventType";

            PlaceHolderCore = dynNotificationEventTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtNotificationEventTypeId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}