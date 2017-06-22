using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationEventType.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int notificationEventTypeId)
        {
            base.ShowData(notificationEventTypeId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new NotificationEventTypeDataModel();
            data.NotificationEventTypeId = notificationEventTypeId;

			var items = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, notificationEventTypeId, "NotificationEventType");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblNotificationEventTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new NotificationEventTypeDataModel();

            SetData(data);
        }

        public void SetData(NotificationEventTypeDataModel item)
        {
            SystemKeyId = item.NotificationEventTypeId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.NotificationEventTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationEventType;

            PlaceHolderCore = dynNotificationEventTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblNotificationEventTypeId;
            CoreControlName = lblName;
            CoreControlDescription = lblDescription;
            CoreControlSortOrder = lblSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblNotificationEventTypeIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}