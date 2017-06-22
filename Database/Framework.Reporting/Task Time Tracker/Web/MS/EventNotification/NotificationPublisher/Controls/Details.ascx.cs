using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisher.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int notificationPublisherId)
        {
            base.ShowData(notificationPublisherId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new NotificationPublisherDataModel();
            data.NotificationPublisherId = notificationPublisherId;

			var items = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, notificationPublisherId, "NotificationPublisher");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblNotificationPublisherIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new NotificationPublisherDataModel();

            SetData(data);
        }

        public void SetData(NotificationPublisherDataModel item)
        {
            SystemKeyId = item.NotificationPublisherId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.NotificationPublisherLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationPublisher;

            PlaceHolderCore = dynNotificationPublisherId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblNotificationPublisherId;
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
                lblNotificationPublisherIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}